using Microsoft.AspNetCore.Mvc;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleServicesController : ControllerBase
    {
        private readonly ILogger<RoleServicesController> _logger;
        private readonly IAddRoleServices<RoleModel> _addRoleServices;
        private readonly IUpdateRoleServices _updateRoleService;
        private readonly ISearchRoleServices<RoleModel, RoleNavigationOptions> _searchRoleServices;

        public RoleServicesController(ILogger<RoleServicesController> logger,
                IAddRoleServices<RoleModel> addRoleServices, IUpdateRoleServices updateRoleService,
                ISearchRoleServices<RoleModel, RoleNavigationOptions> searchRoleServices)
        {
            _logger = logger;
            _addRoleServices = addRoleServices;
            _updateRoleService = updateRoleService;
            _searchRoleServices = searchRoleServices;
        }

        //API test get list of roles
        [HttpGet]
        public async Task<IActionResult> GetRolesAsync([FromQuery] RoleNavigationOptions? options, [FromQuery] int? maxCount)
        {
            try
            {
                var roles = await _searchRoleServices.GetAllAsync(options, maxCount);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving roles");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving roles.");
            }
        }

        // 1. Get by RoleId
        [HttpGet("{roleId:uint}")]
        public async Task<IActionResult> GetRoleByIdAsync([FromRoute] uint roleId, [FromQuery] RoleNavigationOptions? options)
        {
            try
            {
                var result = await _searchRoleServices.GetByRoleIdAsync(roleId, options);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving role with ID {roleId}");
                return StatusCode(500, "An error occurred while retrieving the role.");
            }
        }

        // 2. Get by RoleName (exact)
        [HttpGet("by-name")]
        public async Task<IActionResult> GetRoleByNameAsync([FromQuery] string roleName, [FromQuery] RoleNavigationOptions? options)
        {
            try
            {
                var result = await _searchRoleServices.GetByRoleNameAsync(roleName, options);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving role with name {roleName}");
                return StatusCode(500, "An error occurred while retrieving the role.");
            }
        }

        // 3. Get by Status
        [HttpGet("by-status")]
        public async Task<IActionResult> GetRolesByStatusAsync([FromQuery] bool status, [FromQuery] RoleNavigationOptions? options,
            [FromQuery] int? maxCount)
        {
            try
            {
                var result = await _searchRoleServices.GetByStatusAsync(status, options, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving roles by status {status}");
                return StatusCode(500, "An error occurred while retrieving roles by status.");
            }
        }

        // 4. Get relative by RoleName (like search)
        [HttpGet("search-by-name")]
        public async Task<IActionResult> GetRolesBySimilarNameAsync([FromQuery] string roleName, [FromQuery] RoleNavigationOptions? options,
            [FromQuery] int? maxCount)
        {
            try
            {
                var result = await _searchRoleServices.GetRelativeByRoleName(roleName, options, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error searching roles by name {roleName}");
                return StatusCode(500, "An error occurred while searching roles by name.");
            }
        }

        // 5. Lấy theo tháng/năm (tạo mới hoặc cập nhật)
        [HttpGet("by-date-month-year")]
        public async Task<IActionResult> GetRolesByMonthAndYearAsync([FromQuery] int year, [FromQuery] int month,
            [FromQuery] RoleNavigationOptions? options, [FromQuery] int? maxCount, [FromQuery] bool isCreatedDate = true)
        {
            try
            {
                var roles = isCreatedDate
                    ? await _searchRoleServices.GetByCreatedDateMonthAndYearAsync(year, month, options, maxCount)
                    : await _searchRoleServices.GetByLastUpdatedDateMonthAndYearAsync(year, month, options, maxCount);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving roles by month/year");
                return StatusCode(500, "An error occurred.");
            }
        }

        // 6. Lấy theo năm (so sánh)
        [HttpGet("by-year")]
        public async Task<IActionResult> GetRolesByYearAsync([FromQuery] int year, [FromQuery] ECompareType compareType,
            [FromQuery] RoleNavigationOptions? options, [FromQuery] int? maxCount, [FromQuery] bool isCreatedDate = true)
        {
            try
            {
                var roles = isCreatedDate
                    ? await _searchRoleServices.GetByCreatedYearAsync(year, compareType, options, maxCount)
                    : await _searchRoleServices.GetByLastUpdatedYearAsync(year, compareType, options, maxCount);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving roles by year");
                return StatusCode(500, "An error occurred.");
            }
        }

        // 7. Lấy theo ngày (so sánh)
        [HttpGet("by-date")]
        public async Task<IActionResult> GetRolesByDateAsync([FromQuery] DateTime dateTime, [FromQuery] ECompareType compareType,
            [FromQuery] RoleNavigationOptions? options, [FromQuery] int? maxCount, [FromQuery] bool isCreatedDate = true)
        {
            try
            {
                var roles = isCreatedDate
                    ? await _searchRoleServices.GetByCreatedDateTimeAsync(dateTime, compareType, options, maxCount)
                    : await _searchRoleServices.GetByLastUpdatedDateTimeAsync(dateTime, compareType, options, maxCount);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving roles by date");
                return StatusCode(500, "An error occurred.");
            }
        }

        // 8. Lấy theo khoảng ngày
        [HttpGet("by-date-range")]
        public async Task<IActionResult> GetRolesByDateRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
            [FromQuery] RoleNavigationOptions? options, [FromQuery] int? maxCount, [FromQuery] bool isCreatedDate = true)
        {
            try
            {
                var roles = isCreatedDate
                    ? await _searchRoleServices.GetByCreatedDateTimeRangeAsync(startDate, endDate, options, maxCount)
                    : await _searchRoleServices.GetByLastUpdatedDateTimeRangeAsync(startDate, endDate, options, maxCount);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving roles by date range");
                return StatusCode(500, "An error occurred.");
            }
        }

        // Thêm 1 role mới
        [HttpPost]
        public async Task<IActionResult> AddRoleAsync([FromBody] RoleModel role)
        {
            try
            {
                var result = await _addRoleServices.AddRoleAsync(role);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddRoleAsync endpoint.");
                return StatusCode(500, "An unexpected error occurred while adding the role.");
            }
        }

        // Thêm nhiều role mới
        [HttpPost("batch")]
        public async Task<IActionResult> AddRolesAsync([FromBody] IEnumerable<RoleModel> roles)
        {
            try
            {
                var result = await _addRoleServices.AddRolesAsync(roles);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddRolesAsync endpoint.");
                return StatusCode(500, "An unexpected error occurred while adding the roles.");
            }
        }

        // Cập nhật status 1 role
        [HttpPut("{roleId}/status")]
        public async Task<IActionResult> UpdateRoleStatusAsync(
            [FromRoute] uint roleId,
            [FromQuery] bool status)
        {
            try
            {
                var result = await _updateRoleService.UpdateRoleStatusAsync(roleId, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating status for RoleId={roleId}");
                return StatusCode(500, "An error occurred while updating role status.");
            }
        }

        // Cập nhật status nhiều role
        [HttpPut("status/batch")]
        public async Task<IActionResult> UpdateRoleStatusesAsync(
            [FromBody] RoleUpdateStatusRequest request)
        {
            try
            {
                var result = await _updateRoleService.UpdateRoleStatusAsync(request.RoleIds, request.Status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating status for multiple roles");
                return StatusCode(500, "An error occurred while updating role statuses.");
            }
        }

        // Cập nhật tên 1 role
        [HttpPut("{roleId}/name")]
        public async Task<IActionResult> UpdateRoleNameAsync(
            [FromRoute] uint roleId,
            [FromQuery] string newRoleName)
        {
            try
            {
                var result = await _updateRoleService.UpdateRoleNameAsync(roleId, newRoleName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating name for RoleId={roleId}");
                return StatusCode(500, "An error occurred while updating role name.");
            }
        }

        // Cập nhật tên nhiều role
        [HttpPut("name/batch")]
        public async Task<IActionResult> UpdateRoleNamesAsync(
            [FromBody] RoleUpdateNamesRequest request)
        {
            try
            {
                var result = await _updateRoleService.UpdateRoleNamesAsync(request.RoleIds, request.NewRoleNames);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating names for multiple roles");
                return StatusCode(500, "An error occurred while updating role names.");
            }
        }
    }
}
