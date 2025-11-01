using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MySqlX.XDevAPI.Common;
using OpenQA.Selenium.Interactions;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

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