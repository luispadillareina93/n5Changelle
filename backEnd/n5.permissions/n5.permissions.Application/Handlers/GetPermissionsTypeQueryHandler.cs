using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using n5.permissions.Application.Contracts;
using n5.permissions.Application.Dto;
using n5.permissions.Application.Query;
using n5.permissions.Domain.Entity;
using n5.permissions.Infraestructure.Kafka;
using n5.permissions.Infraestructure.UnitOfWorks;
using n5.permissions.Infraestructure.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n5.permissions.Application.Handlers
{
    public class GetPermissionsTypeQueryHandler : IQueryHandler<GetPermissionsQuery, List<PermissionTypeDto>>
    {
        private readonly IUnitOfWork<PermissionType, int> _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IPermissionsProducer _permissionsProducer;

        public GetPermissionsTypeQueryHandler(
                    IUnitOfWork<PermissionType, int> unitOfWork,
                     IConfiguration configuration,
                     IPermissionsProducer permissionsProducer)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _permissionsProducer = permissionsProducer;
        }
        public async Task<List<PermissionTypeDto>> HandleAsync(GetPermissionsQuery query)
        {
            var permissions = _unitOfWork.Repository.GetAll();

            var permissionDtos = permissions.Select(p => new PermissionTypeDto
            {

                Id = p.Id,
                Descripcion = p.Descripcion
            }).ToList();

            return permissionDtos;
        }

    }
}
