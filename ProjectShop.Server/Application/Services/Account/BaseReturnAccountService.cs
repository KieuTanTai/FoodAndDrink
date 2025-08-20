using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;

namespace ProjectShop.Server.Application.Services.Account
{
    public abstract class BaseReturnAccountService : BaseGetByTimeService<AccountModel, AccountNavigationOptions>
    {
        protected readonly IDAO<AccountModel> _baseDAO;
        protected readonly IAccountDAO<AccountModel> _accountDAO;
        protected readonly IPersonDAO<CustomerModel> _customerDAO;
        protected readonly IPersonDAO<EmployeeModel> _employeeDAO;
        protected readonly IEmployeeDAO<EmployeeModel> _specificEmpDAO;
        protected readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUserDAO;

        protected BaseReturnAccountService(
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

        protected override async Task<AccountModel> GetNavigationPropertyByOptionsAsync(AccountModel account, AccountNavigationOptions? options)
        {
            if (account == null)
                throw new InvalidOperationException("Account cannot be null for navigation property retrieval.");

            if (options == null)
                return account;

            if (options.IsGetEmployee)
                account.Employee = await TryLoadEmployeeAsync(account.AccountId);

            if (options.IsGetCustomer)
                account.Customer = await TryLoadCustomerAsync(account.AccountId);

            if (options.IsGetRolesOfUsers)
                account.RolesOfUsers = await TryLoadRolesAsync(account.AccountId);

            return account;
        }

        protected override async Task<IEnumerable<AccountModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<AccountModel> accounts, AccountNavigationOptions? options)
        {
            if (accounts == null || !accounts.Any())
                throw new InvalidOperationException("No accounts provided for navigation property retrieval.");

            if (options == null)
                return accounts;

            var accountList = accounts.ToList();
            var accountIds = accountList.Select(a => a.AccountId).ToList();

            if (options.IsGetEmployee)
            {
                var employees = await TryLoadEmployeesAsync(accountIds);
                foreach (AccountModel account in accountList)
                {
                    employees.TryGetValue(account.AccountId, out var employee);
                    account.Employee = employee ?? new(); // hoặc new EmployeeModel()
                }
            }

            if (options.IsGetCustomer)
            {
                var customers = await TryLoadCustomersAsync(accountIds);
                foreach (var account in accountList)
                {
                    customers.TryGetValue(account.AccountId, out var customer);
                    account.Customer = customer ?? new(); // hoặc new CustomerModel()
                }
            }

            if (options.IsGetRolesOfUsers)
            {
                var rolesOfUsers = await TryLoadRolesOfUsersAsync(accountIds);
                foreach (var account in accountList)
                {
                    rolesOfUsers.TryGetValue(account.AccountId, out var roles);
                    account.RolesOfUsers = roles ?? new List<RolesOfUserModel>();
                }
            }

            return accountList;
        }

        private async Task<EmployeeModel> TryLoadEmployeeAsync(uint accountId)
        {
            try
            {
                return await _specificEmpDAO.GetByAccountIdAsync(accountId) ?? new EmployeeModel();
            }
            catch
            {
                return new(); // hoặc new EmployeeModel() nếu bạn muốn property luôn có object
            }
        }

        private async Task<CustomerModel> TryLoadCustomerAsync(uint accountId)
        {
            try
            {
                return await _customerDAO.GetByAccountIdAsync(accountId) ?? new CustomerModel();
            }
            catch
            {
                return new(); // hoặc new CustomerModel()
            }
        }

        private async Task<ICollection<RolesOfUserModel>> TryLoadRolesAsync(uint accountId)
        {
            try
            {
                var roles = await _roleOfUserDAO.GetByAccountIdAsync(accountId);
                return roles != null && roles.Any() ? roles.ToList() : new List<RolesOfUserModel>();
            }
            catch
            {
                return new List<RolesOfUserModel>();
            }
        }

        private async Task<IDictionary<uint, EmployeeModel>> TryLoadEmployeesAsync(IEnumerable<uint> accountIds)
        {
            try
            {
                return (await _specificEmpDAO.GetByAccountIdsAsync(accountIds))
                    .ToDictionary(entity => entity.AccountId, entity => entity)
                    ?? new Dictionary<uint, EmployeeModel>();
            }
            catch
            {
                return new Dictionary<uint, EmployeeModel>();
            }
        }

        private async Task<IDictionary<uint, CustomerModel>> TryLoadCustomersAsync(IEnumerable<uint> accountIds)
        {
            try
            {
                return (await _customerDAO.GetByAccountIdsAsync(accountIds))
                    .ToDictionary(entity => entity.AccountId, entity => entity)
                    ?? new Dictionary<uint, CustomerModel>();
            }
            catch
            {
                return new Dictionary<uint, CustomerModel>();
            }
        }

        private async Task<IDictionary<uint, ICollection<RolesOfUserModel>>> TryLoadRolesOfUsersAsync(IEnumerable<uint> accountIds)
        {
            try
            {
                var rolesList = await _roleOfUserDAO.GetByAccountIdsAsync(accountIds) ?? Enumerable.Empty<RolesOfUserModel>();
                return rolesList.GroupBy(role => role.AccountId)
                    .ToDictionary(g => g.Key, g => (ICollection<RolesOfUserModel>)g.ToList());
            }
            catch
            {
                return new Dictionary<uint, ICollection<RolesOfUserModel>>();
            }
        }
    }
}
