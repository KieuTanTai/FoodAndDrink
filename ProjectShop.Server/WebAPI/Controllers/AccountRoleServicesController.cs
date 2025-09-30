using Microsoft.AspNetCore.Mvc;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

[ApiController]
[Route("api/[controller]")]
public class AccountRoleServicesController : ControllerBase
{
    private readonly ISearchAccountRoleServices<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> _searchAccountRoleServices;
    private readonly IAddAccountRoleServices<RolesOfUserModel, RolesOfUserKey> _addAccountRoleServices;
    private readonly IDeleteAccountRoleServices<RolesOfUserKey> _deleteAccountRoleServices;
    private readonly ILogger<AccountRoleServicesController> _logger;

    public AccountRoleServicesController(ISearchAccountRoleServices<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> searchAccountRoleServices,
        IDeleteAccountRoleServices<RolesOfUserKey> deleteAccountRoleServices,
        IAddAccountRoleServices<RolesOfUserModel, RolesOfUserKey> addAccountRoleServices,
        ILogger<AccountRoleServicesController> logger)
    {
        _logger = logger;
        _addAccountRoleServices = addAccountRoleServices;
        _searchAccountRoleServices = searchAccountRoleServices;
        _deleteAccountRoleServices = deleteAccountRoleServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] RolesOfUserNavigationOptions? options, [FromQuery] int? maxCount)
    {
        try
        {
            var result = await _searchAccountRoleServices.GetAllAsync(options, maxCount);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all RolesOfUser");
            return StatusCode(500, "An error occurred while retrieving all RolesOfUser.");
        }
    }

    [HttpGet("by-added-month-year")]
    public async Task<IActionResult> GetByAddedMonthAndYearAsync([FromQuery] int year, [FromQuery] int month,
        [FromQuery] RolesOfUserNavigationOptions? options, [FromQuery] int? maxCount)
    {
        try
        {
            var result = await _searchAccountRoleServices.GetByAddedDateMonthAndYearAsync(year, month, options, maxCount);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving RolesOfUser by added month/year {month}/{year}");
            return StatusCode(500, "An error occurred while retrieving RolesOfUser by added month/year.");
        }
    }

    [HttpGet("by-added-date")]
    public async Task<IActionResult> GetByAddedDateTimeAsync([FromQuery] DateTime dateTime, [FromQuery] ECompareType compareType,
        [FromQuery] RolesOfUserNavigationOptions? options, [FromQuery] int? maxCount)
    {
        try
        {
            var result = await _searchAccountRoleServices.GetByAddedDateTimeAsync(dateTime, compareType, options, maxCount);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving RolesOfUser by added date {dateTime}");
            return StatusCode(500, "An error occurred while retrieving RolesOfUser by added date.");
        }
    }

    [HttpGet("by-added-date-range")]
    public async Task<IActionResult> GetByAddedDateTimeRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
        [FromQuery] RolesOfUserNavigationOptions? options, [FromQuery] int? maxCount)
    {
        try
        {
            var result = await _searchAccountRoleServices.GetByAddedDateTimeRangeAsync(startDate, endDate, options, maxCount);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving RolesOfUser by added date range {startDate} - {endDate}");
            return StatusCode(500, "An error occurred while retrieving RolesOfUser by added date range.");
        }
    }

    [HttpGet("by-added-year")]
    public async Task<IActionResult> GetByAddedYearAsync([FromQuery] int year, [FromQuery] ECompareType compareType,
        [FromQuery] RolesOfUserNavigationOptions? options, [FromQuery] int? maxCount)
    {
        try
        {
            var result = await _searchAccountRoleServices.GetByAddedYearAsync(year, compareType, options, maxCount);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving RolesOfUser by added year {year}");
            return StatusCode(500, "An error occurred while retrieving RolesOfUser by added year.");
        }
    }

    [HttpPost("by-keys")]
    public async Task<IActionResult> GetByKeysAsync([FromBody] RolesOfUserKey keys, [FromQuery] RolesOfUserNavigationOptions? options)
    {
        try
        {
            var result = await _searchAccountRoleServices.GetByKeysAsync(keys, options);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving RolesOfUser by keys");
            return StatusCode(500, "An error occurred while retrieving RolesOfUser by keys.");
        }
    }

    [HttpPost("by-list-keys")]
    public async Task<IActionResult> GetByListKeysAsync([FromBody] IEnumerable<RolesOfUserKey> listKeys,
        [FromQuery] RolesOfUserNavigationOptions? options, [FromQuery] int? maxCount)
    {
        try
        {
            var result = await _searchAccountRoleServices.GetByListKeysAsync(listKeys, options, maxCount);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving RolesOfUser by list of keys");
            return StatusCode(500, "An error occurred while retrieving RolesOfUser by list of keys.");
        }
    }

    [HttpGet("by-role-id")]
    public async Task<IActionResult> GetByRoleIdAsync([FromQuery] uint roleId,
        [FromQuery] RolesOfUserNavigationOptions? options, [FromQuery] int? maxCount)
    {
        try
        {
            var result = await _searchAccountRoleServices.GetByRoleIdAsync(roleId, options, maxCount);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving RolesOfUser by roleId {roleId}");
            return StatusCode(500, "An error occurred while retrieving RolesOfUser by roleId.");
        }
    }

    [HttpPost("by-role-ids")]
    public async Task<IActionResult> GetByRoleIdsAsync([FromBody] IEnumerable<uint> roleIds,
        [FromQuery] RolesOfUserNavigationOptions? options, [FromQuery] int? maxCount)
    {
        try
        {
            var result = await _searchAccountRoleServices.GetByRoleIdsAsync(roleIds, options, maxCount);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving RolesOfUser by list of roleIds");
            return StatusCode(500, "An error occurred while retrieving RolesOfUser by list of roleIds.");
        }
    }

    [HttpGet("by-account-id")]
    public async Task<IActionResult> GetByAccountIdAsync([FromQuery] uint accountId, [FromQuery] RolesOfUserNavigationOptions? options,
        [FromQuery] int? maxCount)
    {
        try
        {
            var result = await _searchAccountRoleServices.GetByAccountIdAsync(accountId, options, maxCount);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving RolesOfUser by accountId {accountId}");
            return StatusCode(500, "An error occurred while retrieving RolesOfUser by accountId.");
        }
    }

    [HttpPost("by-account-ids")]
    public async Task<IActionResult> GetByAccountIdsAsync([FromBody] IEnumerable<uint> accountIds, [FromQuery] RolesOfUserNavigationOptions? options,
        [FromQuery] int? maxCount)
    {
        try
        {
            var result = await _searchAccountRoleServices.GetByAccountIdsAsync(accountIds, options, maxCount);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving RolesOfUser by accountIds");
            return StatusCode(500, "An error occurred while retrieving RolesOfUser by accountIds.");
        }
    }

    // Th�m 1 account role (g�n 1 role cho 1 account)
    [HttpPost]
    public async Task<IActionResult> AddAccountRoleAsync([FromBody] RolesOfUserKey keys)
    {
        try
        {
            var result = await _addAccountRoleServices.AddAccountRoleAsync(keys);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred in AddAccountRoleAsync endpoint. AccountId: {keys.AccountId}, RoleId: {keys.RoleId}");
            return StatusCode(500, "An unexpected error occurred while adding the account role.");
        }
    }

    // Th�m nhi?u account role (g�n nhi?u role cho nhi?u account)
    [HttpPost("batch")]
    public async Task<IActionResult> AddAccountRolesAsync([FromBody] IEnumerable<RolesOfUserModel> entities)
    {
        try
        {
            var result = await _addAccountRoleServices.AddAccountRolesAsync(entities);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in AddAccountRolesAsync endpoint.");
            return StatusCode(500, "An unexpected error occurred while adding account roles.");
        }
    }

    // X�a 1 account role theo composite key
    [HttpDelete]
    public async Task<IActionResult> DeleteAccountRoleAsync([FromBody] RolesOfUserKey keys)
    {
        try
        {
            var result = await _deleteAccountRoleServices.DeleteAccountRoleAsync(keys);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting account role with keys: {keys}");
            return StatusCode(500, "An error occurred while deleting the account role.");
        }
    }

    // X�a nhi?u account role theo danh s�ch key
    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteAccountRolesAsync([FromBody] IEnumerable<RolesOfUserKey> listKeys)
    {
        try
        {
            var result = await _deleteAccountRoleServices.DeleteAccountRolesAsync(listKeys);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting multiple account roles.");
            return StatusCode(500, "An error occurred while deleting multiple account roles.");
        }
    }

    // X�a t?t c? role c?a 1 account
    [HttpDelete("by-account-id/{accountId}")]
    public async Task<IActionResult> DeleteByAccountIdAsync([FromRoute] int accountId)
    {
        try
        {
            var result = await _deleteAccountRoleServices.DeleteByAccountIdAsync((uint)accountId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting account roles by accountId: {accountId}");
            return StatusCode(500, "An error occurred while deleting account roles by accountId.");
        }
    }

    // X�a t?t c? role c?a nhi?u account
    [HttpDelete("by-account-ids")]
    public async Task<IActionResult> DeleteByAccountIdsAsync([FromBody] IEnumerable<uint> accountIds)
    {
        try
        {
            var result = await _deleteAccountRoleServices.DeleteByAccountIdsAsync(accountIds);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting account roles by account ids.");
            return StatusCode(500, "An error occurred while deleting account roles by account ids.");
        }
    }

    // X�a t?t c? account c?a 1 role
    [HttpDelete("by-role-id/{roleId}")]
    public async Task<IActionResult> DeleteByRoleIdAsync([FromRoute] int roleId)
    {
        try
        {
            var result = await _deleteAccountRoleServices.DeleteByRoleIdAsync((uint)roleId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting account roles by roleId: {roleId}");
            return StatusCode(500, "An error occurred while deleting account roles by roleId.");
        }
    }

    // X�a t?t c? account c?a nhi?u role
    [HttpDelete("by-role-ids")]
    public async Task<IActionResult> DeleteByRoleIdsAsync([FromBody] IEnumerable<uint> roleIds)
    {
        try
        {
            var result = await _deleteAccountRoleServices.DeleteByRoleIdsAsync(roleIds);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting account roles by role ids.");
            return StatusCode(500, "An error occurred while deleting account roles by role ids.");
        }
    }
}