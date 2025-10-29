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

        #region HttpPost Actions
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] FELoginRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                return BadRequest("Request body is null.");
            if (string.IsNullOrEmpty(request.Email.Trim()) || string.IsNullOrEmpty(request.Password.Trim()))
                return BadRequest("Email and Password are required.");

            try
            {
                ServiceResult<Account> result = await _accountServices.HandleGetAuthLoginAsync(request.Email.Trim(), request.Password.Trim(), true, false, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Login failed for user {Email}: {ErrorMessage}", request.Email, result.LogEntries?.LastOrDefault()?.Message);
                    return Unauthorized(result.LogEntries);
                }

                if (result.Data == null)
                {
                    _logger.LogWarning("Login failed for user {Email}: No user data returned.", request.Email);
                    return Unauthorized("Invalid login attempt.");
                }

                await SetUserClaimsAsync(result.Data, request.RememberMe);
                _logger.LogInformation("User {Email} logged in successfully.", request.Email);
                return Ok(result.Data);
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

        #region Helper Methods
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

        #endregion
    }
}