using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using System.Security.Claims;

namespace ProjectShop.Server.Application.Services.AccountServices
{
    public class LoginService : ILoginServices<Account, AccountNavigationOptions>
    {
        private readonly IDBContext _dbContext;
        private readonly IHashPassword _hashPassword;
        private readonly IBaseGetNavigationPropertyServices<Account, AccountNavigationOptions> _navigationService;
        private readonly IServiceResultFactory<LoginService> _serviceResultFactory;
        private readonly ILogService _logger;

        public LoginService(IDBContext dbContext, IHashPassword hashPassword,
            IBaseGetNavigationPropertyServices<Account, AccountNavigationOptions> navigationService,
            IServiceResultFactory<LoginService> serviceResultFactory,
            ILogService logger)
        {
            _dbContext = dbContext;
            _hashPassword = hashPassword;
            _navigationService = navigationService;
            _serviceResultFactory = serviceResultFactory;
            _logger = logger;
        }

        public async Task<ServiceResult<Account>> HandleLoginAsync(string userName, string password, AccountNavigationOptions? options)
        {
            ServiceResult<Account> result = new(true);
            try
            {
                Account? account = await _dbContext.Set<Account>().FirstOrDefaultAsync(a => a.UserName == userName);
                if (account == null)
                    return _serviceResultFactory.CreateServiceResult($"Account with username '{userName}' not found.", new Account(), false);
                // validate account status and password
                if (!account.AccountStatus)
                    return _serviceResultFactory.CreateServiceResult($"Account is inactive.", new Account(), false);
                string accountPassword = account.Password;
                if (!await _hashPassword.ComparePasswords(accountPassword, password))
                    return _serviceResultFactory.CreateServiceResult($"Invalid password.", new Account(), false);

                if (options != null)
                    result = await _navigationService.GetNavigationPropertyByOptionsAsync(account, options);

                result.LogEntries = result.LogEntries!.Append(_logger.JsonLogInfo<Account, LoginService>($"Login successful for user: {userName}"));
                return result;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult("An error occurred during login.", new Account(), false, ex);
            }
        }
    }
}
