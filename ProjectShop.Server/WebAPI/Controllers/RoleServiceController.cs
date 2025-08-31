using Microsoft.AspNetCore.Mvc;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.WebAPI.Controllers
{
    public class RoleServiceController : ControllerBase
    {
        private readonly ILogger<RoleServiceController> _logger;
        private readonly IAddAccountRoleService<RolesOfUserModel, RolesOfUserKey> _addAccountRoleService;
        private readonly IAddRoleService<RoleModel> _addRoleService;
        private readonly IUpdateRoleService _updateRoleService;
        private readonly IDeleteAccountRoleService<RolesOfUserKey> _deleteAccountRoleService;
        private readonly ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> _searchAccountRoleService;
        private readonly ISearchRoleService<RoleModel, RoleNavigationOptions> _searchRoleService;

        public RoleServiceController(ILogger<RoleServiceController> logger, IAddAccountRoleService<RolesOfUserModel, RolesOfUserKey> addAccountRoleService,
                IAddRoleService<RoleModel> addRoleService, IUpdateRoleService updateRoleService, IDeleteAccountRoleService<RolesOfUserKey> deleteAccountRoleService, ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions,
                RolesOfUserKey> searchAccountRoleService, ISearchRoleService<RoleModel, RoleNavigationOptions> searchRoleService)
        {
            _logger = logger;
            _addAccountRoleService = addAccountRoleService;
            _addRoleService = addRoleService;
            _updateRoleService = updateRoleService;
            _deleteAccountRoleService = deleteAccountRoleService;
            _searchAccountRoleService = searchAccountRoleService;
            _searchRoleService = searchRoleService;
        }

        //API test get list of roles
        [HttpGet(Name = "roles")]
        public async Task<IActionResult> GetRolesAsync([FromQuery] RoleNavigationOptions options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var roles = await _searchRoleService.GetAllAsync(options, maxGetCount);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving roles");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving roles.");
            }
        }

        //API test get by DateTime
        [HttpGet("byCreatedDate", Name = "rolesByCreatedDate")]
        public async Task<IActionResult> GetRolesByCreatedDateAsync([FromQuery] DateTime dateTime, [FromQuery] ECompareType compareType,
                                                            [FromQuery] RoleNavigationOptions options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var roles = await _searchRoleService.GetByCreatedDateTimeAsync(dateTime, compareType, options, maxGetCount);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving roles by date time");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving roles by date time.");
            }
        }

        // DRY
        //private async Task<IEnumerable<RoleModel>> GetByMonthAndYearAsync(int year, int month, bool isCreated,
        //    RoleNavigationOptions? options, int? maxGetCount)
        //{
        //    return null;
        //}
    }
}
