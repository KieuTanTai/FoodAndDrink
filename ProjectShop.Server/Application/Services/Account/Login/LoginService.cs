using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IEntities;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount.ILogin;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Account.Login
{
    public class LoginService : ValidateService<AccountModel>, ILoginService<AccountModel>
    {
        private readonly IDAO<AccountModel> _baseDAO;
        private readonly IAccountDAO<AccountModel> _accountDAO;
        private readonly ICurrentAccountLogin<AccountModel, RolesOfUserModel> _currentAccountLogin;

        public LoginService(
            IDAO<AccountModel> baseDAO,
            IAccountDAO<AccountModel> accountDAO,
            ICurrentAccountLogin<AccountModel, RolesOfUserModel> currentAccountLogin)
        {
            _baseDAO = baseDAO ?? throw new ArgumentNullException(nameof(baseDAO), "Base DAO cannot be null.");
            _accountDAO = accountDAO ?? throw new ArgumentNullException(nameof(accountDAO), "Account DAO cannot be null.");
            _currentAccountLogin = currentAccountLogin ?? throw new ArgumentNullException(nameof(currentAccountLogin), "Current account login cannot be null.");
        }

        public async Task<AccountModel> HandleLoginAsync(string userName, string password)
        {
            try
            {
                AccountModel? account = await _accountDAO.GetByUserNameAsync(userName) ?? throw new InvalidOperationException("Account not found.");
                // validate account status and password
                if (!account.AccountStatus)
                    throw new InvalidOperationException("Account is inactive.");
                string accountPassword = account.Password;
                if (!await hashPassword.ComparePasswords(accountPassword, password))
                    throw new InvalidOperationException("Incorrect password.");

                // return the account if everything is valid
                _currentAccountLogin.SetAccount(account);
                return account;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred during login.", ex);
            }
        }
    }
}
