using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IEntities;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
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

        public LoginService(IAccountDAO<AccountModel> accountDAO, IHashPassword hashPassword,
            IBaseGetNavigationPropertyService<AccountModel, AccountNavigationOptions> navigationService
        )
        {
            _accountDAO = accountDAO;
            _hashPassword = hashPassword;
            _navigationService = navigationService;
        }

        public async Task<AccountModel> HandleLoginAsync(string userName, string password, AccountNavigationOptions? options)
        {
            try
            {
                AccountModel? account = await _accountDAO.GetByUserNameAsync(userName) ?? throw new InvalidOperationException("Account not found.");
                // validate account status and password
                if (!account.AccountStatus)
                    throw new InvalidOperationException("Account is inactive.");
                string accountPassword = account.Password;
                if (!await _hashPassword.ComparePasswords(accountPassword, password))
                    throw new InvalidOperationException("Incorrect password.");
                if (options != null)
                    account = await _navigationService.GetNavigationPropertyByOptionsAsync(account, options);
                //_currentAccountLogin.SetAccount(account);
                return account;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred during login.", ex);
            }
        }
    }
}
