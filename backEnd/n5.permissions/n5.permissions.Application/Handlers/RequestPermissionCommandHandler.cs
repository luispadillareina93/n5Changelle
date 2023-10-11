using AutoMapper;
using Microsoft.Extensions.Configuration;
using n5.permissions.Application.Commands;
using n5.permissions.Application.Contracts;
using n5.permissions.Domain.Entity;
using n5.permissions.Infraestructure.Kafka;
using n5.permissions.Infraestructure.UnitOfWorks;
using n5.permissions.Infraestructure.Utils;
using Nest;
using System.Threading.Tasks;

namespace n5.permissions.Application.Handlers
{
    public class RequestPermissionCommandHandler : ICommandHandler<RequestPermissionCommand>
    {
        private readonly IUnitOfWork<Permission, int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;
        private readonly IPermissionsProducer _permissionsProducer;
        private readonly IConfiguration _configuration;

        public RequestPermissionCommandHandler(
              IUnitOfWork<Permission, int> unitOfWork,
              IMapper mapper,
              IElasticClient elasticClient,
              IPermissionsProducer permissionsProducer,
              IConfiguration configuration
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _elasticClient = elasticClient;
            _permissionsProducer = permissionsProducer;
            _configuration = configuration;
        }
        public async Task HandleAsync(RequestPermissionCommand command)
        {
            var permission = _mapper.Map<Permission>(command);
           var result= _unitOfWork.Repository.Insert(permission);
            await _unitOfWork.CommitAsync();

            await _elasticClient.IndexDocumentAsync(permission);
            await RegisterEvent(result);
        }
        private async Task RegisterEvent(Permission input)
        {
            var topic = _configuration["Kafka:Topic"];
             await _elasticClient.IndexDocumentAsync(input);
            await _permissionsProducer.ProduceAsync(topic, EventType.Post.ToString());
        }
    }
}

