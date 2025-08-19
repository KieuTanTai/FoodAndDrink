using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Account
{
    public abstract class BaseReturnAccountService<TEntity> : ValidateService<TEntity> where TEntity : class
    {
        protected readonly IDAO<AccountModel> _baseDAO;
        protected readonly IAccountDAO<AccountModel> _accountDAO;
        protected readonly IPersonDAO<CustomerModel> _customerDAO;
        protected readonly IPersonDAO<EmployeeModel> _employeeDAO;
        protected readonly IEmployeeDAO<EmployeeModel> _specificEmpDAO;
        protected readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUserDAO;

        public BaseReturnAccountService(
            IDAO<AccountModel> baseDAO,
            IAccountDAO<AccountModel> accountDAO,
            IPersonDAO<CustomerModel> customerDAO,
            IPersonDAO<EmployeeModel> employeeDAO,
            IEmployeeDAO<EmployeeModel> specificEmpDAO,
            IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO)
        {
            _baseDAO = baseDAO ?? throw new ArgumentNullException(nameof(baseDAO));
            _accountDAO = accountDAO ?? throw new ArgumentNullException(nameof(accountDAO));
            _customerDAO = customerDAO ?? throw new ArgumentNullException(nameof(customerDAO));
            _employeeDAO = employeeDAO ?? throw new ArgumentNullException(nameof(employeeDAO));
            _specificEmpDAO = specificEmpDAO ?? throw new ArgumentNullException(nameof(specificEmpDAO));
            _roleOfUserDAO = roleOfUserDAO ?? throw new ArgumentNullException(nameof(roleOfUserDAO));
        }

        protected async Task<AccountModel> GetNavigationPropertyByOptions(AccountModel account, AccountNavigationOptions? options)
        {
            if (account == null)
                throw new InvalidOperationException("Account cannot be null for navigation property retrieval.");
            try
            {
                if (options != null)
                {
                    if (options.IsGetEmployee)
                    {
                        EmployeeModel? employee = await _specificEmpDAO.GetByAccountIdAsync(account.AccountId);
                        if (employee != null)
                            account.Employee = employee;
                    }
                    if (options.IsGetCustomer)
                    {
                        CustomerModel? customer = await _customerDAO.GetByAccountIdAsync(account.AccountId);
                        if (customer != null)
                            account.Customer = customer;
                    }
                    if (options.IsGetRolesOfUsers)
                    {
                        IEnumerable<RolesOfUserModel> roles = await _roleOfUserDAO.GetByAccountIdAsync(account.AccountId);
                        if (roles != null && roles.Any())
                            account.RolesOfUsers = (ICollection<RolesOfUserModel>)roles;
                    }
                }
                return account;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving navigation properties for the account.", ex);
            }
        }

        protected async Task<IEnumerable<AccountModel>> GetNavigationPropertyByOptions(IEnumerable<AccountModel> accounts, AccountNavigationOptions? options)
        {
            if (accounts == null || !accounts.Any())
                throw new InvalidOperationException("No accounts provided for navigation property retrieval.");

            try
            {
                if (options != null)
                {
                    if (options.IsGetEmployee)
                    {
                        foreach (AccountModel account in accounts)
                        {
                            EmployeeModel? employee = await _specificEmpDAO.GetByAccountIdAsync(account.AccountId);
                            if (employee != null)
                                account.Employee = employee;
                        }
                    }
                    if (options.IsGetCustomer)
                    {
                        foreach (AccountModel account in accounts)
                        {
                            CustomerModel? customer = await _customerDAO.GetByAccountIdAsync(account.AccountId);
                            if (customer != null)
                                account.Customer = customer;
                        }
                    }
                    if (options.IsGetRolesOfUsers)
                    {
                        foreach (AccountModel account in accounts)
                        {
                            IEnumerable<RolesOfUserModel> roles = await _roleOfUserDAO.GetByAccountIdAsync(account.AccountId);
                            if (roles != null && roles.Any())
                                account.RolesOfUsers = (ICollection<RolesOfUserModel>)roles;
                        }
                    }
                }
                return accounts;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving navigation properties for the account.", ex);
            }
        }
    }
}
