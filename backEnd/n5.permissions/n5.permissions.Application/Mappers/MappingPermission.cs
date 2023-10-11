using n5.permissions.Application.Commands;
using n5.permissions.Domain.Entity;
using AutoMapper;
using n5.permissions.Application.Dto;

namespace n5.permissions.Application.Mappers
{
    public class MappingPermission:Profile
    {
        public MappingPermission()
        {
            CreateMap<RequestPermissionCommand, Permission>();
            CreateMap<Permission, RequestPermissionCommand>();

            CreateMap<ModifyPermissionCommand, Permission>();
            CreateMap<Permission, ModifyPermissionCommand>();
            CreateMap<PermissionType, PermissionTypeDto>();


        }
    }
}
