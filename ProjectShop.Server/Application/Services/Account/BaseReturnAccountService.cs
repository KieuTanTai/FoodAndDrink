using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.ObjectValue.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.Account
{
    public class BaseReturnAccountService : IBaseGetNavigationPropertyService<AccountModel, AccountNavigationOptions>
    {
        private readonly IPersonDAO<CustomerModel> _customerDAO;
        private readonly IEmployeeDAO<EmployeeModel> _specificEmpDAO;
        private readonly ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> _roleOfUserService;

        public BaseReturnAccountService(
            IPersonDAO<CustomerModel> customerDAO,
            IEmployeeDAO<EmployeeModel> specificEmpDAO,
            ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> roleOfUserDAO)
        {
            _customerDAO = customerDAO ?? throw new ArgumentNullException(nameof(customerDAO));
            _specificEmpDAO = specificEmpDAO ?? throw new ArgumentNullException(nameof(specificEmpDAO));
            _roleOfUserService = roleOfUserDAO ?? throw new ArgumentNullException(nameof(roleOfUserDAO));
        }

        public async Task<AccountModel> GetNavigationPropertyByOptionsAsync(AccountModel account, AccountNavigationOptions? options)
        {
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

        public async Task<IEnumerable<AccountModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<AccountModel> accounts, AccountNavigationOptions? options)
        {
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
                var roles = await _roleOfUserService.GetByAccountIdAsync(accountId);
                return roles != null && roles.Any() ? roles.ToList() : new List<RolesOfUserModel>();
            }
            catch
            {
                return new List<RolesOfUserModel>();
            }
        }

        private async Task<IDictionary<uint, EmployeeModel>> TryLoadEmployeesAsync(IEnumerable<uint> accountIds)
        {
            IEnumerable<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                employees = (await _specificEmpDAO.GetByAccountIdsAsync(accountIds)) ?? Enumerable.Empty<EmployeeModel>();
                return employees.ToDictionary(entity => entity.AccountId, entity => entity)
                    ?? new Dictionary<uint, EmployeeModel>();
            }
            catch
            {
                return new Dictionary<uint, EmployeeModel>();
            }
        }

        private async Task<IDictionary<uint, CustomerModel>> TryLoadCustomersAsync(IEnumerable<uint> accountIds)
        {
            IEnumerable<CustomerModel> customers = new List<CustomerModel>();
            try
            {
                customers = await _customerDAO.GetByAccountIdsAsync(accountIds) ?? Enumerable.Empty<CustomerModel>();

                return customers.ToDictionary(entity => entity.AccountId, entity => entity)
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
                var rolesList = await _roleOfUserService.GetByAccountIdsAsync(accountIds) ?? Enumerable.Empty<RolesOfUserModel>();
                return rolesList.GroupBy(role => role.AccountId)
                    .ToDictionary(group => group.Key, group => (ICollection<RolesOfUserModel>)group.ToList());
            }
            catch
            {
                return new Dictionary<uint, ICollection<RolesOfUserModel>>();
            }
        }
    }
}
