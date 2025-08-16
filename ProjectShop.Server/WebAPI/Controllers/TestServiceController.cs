using Microsoft.AspNetCore.Mvc;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Infrastructure.Services;

namespace ProjectShop.Server.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TestServiceController : ControllerBase
    {

        private readonly ILogger<TestServiceController> _logger;
        private readonly ISearchAccountService<AccountModel> _searchAccountService = GetProviderService.SystemServices.GetRequiredService<ISearchAccountService<AccountModel>>();

        public TestServiceController(ILogger<TestServiceController> logger)
        {
            _logger = logger;
        }

        // API test lấy danh sách account
        [HttpGet(Name = "accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            _logger.LogInformation("Bắt đầu lấy danh sách account.");

            IEnumerable<AccountModel> accounts = await _searchAccountService.GetAllAsync();

            _logger.LogInformation("Đã lấy {Count} account từ database.", accounts.Count());

            IEnumerable<AccountModel> filteredAccounts = accounts.Take(30);

            foreach (var account in filteredAccounts)
            {
                _logger.LogInformation(
                    "Account: {UserName}, Created: {Created}, Last Updated: {Updated}, Status: {Status}",
                    account.UserName,
                    account.AccountCreatedDate,
                    account.AccountLastUpdatedDate,
                    account.AccountStatus
                );
            }

            _logger.LogInformation("Kết thúc quá trình lấy account.");

            return Ok(accounts); // trả json ra postman/browser
        }

    }
}
