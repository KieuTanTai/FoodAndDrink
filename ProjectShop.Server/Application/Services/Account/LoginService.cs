using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IEntities;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using System.Security.Claims;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Account
{
    public class LoginService : BaseReturnAccountService<AccountModel>, ILoginService<AccountModel, AccountNavigationOptions>
    {
        public LoginService(
            IDAO<AccountModel> baseDAO,
            IAccountDAO<AccountModel> accountDAO,
            IPersonDAO<CustomerModel> customerDAO,
            IPersonDAO<EmployeeModel> employeeDAO,
            IEmployeeDAO<EmployeeModel> specificEmpDAO,
            IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO) : base(baseDAO, accountDAO, customerDAO, employeeDAO, specificEmpDAO, roleOfUserDAO)
        {
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
                if (!await hashPassword.ComparePasswords(accountPassword, password))
                    throw new InvalidOperationException("Incorrect password.");
                if (options != null)
                    account = await GetNavigationPropertyByOptions(account, options);
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
