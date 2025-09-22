using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IEntities;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using System.Security.Claims;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Account
{
    public class LoginService : ILoginServices<AccountModel, AccountNavigationOptions>
    {
        private readonly IAccountDAO<AccountModel> _accountDAO;
        private readonly IHashPassword _hashPassword;
        private readonly IBaseGetNavigationPropertyServices<AccountModel, AccountNavigationOptions> _navigationService;
        private readonly IServiceResultFactory<LoginService> _serviceResultFactory;
        private readonly ILogService _logger;

        public LoginService(IAccountDAO<AccountModel> accountDAO, IHashPassword hashPassword,
            IBaseGetNavigationPropertyServices<AccountModel, AccountNavigationOptions> navigationService,
            IServiceResultFactory<LoginService> serviceResultFactory, ILogService logger)
        {
            _logger = logger;
            _accountDAO = accountDAO;
            _hashPassword = hashPassword;
            _navigationService = navigationService;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResult<AccountModel>> HandleLoginAsync(string userName, string password, AccountNavigationOptions? options)
        {
            ServiceResult<AccountModel> result = new(true);
            try
            {
                AccountModel? account = await _accountDAO.GetByUserNameAsync(userName);
                if (account == null)
                    return _serviceResultFactory.CreateServiceResult($"Account with username '{userName}' not found.", new AccountModel(), false);
                // validate account status and password
                if (!account.AccountStatus)
                    return _serviceResultFactory.CreateServiceResult($"Account is inactive.", new AccountModel(), false);
                string accountPassword = account.Password;
                if (!await _hashPassword.ComparePasswords(accountPassword, password))
                    return _serviceResultFactory.CreateServiceResult($"Invalid password.", new AccountModel(), false);

                if (options != null)
                    result = await _navigationService.GetNavigationPropertyByOptionsAsync(account, options);

                result.LogEntries = result.LogEntries!.Append(_logger.JsonLogInfo<AccountModel, LoginService>($"Login successful for user: {userName}"));
                return result;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult("An error occurred during login.", new AccountModel(), false, ex);
            }
        }
    }
}
