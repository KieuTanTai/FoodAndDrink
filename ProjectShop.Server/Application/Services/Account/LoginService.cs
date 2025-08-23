using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IEntities;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;
using ProjectShop.Server.Core.ObjectValue.GetNavigationPropertyOptions;
using System.Security.Claims;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Account
{
    public class LoginService : ILoginService<AccountModel, AccountNavigationOptions>
    {
        private readonly IAccountDAO<AccountModel> _accountDAO;
        private readonly IHashPassword _hashPassword;
        private readonly IBaseGetNavigationPropertyService<AccountModel, AccountNavigationOptions> _navigationService;
        private readonly IServiceResultFactory<LoginService> _serviceResultFactory;
        private readonly ILogService _logger;

        public LoginService(IAccountDAO<AccountModel> accountDAO, IHashPassword hashPassword,
            IBaseGetNavigationPropertyService<AccountModel, AccountNavigationOptions> navigationService,
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
            ServiceResult<AccountModel> result = new ServiceResult<AccountModel>();
            try
            {
                AccountModel? account = await _accountDAO.GetByUserNameAsync(userName) ?? throw new InvalidOperationException("Account not found.");
                // validate account status and password
                if (!account.AccountStatus)
                    return _serviceResultFactory.CreateServiceResult<AccountModel>($"Account is inactive.", new AccountModel(), false);
                string accountPassword = account.Password;
                if (!await _hashPassword.ComparePasswords(accountPassword, password))
                    return _serviceResultFactory.CreateServiceResult<AccountModel>($"Invalid password.", new AccountModel(), false);

                if (options != null)
                    result = await _navigationService.GetNavigationPropertyByOptionsAsync(account, options);
                result.LogEntries = result.LogEntries!.Append(_logger.JsonLogInfo<AccountModel, LoginService>($"Login successful for user: {userName}"));
                return result;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult<AccountModel>("An error occurred during login.", new AccountModel(), false, ex);
            }
        }
    }
}
