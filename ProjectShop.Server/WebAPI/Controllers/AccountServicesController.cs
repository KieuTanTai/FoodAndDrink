using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using System.Security.Claims;

namespace ProjectShop.Server.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountServicesController : ControllerBase
    {
        private readonly ILogger<AccountServicesController> _logger;
        private readonly ILogService _logService;
        private readonly ILoginServices<AccountModel, AccountNavigationOptions> _loginAccountServices;
        private readonly ISignupServices<AccountModel> _signupServices;
        private readonly IUpdateAccountServices _updateAccountStatusServices;
        private readonly ISearchAccountServices<AccountModel, AccountNavigationOptions> _searchAccountServices;

        public AccountServicesController(ILogger<AccountServicesController> logger,
            ILoginServices<AccountModel, AccountNavigationOptions> loginAccountServices,
            ISignupServices<AccountModel> signupServices,
            IUpdateAccountServices updateAccountStatusServices,
            ILogService logService,
            ISearchAccountServices<AccountModel, AccountNavigationOptions> searchAccountServices)
        {
            _logger = logger;
            _logService = logService;
            _loginAccountServices = loginAccountServices;
            _signupServices = signupServices;
            _updateAccountStatusServices = updateAccountStatusServices;
            _searchAccountServices = searchAccountServices;
        }

        // API to get account list
        [HttpGet(Name = "accounts")]
        public async Task<IActionResult> GetAccounts([FromQuery] bool? status, [FromQuery] AccountNavigationOptions? options, [FromQuery] int? maxCount)
        {
            _logger.LogInformation("Starting to retrieve account list.");
            //IEnumerable<AccountModel> accounts;
            ServiceResults<AccountModel> serviceResults = new ServiceResults<AccountModel>();
            try
            {
                if (maxCount.HasValue && maxCount.Value <= 0)
                {
                    _logger.LogWarning("maxCount must be greater than 0.");
                    return BadRequest("maxCount must be greater than 0.");
                }

                if (status.HasValue)
                    serviceResults = await _searchAccountServices.GetByStatusAsync(status.Value, options, maxCount);
                else
                    serviceResults = await _searchAccountServices.GetAllAsync(options, maxCount);
                return Ok(serviceResults); // return json to postman/browser
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\nError when retrieving account list.");
                return Ok(new List<AccountModel>());
            }

        }

        [HttpGet("by-status")]
        public async Task<IActionResult> GetByStatusAsync([FromQuery] bool status, [FromQuery] AccountNavigationOptions? options, [FromQuery] int? maxCount)
        {
            try
            {
                var result = await _searchAccountServices.GetByStatusAsync(status, options, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving accounts by status {status}");
                return StatusCode(500, "An error occurred while retrieving accounts by status.");
            }
        }

        [HttpGet("by-username")]
        public async Task<IActionResult> GetByUserNameAsync([FromQuery] string userName, [FromQuery] AccountNavigationOptions? options)
        {
            try
            {
                var result = await _searchAccountServices.GetByUserNameAsync(userName, options);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving account by username {userName}");
                return StatusCode(500, "An error occurred while retrieving account by username.");
            }
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetByAccountIdAsync([FromRoute] int accountId, [FromQuery] AccountNavigationOptions? options)
        {
            try
            {
                var result = await _searchAccountServices.GetByAccountIdAsync((uint)accountId, options);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving account by id {accountId}");
                return StatusCode(500, "An error occurred while retrieving account by id.");
            }
        }

        [HttpGet("created/by-month-year")]
        public async Task<IActionResult> GetByCreatedDateMonthAndYearAsync([FromQuery] int year, [FromQuery] int month,
            [FromQuery] AccountNavigationOptions? options, [FromQuery] int? maxCount)
        {
            try
            {
                var result = await _searchAccountServices.GetByCreatedDateMonthAndYearAsync(year, month, options, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving accounts created in {month}/{year}");
                return StatusCode(500, "An error occurred while retrieving accounts by created month/year.");
            }
        }

        [HttpGet("created/by-year")]
        public async Task<IActionResult> GetByCreatedYearAsync([FromQuery] int year, [FromQuery] ECompareType compareType,
            [FromQuery] AccountNavigationOptions? options, [FromQuery] int? maxCount)
        {
            try
            {
                var result = await _searchAccountServices.GetByCreatedYearAsync(year, compareType, options, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving accounts by created year {year} and compareType {compareType}");
                return StatusCode(500, "An error occurred while retrieving accounts by created year.");
            }
        }

        [HttpGet("created/by-date-range")]
        public async Task<IActionResult> GetByCreatedDateTimeRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
            [FromQuery] AccountNavigationOptions? options, [FromQuery] int? maxCount)
        {
            try
            {
                var result = await _searchAccountServices.GetByCreatedDateTimeRangeAsync(startDate, endDate, options, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving accounts created between {startDate} and {endDate}");
                return StatusCode(500, "An error occurred while retrieving accounts by created date range.");
            }
        }

        [HttpGet("created/by-date")]
        public async Task<IActionResult> GetByCreatedDateTimeAsync([FromQuery] DateTime dateTime, [FromQuery] ECompareType compareType,
            [FromQuery] AccountNavigationOptions? options, [FromQuery] int? maxCount)
        {
            try
            {
                var result = await _searchAccountServices.GetByCreatedDateTimeAsync(dateTime, compareType, options, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving accounts created at {dateTime} with compareType {compareType}");
                return StatusCode(500, "An error occurred while retrieving accounts by created date.");
            }
        }

        [HttpGet("updated/by-month-year")]
        public async Task<IActionResult> GetByLastUpdatedDateMonthAndYearAsync([FromQuery] int year, [FromQuery] int month,
            [FromQuery] AccountNavigationOptions? options, [FromQuery] int? maxCount)
        {
            try
            {
                var result = await _searchAccountServices.GetByLastUpdatedDateMonthAndYearAsync(year, month, options, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving accounts last updated in {month}/{year}");
                return StatusCode(500, "An error occurred while retrieving accounts by last updated month/year.");
            }
        }

        [HttpGet("updated/by-year")]
        public async Task<IActionResult> GetByLastUpdatedYearAsync([FromQuery] int year, [FromQuery] ECompareType compareType,
            [FromQuery] AccountNavigationOptions? options, [FromQuery] int? maxCount)
        {
            try
            {
                var result = await _searchAccountServices.GetByLastUpdatedYearAsync(year, compareType, options, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving accounts last updated in year {year} with compareType {compareType}");
                return StatusCode(500, "An error occurred while retrieving accounts by last updated year.");
            }
        }

        [HttpGet("updated/by-date-range")]
        public async Task<IActionResult> GetByLastUpdatedDateTimeRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
            [FromQuery] AccountNavigationOptions? options, [FromQuery] int? maxCount)
        {
            try
            {
                var result = await _searchAccountServices.GetByLastUpdatedDateTimeRangeAsync(startDate, endDate, options, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving accounts last updated between {startDate} and {endDate}");
                return StatusCode(500, "An error occurred while retrieving accounts by last updated date range.");
            }
        }

        [HttpGet("updated/by-date")]
        public async Task<IActionResult> GetByLastUpdatedDateTimeAsync([FromQuery] DateTime dateTime, [FromQuery] ECompareType compareType,
            [FromQuery] AccountNavigationOptions? options, [FromQuery] int? maxCount)
        {
            try
            {
                var result = await _searchAccountServices.GetByLastUpdatedDateTimeAsync(dateTime, compareType, options, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving accounts last updated at {dateTime} with compareType {compareType}");
                return StatusCode(500, "An error occurred while retrieving accounts by last updated date.");
            }
        }

        // API to get current account information
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentAccount([FromQuery] AccountNavigationOptions? options)
        {
            _logger.LogInformation("Starting to retrieve current account information.");
            string userName = GetUserNameByClaims();
            if (string.IsNullOrEmpty(userName))
            {
                _logger.LogWarning("Current account username not found.");
                return NotFound("Current account not found.");
            }

            try
            {
                ServiceResult<AccountModel> serviceResult = new();
                serviceResult = await _searchAccountServices.GetByUserNameAsync(userName, options);
                AccountModel currentAccount = serviceResult.Data!;
                _logger.LogInformation($"Current account information: {currentAccount.UserName}");
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when retrieving current account information.");
                return BadRequest("Failed to get current account.");
            }
        }

        // API to get current user's roles list
        [HttpGet("current-account-roles")]
        public async Task<IActionResult> GetCurrentAccountRoles()
        {
            _logger.LogInformation("Starting to retrieve current user's roles list.");
            string userName = GetUserNameByClaims();
            if (string.IsNullOrEmpty(userName))
            {
                _logger.LogWarning("Current account username not found.");
                return NotFound("Current account not found.");
            }

            try
            {
                ServiceResult<AccountModel> serviceResult = new ServiceResult<AccountModel>();
                AccountNavigationOptions options = new() { IsGetRolesOfUsers = true };
                serviceResult = await _searchAccountServices.GetByUserNameAsync(userName, options);
                AccountModel? account = serviceResult.Data!;
                IEnumerable<RolesOfUserModel> roles = account.RolesOfUsers;
                _logger.LogInformation($"Number of current user's roles: {roles.Count()}");
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when retrieving current user's roles list.");
                return BadRequest("Failed to get current account roles.");
            }
        }

        // API for login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] FELoginRequest request, [FromQuery] AccountNavigationOptions? options)
        {
            _logService.LogInfo<AccountModel, AccountServicesController>("Starting login process.");
            ServiceResult<AccountModel> serviceResult = new ServiceResult<AccountModel>();

            try
            {
                // validate cookie here if needed
                if (Request.Cookies.ContainsKey(".AspNetCore.Cookies"))
                    serviceResult = await _searchAccountServices.GetByUserNameAsync(GetUserNameByClaims());
                else
                    serviceResult = await _loginAccountServices.HandleLoginAsync(request.Email, request.Password, options);

                if (serviceResult.Data == null || string.IsNullOrEmpty(serviceResult.Data.UserName)
                    || serviceResult.Data.AccountId <= 0)
                {
                    _logService.LogWarning<AccountModel, AccountServicesController>($"Login failed. {request.Email}");
                    return BadRequest("Login failed. Invalid username or password.");
                }

                AccountModel account = serviceResult.Data!;
                // Set claims principal for the current user
                await SetUserClaims(account, request);
                _logService.LogInfo<AccountModel, AccountServicesController>($"Login successful. {account.UserName}");
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                _logService.LogError<AccountModel, AccountServicesController>("Error during login.", ex);
                return BadRequest("Login failed.");
            }
        }

        // API for multi signup (migrate data from other platforms)
        [HttpPost("multi-signup")]
        public async Task<IActionResult> MultiSignup([FromBody] FEMigrateAccountRequest request)
        {
            _logger.LogInformation("Starting multi-signup process.");
            //DEBUG: MOCK TEST ONLY, REMOVE IN PRODUCTION
            if (request.AdminKey != "admin")
            {
                _logger.LogWarning("Invalid admin key for multi-signup.");
                return Unauthorized("Invalid admin key.");
            }

            //END DEBUG
            if (request == null || request.Accounts == null || !request.Accounts.Any())
            {
                _logService.LogWarning<AccountModel, AccountServicesController>("Invalid multi-signup data.");
                return BadRequest("Invalid multi-signup data.");
            }

            try
            {
                ServiceResults<AccountModel> serviceResults = new ServiceResults<AccountModel>();
                serviceResults = await _signupServices.AddAccountsAsync(request.Accounts);
                if (serviceResults.Data == null || !serviceResults.Data.Any())
                {
                    _logService.LogWarning<AccountModel, AccountServicesController>("Multi-signup failed.");
                    return BadRequest("Multi-signup failed.");
                }

                _logger.LogInformation($"Multi-signup successful. Number of accounts created: {serviceResults.Data.Count()}");
                return Ok(serviceResults);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during multi-signup.");
                return StatusCode(500, "Internal server error during multi-signup.");
            }
        }

        // API for signup
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] RegisterRequest request)
        {
            _logger.LogInformation("Starting signup process.");

            // Validate input
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                _logService.LogWarning<AccountModel, AccountServicesController>("Invalid signup data.");
                return BadRequest("Invalid signup data.");
            }

            try
            {
                ServiceResult<AccountModel> serviceResult = new ServiceResult<AccountModel>();
                serviceResult = await _signupServices.AddAccountAsync(new AccountModel(request.Email, request.Password));

                if (serviceResult.Data == null || string.IsNullOrEmpty(serviceResult.Data.UserName) || !serviceResult.IsSuccess)
                {
                    _logService.LogWarning<AccountModel, AccountServicesController>($@"Signup failed. {serviceResult.Data}, 
                        {serviceResult.Data?.UserName}, {serviceResult.Data?.Password}");
                    if (serviceResult.Data != null)
                        serviceResult.Data.Password = "";
                    return BadRequest(serviceResult);
                }

                _logger.LogInformation($"Signup successful. {request.Email}");
                // Could return CreatedAtAction if desired, using Ok for simplicity
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during signup.");
                return StatusCode(500, "Internal server error during signup.");
            }
        }

        // TODO: API to update account status and related customer or employee status
        [HttpPatch("update-status")]
        public async Task<IActionResult> UpdateAccountStatus([FromBody] AccountUpdateStatusRequest request)
        {
            _logger.LogInformation("Starting account status update process.");
            if (request.AccountId <= 0)
            {
                _logger.LogWarning("Invalid AccountId.");
                return BadRequest("Invalid AccountId.");
            }

            try
            {
                JsonLogEntry logEntry = await _updateAccountStatusServices.UpdateAccountStatusAsync(request.AccountId, request.Status);
                _logger.LogInformation($"Status update successful. AccountId: {request.AccountId}, New Status: {request.Status}");
                return Ok(logEntry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when updating account status.");
                return BadRequest("Update status failed.");
            }
        }

        // API to update account password
        [HttpPatch("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] AccountUpdatePasswordRequest request)
        {
            _logger.LogInformation("Starting account password update process.");
            if (request.AccountId <= 0 || string.IsNullOrEmpty(request.NewPassword))
            {
                _logger.LogWarning("Invalid AccountId or NewPassword.");
                return BadRequest("Invalid AccountId or NewPassword.");
            }

            try
            {
                JsonLogEntry logEntry = await _updateAccountStatusServices.UpdateAccountPasswordAsync(request.AccountId, request.NewPassword);
                _logger.LogInformation($"Password update successful. AccountId: {request.AccountId}");
                return Ok(logEntry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when updating account password.");
                return BadRequest("Update password failed.");
            }
        }

        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("Starting logout process.");
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                _logger.LogInformation("Logout successful.");
                return Ok("Logout successful.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout.");
                _logService.LogError<AccountModel, AccountServicesController>("error when fetch!", ex);
                return StatusCode(500, "Internal server error during logout.");
            }
        }

        [HttpDelete("logout-all")]
        public async Task<IActionResult> LogoutAll()
        {
            _logger.LogInformation("Starting logout from all sessions process.");
            try
            {
                // Delete all cookies (if any)
                foreach (var cookie in Request.Cookies.Keys)
                    Response.Cookies.Delete(cookie);

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                _logger.LogInformation("Logout from all sessions successful.");
                return Ok("Logout from all sessions successful.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout from all sessions.");
                return StatusCode(500, "Internal server error during logout from all sessions.");
            }
        }

        // TODO: API to disable account and related customer or employee
        [HttpDelete("disable-account/{accountId}")]
        public async Task<IActionResult> DisableAccount([FromRoute] int accountId, [FromBody] AccountUpdateRelativeRequest request)
        {
            _logger.LogInformation("Starting account disabling process.");
            if (accountId <= 0)
            {
                _logger.LogWarning("Invalid AccountId.");
                return BadRequest("Invalid AccountId.");
            }

            try
            {
                JsonLogEntry logEntry = await _updateAccountStatusServices.UpdateAccountStatusAsync((uint)accountId, false);
                _logger.LogInformation($"Account disabling successful. AccountId: {accountId}");
                if (request.IsUpdateCustomer)
                {
                    _logger.LogInformation($"Updating related customer status for AccountId: {accountId}");
                }

                else if (request.IsUpdateEmployee)
                {
                    // Call service to update related employee status
                    _logger.LogInformation($"Updating related employee status for AccountId: {accountId}");
                    // await _updateEmployeeStatusServices.UpdateEmployeeStatusByAccountIdAsync((uint)accountId, false);
                }

                else if (request.IsUpdateCustomer && request.IsUpdateEmployee)
                    _logger.LogError("Both IsUpdateCustomer and IsUpdateEmployee cannot be true at the same time.");
                return Ok(logEntry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when disabling account.");
                return BadRequest("Disable account failed.");
            }
        }

        // API for testing OPTIONS on Postman
        [HttpOptions]
        public IActionResult Options()
        {
            _logger.LogInformation("OPTIONS request received.");
            return Ok();
        }

        // API for testing HEAD on Postman
        [HttpHead]
        public IActionResult Head()
        {
            _logger.LogInformation("HEAD request received.");
            return Ok();
        }

        private static ClaimsPrincipal BuildClaimsPrincipal(AccountModel account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.UserName ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
            };
            foreach (var role in account.RolesOfUsers)
                claims.Add(new Claim(ClaimTypes.Role, role.RoleId.ToString()));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }

        private async Task SetUserClaims(AccountModel account, FELoginRequest request)
        {
            ClaimsPrincipal principal = BuildClaimsPrincipal(account);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = request.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
                    AllowRefresh = true
                });
        }

        private string GetUserNameByClaims()
        {
            var userNameClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
            return userNameClaim?.Value ?? string.Empty;
        }
    }
}
