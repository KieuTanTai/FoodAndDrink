using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using System.Security.Claims;

namespace ProjectShop.Server.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountServicesController : ControllerBase
    {
        private readonly ILogger<AccountServicesController> _logger;
        private readonly ILoginServices<AccountModel, AccountNavigationOptions> _loginAccountServices;
        private readonly ISignupServices<AccountModel> _signupServices;
        private readonly IUpdateAccountServices _updateAccountStatusServices;
        private readonly ISearchAccountServices<AccountModel, AccountNavigationOptions> _searchAccountServices;

        public AccountServicesController(ILogger<AccountServicesController> logger,
            ILoginServices<AccountModel, AccountNavigationOptions> loginAccountServices,
            ISignupServices<AccountModel> signupServices,
            IUpdateAccountServices updateAccountStatusServices,
            ISearchAccountServices<AccountModel, AccountNavigationOptions> searchAccountServices)
        {
            _logger = logger;
            _loginAccountServices = loginAccountServices;
            _signupServices = signupServices;
            _updateAccountStatusServices = updateAccountStatusServices;
            _searchAccountServices = searchAccountServices;
        }

        // API test lấy danh sách account
        [HttpGet(Name = "accounts")]
        public async Task<IActionResult> GetAccounts([FromQuery] bool? status, [FromQuery] AccountNavigationOptions? options, [FromQuery] int? maxCount)
        {
            _logger.LogInformation("Bắt đầu lấy danh sách account.");
            //IEnumerable<AccountModel> accounts;
            ServiceResults<AccountModel> serviceResults = new ServiceResults<AccountModel>();
            try
            {
                if (maxCount.HasValue && maxCount.Value <= 0)
                {
                    _logger.LogWarning("maxCount phải lớn hơn 0.");
                    return BadRequest("maxCount must be greater than 0.");
                }

                if (status.HasValue)
                    serviceResults = await _searchAccountServices.GetByStatusAsync(status.Value, options, maxCount);
                else
                    serviceResults = await _searchAccountServices.GetAllAsync(options, maxCount);
                return Ok(serviceResults); // trả json ra postman/browser
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\nLỗi khi lấy danh sách tài khoản.");
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
                ServiceResult<AccountModel> serviceResult = new ServiceResult<AccountModel>();
                serviceResult = await _searchAccountServices.GetByUserNameAsync(userName, options);
                AccountModel currentAccount = serviceResult.Data!;
                _logger.LogInformation($"Thông tin tài khoản hiện tại: {currentAccount.UserName}");
                return Ok(serviceResult);
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
                ServiceResult<AccountModel> serviceResult = new ServiceResult<AccountModel>();
                AccountNavigationOptions options = new() { IsGetRolesOfUsers = true };
                serviceResult = await _searchAccountServices.GetByUserNameAsync(userName, options);
                AccountModel? account = serviceResult.Data!;
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

        // TODO: API test lấy danh sách account và roles
        // API test đăng nhập
        [HttpPost(Name = "login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, [FromQuery] AccountNavigationOptions? options)
        {
            _logger.LogInformation("Bắt đầu quá trình đăng nhập.");
            try
            {
                ServiceResult<AccountModel> serviceResult = new ServiceResult<AccountModel>();
                serviceResult = await _loginAccountServices.HandleLoginAsync(request.Email, request.Password, options);
                AccountModel account = serviceResult.Data!;
                // Set claims principal for the current user
                ClaimsPrincipal principal = BuildClaimsPrincipal(account);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                _logger.LogInformation($"Đăng nhập thành công. {account.UserName}");
                return Ok(serviceResult);
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
                ServiceResult<AccountModel> serviceResult = new ServiceResult<AccountModel>();
                serviceResult = await _signupServices.AddAccountAsync(new AccountModel(request.Email, request.Password));
                uint result = serviceResult.Data!.AccountId;
                _logger.LogInformation($"Đăng ký thành công. {request.Email}");
                // Có thể trả về CreatedAtAction nếu muốn, ở đây dùng Ok cho đơn giản
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đăng ký.");
                return StatusCode(500, "Internal server error during signup.");
            }
        }

        // API test cập nhật trạng thái tài khoản
        [HttpPatch("update-status")]
        public async Task<IActionResult> UpdateAccountStatus([FromBody] AccountUpdateStatusRequest request)
        {
            _logger.LogInformation("Bắt đầu quá trình cập nhật trạng thái tài khoản.");
            if (request.AccountId <= 0)
            {
                _logger.LogWarning("AccountId không hợp lệ.");
                return BadRequest("Invalid AccountId.");
            }

            try
            {
                JsonLogEntry logEntry = await _updateAccountStatusServices.UpdateAccountStatusAsync(request.AccountId, request.Status);
                _logger.LogInformation($"Cập nhật trạng thái thành công. AccountId: {request.AccountId}, New Status: {request.Status}");
                return Ok(logEntry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật trạng thái tài khoản.");
                return BadRequest("Update status failed.");
            }
        }

        // API test cập nhật mật khẩu tài khoản
        [HttpPatch("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] AccountUpdatePasswordRequest request)
        {
            _logger.LogInformation("Bắt đầu quá trình cập nhật mật khẩu tài khoản.");
            if (request.AccountId <= 0 || string.IsNullOrEmpty(request.NewPassword))
            {
                _logger.LogWarning("AccountId hoặc NewPassword không hợp lệ.");
                return BadRequest("Invalid AccountId or NewPassword.");
            }

            try
            {
                JsonLogEntry logEntry = await _updateAccountStatusServices.UpdateAccountPasswordAsync(request.AccountId, request.NewPassword);
                _logger.LogInformation($"Cập nhật mật khẩu thành công. AccountId: {request.AccountId}");
                return Ok(logEntry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật mật khẩu tài khoản.");
                return BadRequest("Update password failed.");
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
