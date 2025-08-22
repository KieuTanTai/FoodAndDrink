using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.ObjectValue;
using ProjectShop.Server.Core.ObjectValue.GetNavigationPropertyOptions;
using System.Security.Claims;

namespace ProjectShop.Server.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountServicesController : ControllerBase
    {
        private readonly ILogger<AccountServicesController> _logger;
        private readonly ILoginService<AccountModel, AccountNavigationOptions> _loginAccountService;
        private readonly ISignupService<AccountModel> _signupService;
        private readonly IUpdateAccountService _updateAccountStatusService;
        private readonly ISearchAccountService<AccountModel, AccountNavigationOptions> _searchAccountService;

        public AccountServicesController(ILogger<AccountServicesController> logger, ILoginService<AccountModel, AccountNavigationOptions> loginAccountService,
                ISignupService<AccountModel> signupService, IUpdateAccountService updateAccountStatusService, ISearchAccountService<AccountModel, AccountNavigationOptions> searchAccountService)
        {
            _logger = logger;
            _loginAccountService = loginAccountService;
            _signupService = signupService;
            _updateAccountStatusService = updateAccountStatusService;
            _searchAccountService = searchAccountService;
        }

        // API test lấy danh sách account
        [HttpGet(Name = "accounts")]
        public async Task<IActionResult> GetAccounts([FromQuery] int? maxCount, [FromQuery] bool? status, [FromQuery] AccountNavigationOptions? options)
        {
            _logger.LogInformation("Bắt đầu lấy danh sách account.");
            IEnumerable<AccountModel> accounts;
            try
            {
                if (maxCount.HasValue && maxCount.Value <= 0)
                {
                    _logger.LogWarning("maxCount phải lớn hơn 0.");
                    return BadRequest("maxCount must be greater than 0.");
                }

                if (status.HasValue)
                    accounts = await _searchAccountService.GetByStatusAsync(status.Value, options, maxCount);
                else
                    accounts = await _searchAccountService.GetAllAsync(options, maxCount);
                return Ok(accounts); // trả json ra postman/browser
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\nLỗi khi lấy danh sách tài khoản.");
                return Ok(new List<AccountModel>());
            }

        }

        // TODO: API test lấy danh sách account và roles
        // API test đăng nhập
        [HttpPost(Name = "login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, [FromQuery] AccountNavigationOptions? options)
        {
            _logger.LogInformation("Bắt đầu quá trình đăng nhập.");
            try
            {
                AccountModel? account = await _loginAccountService.HandleLoginAsync(request.Email, request.Password, options);
                // Set claims principal for the current user
                ClaimsPrincipal principal = BuildClaimsPrincipal(account);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                _logger.LogInformation($"Đăng nhập thành công. {account.UserName}");
                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đăng nhập.");
                return BadRequest("Login failed.");
            }
        }

        // API test đăng ký
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] RegisterRequest request)
        {
            _logger.LogInformation("Bắt đầu quá trình đăng ký.");

            // Validate đầu vào
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                _logger.LogWarning("Dữ liệu đăng ký không hợp lệ.");
                return BadRequest("Invalid signup data.");
            }

            try
            {
                int result = await _signupService.AddAccountAsync(new AccountModel(request.Email, request.Password));
                _logger.LogInformation($"Đăng ký thành công. {request.Email}");
                // Có thể trả về CreatedAtAction nếu muốn, ở đây dùng Ok cho đơn giản
                return Ok(new { AccountId = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đăng ký.");
                return StatusCode(500, "Internal server error during signup.");
            }
        }

        // API test cập nhật trạng thái tài khoản
        [HttpPatch("update-status")]
        public async Task<IActionResult> UpdateAccountStatus([FromBody] UpdateAccountStatusRequest request)
        {
            _logger.LogInformation("Bắt đầu quá trình cập nhật trạng thái tài khoản.");
            if (request.AccountId <= 0)
            {
                _logger.LogWarning("AccountId không hợp lệ.");
                return BadRequest("Invalid AccountId.");
            }

            try
            {
                int result = await _updateAccountStatusService.UpdateAccountStatusAsync(request.AccountId, request.Status);
                _logger.LogInformation($"Cập nhật trạng thái tài khoản thành công. AccountId: {request.AccountId}, Status: {request.Status}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật trạng thái tài khoản.");
                return BadRequest("Update status failed.");
            }
        }

        // API test cập nhật mật khẩu tài khoản
        [HttpPatch("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateAccountPasswordRequest request)
        {
            _logger.LogInformation("Bắt đầu quá trình cập nhật mật khẩu tài khoản.");
            if (request.AccountId <= 0 || string.IsNullOrEmpty(request.NewPassword))
            {
                _logger.LogWarning("AccountId hoặc NewPassword không hợp lệ.");
                return BadRequest("Invalid AccountId or NewPassword.");
            }

            try
            {
                int result = await _updateAccountStatusService.UpdateAccountPasswordAsync(request.AccountId, request.NewPassword);
                _logger.LogInformation($"Cập nhật mật khẩu thành công. AccountId: {request.AccountId}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật mật khẩu tài khoản.");
                return BadRequest("Update password failed.");
            }
        }

        // API test lấy thông tin tài khoản hiện tại
        [HttpGet("current-account")]
        public async Task<IActionResult> GetCurrentAccount([FromQuery] AccountNavigationOptions? options)
        {
            _logger.LogInformation("Bắt đầu lấy thông tin tài khoản hiện tại.");
            string userName = GetUserNameByClaims();
            if (string.IsNullOrEmpty(userName))
            {
                _logger.LogWarning("Không tìm thấy tên đăng nhập của tài khoản hiện tại.");
                return NotFound("Current account not found.");
            }

            try
            {
                AccountModel? currentAccount = await _searchAccountService.GetByUserNameAsync(userName, options);
                _logger.LogInformation($"Thông tin tài khoản hiện tại: {currentAccount.UserName}");
                return Ok(currentAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông tin tài khoản hiện tại.");
                return BadRequest("Failed to get current account.");
            }
        }

        // API test lấy danh sách quyền của người dùng hiện tại
        [HttpGet("current-account-roles")]
        public async Task<IActionResult> GetCurrentAccountRoles()
        {
            _logger.LogInformation("Bắt đầu lấy danh sách quyền của người dùng hiện tại.");
            string userName = GetUserNameByClaims();
            if (string.IsNullOrEmpty(userName))
            {
                _logger.LogWarning("Không tìm thấy tên đăng nhập của tài khoản hiện tại.");
                return NotFound("Current account not found.");
            }

            try
            {
                AccountNavigationOptions options = new() { IsGetRolesOfUsers = true };
                AccountModel? account = await _searchAccountService.GetByUserNameAsync(userName, options);
                IEnumerable<RolesOfUserModel> roles = account.RolesOfUsers;
                _logger.LogInformation($"Số lượng quyền của người dùng hiện tại: {roles.Count()}");
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách quyền của người dùng hiện tại.");
                return BadRequest("Failed to get current account roles.");
            }
        }

        // API test lấy thông tin tài khoản theo id
        [HttpGet("account-by-id/{accountId}")]
        public async Task<IActionResult> GetAccountById([FromQuery] uint accountId, [FromQuery] AccountNavigationOptions? options)
        {
            _logger.LogInformation($"Bắt đầu lấy thông tin tài khoản với AccountId: {accountId}");
            if (accountId <= 0)
            {
                _logger.LogWarning("AccountId không hợp lệ.");
                return BadRequest("Invalid AccountId.");
            }

            try
            {
                AccountModel? currentAccount = await _searchAccountService.GetByAccountIdAsync(accountId, options);
                _logger.LogInformation($"Thông tin tài khoản hiện tại: {currentAccount.UserName}");
                return Ok(currentAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông tin tài khoản theo id.");
                return BadRequest("Failed to get account by id.");
            }
        }

        // API test OPTIONS trên Postman
        [HttpOptions]
        public IActionResult Options()
        {
            _logger.LogInformation("OPTIONS request received.");
            return Ok();
        }

        // API test HEAD trên Postman
        [HttpHead]
        public IActionResult Head()
        {
            _logger.LogInformation("HEAD request received.");
            return Ok();
        }


        // BUG: JUST FOR TEST / NOT USE ON PRODUCTION

        // API test lấy thông tin tài khoản theo tên đăng nhập
        [HttpGet("account-by-username/{username}")]
        public async Task<IActionResult> GetAccountByUsername(string username, [FromQuery] AccountNavigationOptions? options)
        {
            _logger.LogInformation($"Bắt đầu lấy thông tin tài khoản với tên đăng nhập: {username}");
            if (string.IsNullOrEmpty(username))
            {
                _logger.LogWarning("Tên đăng nhập không hợp lệ.");
                return BadRequest("Invalid username.");
            }

            try
            {
                AccountModel? account = await _searchAccountService.GetByUserNameAsync(username, options);
                _logger.LogInformation($"Thông tin tài khoản: {account.UserName}");
                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông tin tài khoản theo tên đăng nhập.");
                return BadRequest("Failed to get account by username.");
            }
        }

        // API test lấy danh sách tài khoản theo trạng thái
        [HttpGet("accounts-by-status/{status}")]
        public async Task<IActionResult> GetAccountsByStatus(bool status, [FromQuery] AccountNavigationOptions? options, [FromQuery] int? maxCount)
        {
            _logger.LogInformation($"Bắt đầu lấy danh sách tài khoản với trạng thái: {status}");

            try
            {
                IEnumerable<AccountModel> accounts = await _searchAccountService.GetByStatusAsync(status, options, maxCount);
                _logger.LogInformation($"Số lượng tài khoản với trạng thái {status}: {accounts.Count()}");
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách tài khoản theo trạng thái.");
                return BadRequest("Failed to get accounts by status.");
            }
        }

        private static ClaimsPrincipal BuildClaimsPrincipal(AccountModel account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, account.UserName),
                    new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                };
            foreach (var role in account.RolesOfUsers)
                claims.Add(new Claim(ClaimTypes.Role, role.RoleId.ToString())); // Dùng tên role

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }

        private string GetUserNameByClaims()
        {
            var userNameClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
            return userNameClaim?.Value ?? string.Empty;
        }
    }
}
