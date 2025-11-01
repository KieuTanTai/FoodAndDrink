using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MySqlX.XDevAPI.Common;
using OpenQA.Selenium.Interactions;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.FrontEndRequestsForAccount;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.ValueObjects.PlatformRules;

namespace ProjectShop.Server.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController(IAccountServices accountServices, ILogger<AccountController> logger, ILogService logService) : ControllerBase
    {
        private readonly IAccountServices _accountServices = accountServices;
        private readonly ILogger<AccountController> _logger = logger;
        private readonly ILogService _logService = logService;

        #region HttpGet Actions

        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUserAsync([FromQuery] AccountNavigationOptions options, CancellationToken cancellationToken = default)
        {
            try
            {
                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
                    return Unauthorized("User is not authenticated.");

                string? userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
                    return Unauthorized("Invalid user identifier.");

                uint accountId = Convert.ToUInt32(userId);
                ServiceResult<Account> result = await _accountServices.GetByAccountIdAsync(accountId, options, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to retrieve current user: {ErrorMessage}", result.LogEntries?.LastOrDefault()?.Message);
                    return NotFound("User not found.");
                }

                _logger.LogInformation("Current user retrieved successfully: {UserId}", userId);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the current user.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("by-account-id")]
        public async Task<IActionResult> GetByAccountIdAsync([FromQuery] uint accountId, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (accountId == 0)
                return BadRequest("Account ID must be provided.");
            try
            {
                ServiceResult<Account> result = await _accountServices.GetByAccountIdAsync(accountId, options, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to retrieve user by account ID {AccountId}: {ErrorMessage}", accountId, result.LogEntries?.LastOrDefault()?.Message);
                    return NotFound("User not found.");
                }

                _logger.LogInformation("User retrieved successfully by account ID: {AccountId}", accountId);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user by account ID {AccountId}.", accountId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("by-username")]
        public async Task<IActionResult> GetByUserNameAsync([FromQuery] string userName, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userName))
                return BadRequest("Username must be provided.");
            try
            {
                ServiceResult<Account> result = await _accountServices.GetByUserNameAsync(userName, options, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to retrieve user by username {UserName}: {ErrorMessage}", userName, result.LogEntries?.LastOrDefault()?.Message);
                    return NotFound("User not found.");
                }

                _logger.LogInformation("User retrieved successfully by username: {UserName}", userName);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user by username {UserName}.", userName);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("all-accounts")]
        public async Task<IActionResult> GetAllAccountsAsync([FromQuery] uint? fromRecord = 0, [FromQuery] uint? pageSize = 10, AccountNavigationOptions? options = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                ServiceResults<Account> result = await _accountServices.GetAllWithOffsetAsync(fromRecord, pageSize, options, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to retrieve all accounts: {ErrorMessage}", result.LogEntries?.LastOrDefault()?.Message);
                    return NotFound("No accounts found.");
                }

                _logger.LogInformation("All accounts retrieved successfully.");
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all accounts.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("by-status")]
        public async Task<IActionResult> GetAccountsByStatusAsync([FromQuery] bool isActive, [FromQuery] uint? fromRecord = 0,
            [FromQuery] uint? pageSize = 10, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default)
        {
            try
            {
                ServiceResults<Account> result = await _accountServices.GetByStatusAsync(isActive, fromRecord, pageSize, options, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to retrieve accounts by status {IsActive}: {ErrorMessage}", isActive, result.LogEntries?.LastOrDefault()?.Message);
                    return NotFound("No accounts found with the specified status.");
                }

                _logger.LogInformation("Accounts retrieved successfully by status: {IsActive}", isActive);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving accounts by status {IsActive}.", isActive);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("by-created-year")]
        public async Task<IActionResult> GetAccountsByCreatedYearAsync([FromQuery] int year, [FromQuery] ECompareType compareType,
            [FromQuery] uint? fromRecord = 0, [FromQuery] uint? pageSize = 10, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (year <= 0 || year > DateTime.Now.Year)
                return BadRequest("Invalid year provided.");
            try
            {
                ServiceResults<Account> result = await _accountServices.GetByCreatedYearAsync(year, compareType, fromRecord, pageSize, options, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to retrieve accounts by created year {Year} with comparison {CompareType}: {ErrorMessage}",
                        year, compareType, result.LogEntries?.LastOrDefault()?.Message);
                    return NotFound("No accounts found with the specified created year.");
                }

                _logger.LogInformation("Accounts retrieved successfully by created year: {Year} with comparison {CompareType}", year, compareType);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving accounts by created year {Year} with comparison {CompareType}.", year, compareType);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("by-created-month-and-year")]
        public async Task<IActionResult> GetAccountsByCreatedMonthAndYearAsync([FromQuery] int year, [FromQuery] int month,
            [FromQuery] ECompareType compareType, [FromQuery] uint? fromRecord = 0, [FromQuery] uint? pageSize = 10,
            AccountNavigationOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (year <= 0 || year > DateTime.Now.Year || month < 1 || month > 12)
                return BadRequest("Invalid month or year provided.");
            try
            {
                ServiceResults<Account> result = await _accountServices.GetByCreatedDateMonthAndYearAsync(year, month, compareType, fromRecord, pageSize, options, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to retrieve accounts by created month {Month}/{Year} with comparison {CompareType}: {ErrorMessage}",
                        month, year, compareType, result.LogEntries?.LastOrDefault()?.Message);
                    return NotFound("No accounts found with the specified created month and year.");
                }

                _logger.LogInformation("Accounts retrieved successfully by created month: {Month}/{Year} with comparison {CompareType}", month, year, compareType);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving accounts by created month {Month}/{Year} with comparison {CompareType}.", month, year, compareType);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("by-created-date-range")]
        public async Task<IActionResult> GetAccountsByCreatedDateRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
            [FromQuery] uint? fromRecord = 0, [FromQuery] uint? pageSize = 10, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (startDate > endDate)
                return BadRequest("Start date must be less than or equal to end date.");
            if (startDate > DateTime.Now || endDate > DateTime.Now)
                return BadRequest("Date range must be within the last year and not in the future.");
            try
            {
                ServiceResults<Account> result = await _accountServices.GetByCreatedDateTimeRangeAsync(startDate, endDate, fromRecord, pageSize, options, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to retrieve accounts by created date range {StartDate} to {EndDate}: {ErrorMessage}",
                        startDate, endDate, result.LogEntries?.LastOrDefault()?.Message);
                    return NotFound("No accounts found within the specified created date range.");
                }

                _logger.LogInformation("Accounts retrieved successfully by created date range: {StartDate} to {EndDate}", startDate, endDate);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving accounts by created date range {StartDate} to {EndDate}.", startDate, endDate);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("by-last-updated-year")]
        public async Task<IActionResult> GetAccountsByLastUpdatedYearAsync([FromQuery] int year, [FromQuery] ECompareType compareType,
            [FromQuery] uint? fromRecord = 0, [FromQuery] uint? pageSize = 10, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (year <= 0 || year > DateTime.Now.Year)
                return BadRequest("Invalid year provided.");
            try
            {
                ServiceResults<Account> result = await _accountServices.GetByLastUpdatedYearAsync(year, compareType, fromRecord, pageSize, options, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to retrieve accounts by last updated year {Year} with comparison {CompareType}: {ErrorMessage}",
                        year, compareType, result.LogEntries?.LastOrDefault()?.Message);
                    return NotFound("No accounts found with the specified last updated year.");
                }

                _logger.LogInformation("Accounts retrieved successfully by last updated year: {Year} with comparison {CompareType}", year, compareType);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving accounts by last updated year {Year} with comparison {CompareType}.", year, compareType);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("by-last-updated-month-and-year")]
        public async Task<IActionResult> GetAccountsByLastUpdatedMonthAndYearAsync([FromQuery] int year, [FromQuery] int month,
            [FromQuery] ECompareType compareType, [FromQuery] uint? fromRecord = 0, [FromQuery] uint? pageSize = 10,
            AccountNavigationOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (year <= 0 || year > DateTime.Now.Year || month < 1 || month > 12)
                return BadRequest("Invalid month or year provided.");
            try
            {
                ServiceResults<Account> result = await _accountServices.GetByLastUpdatedDateMonthAndYearAsync(year, month, compareType, fromRecord, pageSize, options, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to retrieve accounts by last updated month {Month}/{Year} with comparison {CompareType}: {ErrorMessage}",
                        month, year, compareType, result.LogEntries?.LastOrDefault()?.Message);
                    return NotFound("No accounts found with the specified last updated month and year.");
                }

                _logger.LogInformation("Accounts retrieved successfully by last updated month: {Month}/{Year} with comparison {CompareType}", month, year, compareType);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving accounts by last updated month {Month}/{Year} with comparison {CompareType}.", month, year, compareType);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("by-last-updated-date-range")]
        public async Task<IActionResult> GetAccountsByLastUpdatedDateRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
            [FromQuery] uint? fromRecord = 0, [FromQuery] uint? pageSize = 10, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (startDate > endDate)
                return BadRequest("Start date must be less than or equal to end date.");
            if (startDate > DateTime.Now || endDate > DateTime.Now)
                return BadRequest("Date range must be within the last year and not in the future.");
            try
            {
                ServiceResults<Account> result = await _accountServices.GetByLastUpdatedDateTimeRangeAsync(startDate, endDate, fromRecord, pageSize, options, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to retrieve accounts by last updated date range {StartDate} to {EndDate}: {ErrorMessage}",
                        startDate, endDate, result.LogEntries?.LastOrDefault()?.Message);
                    return NotFound("No accounts found within the specified last updated date range.");
                }

                _logger.LogInformation("Accounts retrieved successfully by last updated date range: {StartDate} to {EndDate}", startDate, endDate);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving accounts by last updated date range {StartDate} to {EndDate}.", startDate, endDate);
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion

        #region HttpPost Actions

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] FELoginRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                return BadRequest("Request body is null.");

            try
            {
                // Check if request has valid credentials (email and password not empty)
                bool hasValidRequestCredentials = !string.IsNullOrEmpty(request.Email?.Trim()) && !string.IsNullOrEmpty(request.Password?.Trim());

                // If request has valid credentials, use them for login
                if (hasValidRequestCredentials)
                    return await HelperAccountLoginAsync(request, cancellationToken);

                // If request is not valid, try to validate cookie
                if (Request.Cookies.ContainsKey(".AspNetCore.Cookies"))
                {
                    IActionResult? cookieResult = await HelperValidateCookieLoginAsync(cancellationToken);
                    if (cookieResult != null)
                        return cookieResult;
                }

                // Both request and cookie are invalid
                return BadRequest("Email and Password are required, or valid authentication cookie must be provided.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest, CancellationToken cancellationToken = default)
        {
            if (registerRequest == null)
                return BadRequest("Request body is null.");
            if (string.IsNullOrEmpty(registerRequest.Email.Trim()) || string.IsNullOrEmpty(registerRequest.Password.Trim()))
                return BadRequest("Email and Password are required.");

            try
            {
                Account newAccount = new()
                {
                    UserName = registerRequest.Email.Trim(),
                    Password = registerRequest.Password.Trim(),
                };
                ServiceResult<Account> result = await _accountServices.AddAccountAsync(newAccount, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Registration failed for user {Email}: {ErrorMessage}", registerRequest.Email, result.LogEntries?.LastOrDefault()?.Message);
                    return BadRequest(result.LogEntries);
                }

                _logger.LogInformation("User {Email} registered successfully.", registerRequest.Email);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during registration.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("add-accounts")]
        public async Task<IActionResult> AddMultipleAccountsAsync([FromBody] IEnumerable<Account> accounts, CancellationToken cancellationToken = default)
        {
            if (accounts == null || !accounts.Any())
                return BadRequest("No accounts provided for addition.");

            if (accounts.Any(account => account.AccountId == 0 || string.IsNullOrEmpty(account.UserName) || string.IsNullOrEmpty(account.Password)))
                return BadRequest("Each account must have a valid AccountId, UserName, and Password.");

            try
            {
                ServiceResults<Account> result = await _accountServices.AddAccountsAsync(accounts, HttpContext, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Adding multiple accounts failed: {ErrorMessage}", result.LogEntries?.LastOrDefault()?.Message);
                    return BadRequest(result.LogEntries);
                }

                _logger.LogInformation("Multiple accounts added successfully. Count: {Count}", result.Data?.Count() ?? 0);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding multiple accounts.");
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion

        #region HttpDelete Actions

        [HttpDelete("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                await HttpContext.SignOutAsync();
                _logger.LogInformation("User logged out successfully.");
                return Ok("Logged out successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during logout.");
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion

        #region HttpPut Actions

        [HttpPut("update-account-status")]
        public async Task<IActionResult> UpdateAccountStatusAsync([FromBody] AccountUpdateStatusRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                return BadRequest("Request body is null.");

            // Validate that at least one identifier is provided
            if (request.AccountId == 0 && string.IsNullOrEmpty(request.UserName))
                return BadRequest("Either Account ID or Username must be provided.");

            try
            {
                JsonLogEntry result;

                // Prioritize AccountId if provided, otherwise use UserName
                if (request.AccountId > 0)
                {
                    result = await _accountServices.UpdateAccountStatusAsync(request.AccountId, request.Status, HttpContext, cancellationToken);
                    if (!result.AffectedRows.HasValue || result.AffectedRows.Value == 0)
                    {
                        _logger.LogWarning("Failed to update account {AccountId} status: {ErrorMessage}", request.AccountId, result.Message);
                        return BadRequest(result.Message);
                    }
                    _logger.LogInformation("Account {AccountId} status updated successfully to {Status}.", request.AccountId, request.Status);
                }
                else
                {
                    result = await _accountServices.UpdateAccountStatusByUserNameAsync(request.UserName, request.Status, HttpContext, cancellationToken);
                    if (!result.AffectedRows.HasValue || result.AffectedRows.Value == 0)
                    {
                        _logger.LogWarning("Failed to update account status for username {UserName}: {ErrorMessage}", request.UserName, result.Message);
                        return BadRequest(result.Message);
                    }
                    _logger.LogInformation("Account status for username {UserName} updated successfully to {Status}.", request.UserName, request.Status);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating account status.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] AccountUpdatePasswordRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                return BadRequest("Request body is null.");

            // Validate that at least one identifier is provided
            if (request.AccountId == 0 && string.IsNullOrEmpty(request.UserName))
                return BadRequest("Either Account ID or Username must be provided.");

            if (string.IsNullOrEmpty(request.NewPassword))
                return BadRequest("New password must be provided.");

            try
            {
                JsonLogEntry result;

                // Service only has UpdatePasswordAsync(userName, newPassword), so we need to get username first if only AccountId is provided
                string userName = request.UserName;
                if (request.AccountId > 0 && string.IsNullOrEmpty(userName))
                {
                    // Get account by ID to retrieve username
                    ServiceResult<Account> accountResult = await _accountServices.GetByAccountIdAsync(request.AccountId, null, cancellationToken);
                    if (!accountResult.IsSuccess || accountResult.Data == null)
                    {
                        _logger.LogWarning("Failed to find account {AccountId} for password update.", request.AccountId);
                        return BadRequest("Account not found.");
                    }
                    userName = accountResult.Data.UserName;
                }

                result = await _accountServices.UpdatePasswordAsync(userName, request.NewPassword, HttpContext, cancellationToken);
                if (!result.AffectedRows.HasValue || result.AffectedRows.Value == 0)
                {
                    _logger.LogWarning("Failed to update password for username {UserName}: {ErrorMessage}", userName, result.Message);
                    return BadRequest(result.Message);
                }

                _logger.LogInformation("Password for username {UserName} updated successfully.", userName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating password.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("update-multiple-accounts-status")]
        public async Task<IActionResult> UpdateMultipleAccountsStatusAsync([FromBody] IEnumerable<AccountUpdateStatusRequest> requests, CancellationToken cancellationToken = default)
        {
            if (requests == null || !requests.Any())
                return BadRequest("Request body is null or empty.");

            HashSet<bool> statusSet = [.. requests.Select(r => r.Status)];
            if (statusSet.Count > 1)
                return BadRequest("All requests must have the same status value.");

            // Validate all requests
            if (requests.Any(request => request.AccountId == 0 && string.IsNullOrEmpty(request.UserName)))
                return BadRequest($"{requests.Count(request => request.AccountId == 0 && string.IsNullOrEmpty(request.UserName))} request(s) have neither Account ID nor Username.");
            if (requests.Any(request => request.AccountId > 0 && !string.IsNullOrEmpty(request.UserName)))
                return BadRequest($@"{requests.Count(request => request.AccountId > 0 && !string.IsNullOrEmpty(request.UserName))} request(s) have both Account ID and Username. 
                    Please provide only one identifier per request.");

            try
            {
                bool targetStatus = requests.First().Status;
                var targetUpdateBy = requests.First().AccountId > 0 ? "AccountId" : "UserName";
                IEnumerable<JsonLogEntry> results = [];
                if (targetUpdateBy == "AccountId")
                {
                    var accountIds = requests.Select(r => r.AccountId).Distinct();
                    results = await _accountServices.UpdateAccountStatusAsync(accountIds, targetStatus, HttpContext, cancellationToken);
                }
                else
                {
                    var userNames = requests.Select(r => r.UserName).Distinct();
                    results = await _accountServices.UpdateAccountStatusByUserNamesAsync(userNames, targetStatus, HttpContext, cancellationToken);
                }
                if (results == null || !results.Any())
                {
                    _logger.LogWarning("No accounts were updated in the batch status update.");
                    return BadRequest("No accounts were updated.");
                }
                _logger.LogInformation("Batch account status update completed. Total requests: {TotalCount}, Results returned: {ResultCount}",
                    requests.Count(), results.Count());
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating multiple accounts status.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("update-multiple-passwords")]
        public async Task<IActionResult> UpdateMultiplePasswordsAsync([FromBody] IEnumerable<FrontEndUpdatePasswordAccount> requests, CancellationToken cancellationToken = default)
        {
            if (requests == null || !requests.Any())
                return BadRequest("Request body is null or empty.");
            // Validate all requests
            if (requests.Any(request => string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password)))
                return BadRequest($@"{requests.Count(request => string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))} 
                    request(s) have invalid Username or Password.");
            try
            {
                var results = await _accountServices.UpdatePasswordAsync([.. requests], HttpContext, cancellationToken);
                if (results == null || !results.Any())
                {
                    _logger.LogWarning("No passwords were updated in the batch password update.");
                    return BadRequest("No passwords were updated.");
                }
                _logger.LogInformation("Batch password update completed. Total requests: {TotalCount}, Results returned: {ResultCount}",
                    requests.Count(), results.Count());
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating multiple passwords.");
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion

        #region Helper Methods for ClaimsPrincipal
        //! TODO: Change to setup Admin Role if Have for Principal
        //? Helper Method to Create ClaimsPrincipal

        private async Task SetUserClaimsAsync(Account account, bool isRememberMe = false)
        {
            ClaimsPrincipal claimsPrincipal = CreateClaimsPrincipalAsync(account);
            HttpContext.User = claimsPrincipal;
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = isRememberMe,
                ExpiresUtc = isRememberMe ? DateTimeOffset.UtcNow.AddDays(14) : DateTimeOffset.UtcNow.AddHours(1),
                AllowRefresh = true
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
        }

        private static ClaimsPrincipal CreateClaimsPrincipalAsync(Account account)
        {
            List<Claim> claims =
            [
                new (ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                new (ClaimTypes.Name, account.UserName),
            ];
            foreach (var role in account.AccountRoles)
                claims.Add(new Claim(ClaimTypes.Role, role.RoleId.ToString()));
            foreach (var permission in account.AccountAdditionalPermissions)
                claims.Add(new Claim("Permission", permission.PermissionId.ToString()));

            ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new(identity);
        }

        private async Task<bool> HelperAuthenticateCurrentPermissionsAndRolesAsync()
        {
            var currentUser = ClaimsPrincipal.Current;
            if (currentUser == null || !currentUser.Identity!.IsAuthenticated)
                return false;
            try
            {
                IEnumerable<Claim> roleClaims = currentUser.Claims.Where(c => c.Type == ClaimTypes.Role);
                IEnumerable<Claim> permissionClaims = currentUser.Claims.Where(c => c.Type == "Permission");
                IEnumerable<uint> roleIds = roleClaims.Select(c => Convert.ToUInt32(c.Value));
                IEnumerable<uint> permissionIds = permissionClaims.Select(c => Convert.ToUInt32(c.Value));

                // query db for get all permissions of roles and additional permissions
                //!TODO: Implement this function later
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating current permissions and roles.");
                return false;
            }
        }

        #endregion

        #region Helper Methods for Optional Operations

        private async Task<IActionResult> HelperValidateCookieLoginAsync(CancellationToken cancellationToken = default)
        {
            string userName = HttpContext.User.Identity?.Name ?? string.Empty;
            if (string.IsNullOrEmpty(userName))
            {
                _logger.LogWarning("Invalid cookie data: Username is empty.");
                return BadRequest("Invalid cookie data.");
            }

            ServiceResult<Account> cookieResult = await _accountServices.GetByUserNameAsync(userName, null, cancellationToken);
            if (cookieResult.IsSuccess && cookieResult.Data != null)
            {
                _logger.LogInformation("User {UserName} logged in successfully using cookies.", userName);
                return Ok(cookieResult.Data);
            }

            _logger.LogWarning("Cookie validation failed for user {UserName}.", userName);
            return BadRequest();
        }

        private async Task<IActionResult> HelperAccountLoginAsync(FELoginRequest account, CancellationToken cancellationToken = default)
        {
            try
            {
                ServiceResult<Account> result = await _accountServices.HandleGetAuthLoginAsync(account.Email.Trim(), account.Password.Trim(), true, false, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Login failed for user {Email}: {ErrorMessage}", account.Email, result.LogEntries?.LastOrDefault()?.Message);
                    return Unauthorized(result.LogEntries);
                }

                if (result.Data == null)
                {
                    _logger.LogWarning("Login failed for user {Email}: No user data returned.", account.Email);
                    return Unauthorized("Invalid login attempt.");
                }

                await SetUserClaimsAsync(result.Data, account.RememberMe);
                _logger.LogInformation("User {Email} logged in successfully.", account.Email);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login for user {Email}.", account.Email);
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion
    }
}