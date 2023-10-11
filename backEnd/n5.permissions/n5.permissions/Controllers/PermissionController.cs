using Microsoft.AspNetCore.Mvc;
using n5.permissions.Application.Commands;
using n5.permissions.Application.Contracts;
using n5.permissions.Application.Dto;
using n5.permissions.Application.Query;

namespace n5.permissions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {

        private readonly IQueryHandler<GetPermissionsQuery, List<PermissionDto>> _getPermissionsHandler;
        private readonly ICommandHandler<RequestPermissionCommand> _requestPermissionHandler;
        private readonly ICommandHandler<ModifyPermissionCommand> _modifyPermissionHandler;


        private readonly ILogger<PermissionController> _logger;

        public PermissionController(
            ILogger<PermissionController> logger,
            IQueryHandler<GetPermissionsQuery, List<PermissionDto>> getPermissionsHandler,
            ICommandHandler<RequestPermissionCommand> requestPermissionHandler,
            ICommandHandler<ModifyPermissionCommand> modifyPermissionHandler
            )
        {
            _logger = logger;
            _getPermissionsHandler = getPermissionsHandler;
            _requestPermissionHandler = requestPermissionHandler;
            _modifyPermissionHandler = modifyPermissionHandler;

        }
        [Route("GetPermissions")]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var permissions = await _getPermissionsHandler.HandleAsync(new GetPermissionsQuery());
                return Ok(permissions);
            }
            catch (Exception ex )
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(500, "Ocurrió un error interno en el servidor");
            }
        }

        [Route("RequestPermissions")]
        [HttpPost]
        public async Task<IActionResult> RequestPermissions([FromBody] RequestPermissionCommand input)
        {
            try
            {
                await _requestPermissionHandler.HandleAsync(input);
                return Ok(input);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(500, "Ocurrió un error interno en el servidor");
            }
        }

        [Route("ModifyPermissions")]
        [HttpPut]
        public async Task<IActionResult> ModifyPermissions([FromBody] ModifyPermissionCommand input)
        {
            try
            {
                await _modifyPermissionHandler.HandleAsync(input);
                return Ok(input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(500, "Ocurrió un error interno en el servidor");
            }
        }
    }
}