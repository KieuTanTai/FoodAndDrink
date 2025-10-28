using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        //? POST
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] FELoginRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                return BadRequest("Request body is null.");
            if (string.IsNullOrEmpty(request.Email.Trim()) || string.IsNullOrEmpty(request.Password.Trim()))
                return BadRequest("Email and Password are required.");

            try
            {
                ServiceResult<Account> result = await _accountServices.HandleLoginAsync(request.Email.Trim(), request.Password.Trim(), null, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Login failed for user {Email}: {ErrorMessage}", request.Email, result.LogEntries?.LastOrDefault()?.Message);
                    return Unauthorized(result.LogEntries);
                }

                ClaimsPrincipal claimsPrincipal = CreateClaimsPrincipalAsync(result.Data!);
                HttpContext.User = claimsPrincipal;
                _logger.LogInformation("User {Email} logged in successfully.", request.Email);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login.");
                return StatusCode(500, "Internal server error");
            }
        }

        

        //? Helper Method to Create ClaimsPrincipal
        private static ClaimsPrincipal CreateClaimsPrincipalAsync(Account account)
        {
            List<Claim> claims =
            [
                new (ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                new (ClaimTypes.Name, account.UserName),
            ];

            ClaimsIdentity identity = new(claims, "LoggedInUser");
            return new(identity);
        }
    }
}