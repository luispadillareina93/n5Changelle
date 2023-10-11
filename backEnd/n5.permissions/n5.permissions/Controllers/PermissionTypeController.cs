using Microsoft.AspNetCore.Mvc;
using n5.permissions.Application.Contracts;
using n5.permissions.Application.Dto;
using n5.permissions.Application.Query;
using n5.permissions.Controllers;

namespace n5.permissions.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionTypeController:ControllerBase
    {

        private readonly IQueryHandler<GetPermissionsQuery, List<PermissionTypeDto>> _getPermissionsTypeHandler;
        ILogger<PermissionTypeController> _logger;

        public PermissionTypeController(
                        IQueryHandler<GetPermissionsQuery, List<PermissionTypeDto>> getPermissionsTypeHandler,
                         ILogger<PermissionTypeController> logger
)
        {
            _getPermissionsTypeHandler = getPermissionsTypeHandler;
            _logger= logger;
        }

        [Route("GetPermissionsType")]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var permissions = await _getPermissionsTypeHandler.HandleAsync(new GetPermissionsQuery());
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(500, "Ocurrió un error interno en el servidor");
            }
        }
    }
}
