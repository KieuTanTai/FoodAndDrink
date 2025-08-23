using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;
using ProjectShop.Server.Core.ObjectValue.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.Account
{
    public class BaseReturnAccountService : IBaseGetNavigationPropertyService<AccountModel, AccountNavigationOptions>
    {
        private readonly IPersonDAO<CustomerModel> _customerDAO;
        private readonly IEmployeeDAO<EmployeeModel> _specificEmpDAO;
        private readonly ILogService _logger;
        private readonly ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> _roleOfUserService;
        private readonly IServiceResultFactory<BaseReturnAccountService> _serviceResultFactory;

        public BaseReturnAccountService(
            IPersonDAO<CustomerModel> customerDAO,
            IEmployeeDAO<EmployeeModel> specificEmpDAO,
            ILogService logger,
            ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> roleOfUserDAO,
            IServiceResultFactory<BaseReturnAccountService> serviceResultFactory)
        {
            _logger = logger;
            _customerDAO = customerDAO;
            _specificEmpDAO = specificEmpDAO;
            _roleOfUserService = roleOfUserDAO;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResult<AccountModel>> GetNavigationPropertyByOptionsAsync(AccountModel account, AccountNavigationOptions options)
        {
            List<JsonLogEntry> logEntries = new List<JsonLogEntry>();
            if (options.IsGetEmployee)
            {
                ServiceResult<EmployeeModel> result = await TryLoadEmployeeAsync(account.AccountId);
                logEntries.AddRange(result.LogEntries!);
                account.Employee = result.Data!;
            }

            if (options.IsGetCustomer)
            {
                ServiceResult<CustomerModel> result = await TryLoadCustomerAsync(account.AccountId);
                logEntries.AddRange(result.LogEntries!);
                account.Customer = result.Data!;
            }

            if (options.IsGetRolesOfUsers)
            {
                ServiceResults<RolesOfUserModel> result = await TryLoadRolesAsync(account.AccountId);
                logEntries.AddRange(result.LogEntries!);
                account.RolesOfUsers = (ICollection<RolesOfUserModel>)result.Data!;
            }

            logEntries.Add(_logger.JsonLogInfo<AccountModel, BaseReturnAccountService>("Successfully retrieved account with navigation properties."));
            return _serviceResultFactory.CreateServiceResult<AccountModel>(account, logEntries);
        }

        public async Task<ServiceResults<AccountModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<AccountModel> accounts, AccountNavigationOptions options)
        {
            var accountList = accounts.ToList();
            var accountIds = accountList.Select(a => a.AccountId).ToList();
            List<JsonLogEntry> logEntries = new List<JsonLogEntry>();

            if (options.IsGetEmployee)
            {
                var employees = await TryLoadEmployeesAsync(accountIds);
                foreach (AccountModel account in accountList)
                {
                    employees.TryGetValue(account.AccountId, out var serviceResult);
                    logEntries.AddRange(serviceResult!.LogEntries!);
                    if (serviceResult.Data!.EmployeeId == 0)
                        break;
                    account.Employee = serviceResult.Data!;
                }
            }

            if (options.IsGetCustomer)
            {
                var customers = await TryLoadCustomersAsync(accountIds);
                foreach (var account in accountList)
                {
                    customers.TryGetValue(account.AccountId, out var serviceResult);
                    logEntries.AddRange(serviceResult!.LogEntries!);
                    if (serviceResult.Data!.CustomerId == 0)
                        break;
                    account.Customer = serviceResult.Data;
                }
            }

            if (options.IsGetRolesOfUsers)
            {
                var rolesOfUsers = await TryLoadRolesOfUsersAsync(accountIds);
                foreach (var account in accountList)
                {
                    rolesOfUsers.TryGetValue(account.AccountId, out var serviceResults);
                    logEntries.AddRange(serviceResults!.LogEntries!);
                    if (!serviceResults.Data!.Any())
                        break;
                    account.RolesOfUsers = (ICollection<RolesOfUserModel>)serviceResults.Data!;
                }
            }

            logEntries.Add(_logger.JsonLogInfo<AccountModel, BaseReturnAccountService>("Successfully retrieved accounts with navigation properties."));
            return _serviceResultFactory.CreateServiceResults(accountList, logEntries);
        }

        private async Task<ServiceResult<EmployeeModel>> TryLoadEmployeeAsync(uint accountId)
        {
            try
            {
                EmployeeModel? employee = await _specificEmpDAO.GetByAccountIdAsync(accountId);
                bool isEmployeeFound = employee != null;
                employee = employee ?? new EmployeeModel();
                string message = isEmployeeFound ? $"Successfully retrieved employee for accountId: {accountId}." : $"Employee not found for accountId: {accountId}.";
                return _serviceResultFactory.CreateServiceResult<EmployeeModel>(message, employee, isEmployeeFound);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult<EmployeeModel>(
                    $"Error occurred while retrieving employee for accountId: {accountId}.", new EmployeeModel(), false, ex);
            }
        }

        private async Task<ServiceResult<CustomerModel>> TryLoadCustomerAsync(uint accountId)
        {
            try
            {
                CustomerModel? customer = await _customerDAO.GetByAccountIdAsync(accountId);
                bool isCustomerFound = customer != null;
                customer = customer ?? new CustomerModel();
                string message = isCustomerFound ? $"Successfully retrieved customer for accountId: {accountId}." : $"Customer not found for accountId: {accountId}.";
                return _serviceResultFactory.CreateServiceResult<CustomerModel>(message, customer, isCustomerFound);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult<CustomerModel>(
                    $"Error occurred while retrieving customer for accountId: {accountId}.", new CustomerModel(), false, ex);
            }
        }

        private async Task<ServiceResults<RolesOfUserModel>> TryLoadRolesAsync(uint accountId)
        {
            try
            {
                return await _roleOfUserService.GetByAccountIdAsync(accountId) 
                    ?? _serviceResultFactory.CreateServiceResults<RolesOfUserModel>( $"No roles found for accountId: {accountId}.", new List<RolesOfUserModel>(), false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>(
                    $"Error occurred while retrieving roles for accountId: {accountId}.", new List<RolesOfUserModel>(), false, ex);
            }
        }

        private async Task<IDictionary<uint, ServiceResult<EmployeeModel>>> TryLoadEmployeesAsync(IEnumerable<uint> accountIds)
        {
            uint firstId = accountIds.FirstOrDefault();
            try
            {
                IEnumerable<EmployeeModel> employees = await _specificEmpDAO.GetByAccountIdsAsync(accountIds) ?? Enumerable.Empty<EmployeeModel>();
                bool isEmployeesFound = employees.Any();
                if (!isEmployeesFound)
                    return new Dictionary<uint, ServiceResult<EmployeeModel>>
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResult<EmployeeModel>($"No employees found for accountId: {firstId}.", new EmployeeModel(), false)
                    };

                return employees.ToDictionary(entity => entity.AccountId, entity => _serviceResultFactory.CreateServiceResult<EmployeeModel>(
                    $"Successfully retrieved employee for accountId: {entity.AccountId}.", entity, isEmployeesFound));
            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResult<EmployeeModel>>
                {
                    [firstId] = _serviceResultFactory.CreateServiceResult<EmployeeModel>($"Error occurred while retrieving employees for accountId: {firstId}.", new EmployeeModel(), false, ex)
                };
            }
        }

        private async Task<IDictionary<uint, ServiceResult<CustomerModel>>> TryLoadCustomersAsync(IEnumerable<uint> accountIds)
        {
            uint firstId = accountIds.FirstOrDefault();
            try
            {
                IEnumerable<CustomerModel> customers = await _customerDAO.GetByAccountIdsAsync(accountIds) ?? Enumerable.Empty<CustomerModel>();
                bool isCustomersFound = customers.Any();
                if (!isCustomersFound)
                    return new Dictionary<uint, ServiceResult<CustomerModel>>
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResult<CustomerModel>($"No customers found for accountId: {firstId}.", new CustomerModel(), false) 
                    };
                return customers.ToDictionary(entity => entity.AccountId, entity => _serviceResultFactory.CreateServiceResult<CustomerModel>(
                    $"Successfully retrieved customer for accountId: {entity.AccountId}.", entity, isCustomersFound));

            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResult<CustomerModel>>
                {
                    [firstId] = _serviceResultFactory.CreateServiceResult<CustomerModel>($"Error occurred while retrieving customers for accountId: {firstId}.", new CustomerModel(), false, ex)
                };
            }
        }

        private async Task<IDictionary<uint, ServiceResults<RolesOfUserModel>>> TryLoadRolesOfUsersAsync(IEnumerable<uint> accountIds)
        {
            uint firstId = accountIds.FirstOrDefault();
            try
            {
                ServiceResults<RolesOfUserModel> roles = await _roleOfUserService.GetByAccountIdsAsync(accountIds) 
                    ?? _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"No roles found for provided accountIds.", new List<RolesOfUserModel>(), false);
                bool isRolesFound = roles.Data!.Any();
                if (!isRolesFound)
                    return new Dictionary<uint, ServiceResults<RolesOfUserModel>>
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"No roles found for accountId: {firstId}.", new List<RolesOfUserModel>(), false)
                    };
                return roles.Data!.GroupBy(role => role.AccountId)
                    .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<RolesOfUserModel>(
                        $"Successfully retrieved roles for accountId: {group.Key}.", group, isRolesFound));
            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResults<RolesOfUserModel>>
                {
                     [firstId] = _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"Error occurred while retrieving roles for accountId: {firstId}.", new List<RolesOfUserModel>(), false, ex)
                };
            }
        }
    }
}
