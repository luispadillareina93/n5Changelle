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
    public class GetPermissionsQueryHandler : IQueryHandler<GetPermissionsQuery, List<PermissionDto>>
    {
        private readonly IUnitOfWork<Permission, int> _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IPermissionsProducer _permissionsProducer;

        public GetPermissionsQueryHandler(
                    IUnitOfWork<Permission, int> unitOfWork,
                     IConfiguration configuration,
                     IPermissionsProducer permissionsProducer)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _permissionsProducer = permissionsProducer;
        }
        public async Task<List<PermissionDto>> HandleAsync(GetPermissionsQuery query)
        {
            var permissions = _unitOfWork.Repository.GetAll();

            var permissionDtos = permissions.Select(p => new PermissionDto
            {

                Id = p.Id,
                ApellidoEmpleado = p.ApellidoEmpleado,
                NombreEmpleado = p.NombreEmpleado,
                FechaPermiso = p.FechaPermiso,
                TipoPermisoId = p.TipoPermisoId,
                TipoPermiso = p.TipoPermiso.Descripcion
            }).ToList();

            await RegisterEvent();
            return permissionDtos;
        }

        private async Task RegisterEvent()
        {
            var topic = _configuration["kafka:Topic"];
            await _permissionsProducer.ProduceAsync(topic, EventType.Get.ToString());
        }

    }
}
