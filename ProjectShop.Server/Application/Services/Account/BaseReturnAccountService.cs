using ProjectShop.Server.Application.Services.Product;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services.Account
{
    public class BaseReturnAccountService : IBaseGetNavigationPropertyServices<AccountModel, AccountNavigationOptions>
    {
        private readonly IPersonDAO<CustomerModel> _customerDAO;
        private readonly IEmployeeDAO<EmployeeModel> _specificEmpDAO;
        private readonly ILogService _logger;
        private readonly ISearchAccountRoleServices<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> _roleOfUserService;
        private readonly IServiceResultFactory<BaseReturnAccountService> _serviceResultFactory;

        public BaseReturnAccountService(
            IPersonDAO<CustomerModel> customerDAO,
            IEmployeeDAO<EmployeeModel> specificEmpDAO,
            ILogService logger,
            ISearchAccountRoleServices<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> roleOfUserDAO,
            IServiceResultFactory<BaseReturnAccountService> serviceResultFactory)
        {
            _logger = logger;
            _customerDAO = customerDAO;
            _specificEmpDAO = specificEmpDAO;
            _roleOfUserService = roleOfUserDAO;
            _serviceResultFactory = serviceResultFactory;
        }

        // Method gốc: orchestrator, chỉ gọi các method đã tách nhỏ

        public async Task<ServiceResult<AccountModel>> GetNavigationPropertyByOptionsAsync(AccountModel account, AccountNavigationOptions options, [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            if (options.IsGetEmployee)
                await LoadEmployeeAsync(account, logEntries);

            if (options.IsGetCustomer)
                await LoadCustomerAsync(account, logEntries);

            if (options.IsGetRolesOfUsers)
                await LoadRolesOfUsersAsync(account, logEntries);

            logEntries.Add(_logger.JsonLogInfo<AccountModel, BaseReturnAccountService>("Successfully retrieved account with navigation properties.", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResult<AccountModel>(account, logEntries);
        }

        public async Task<ServiceResults<AccountModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<AccountModel> accounts, AccountNavigationOptions options, [CallerMemberName] string? methodCall = null)
        {
            var accountList = accounts.ToList();
            List<JsonLogEntry> logEntries = [];

            if (options.IsGetEmployee)
                await LoadEmployeesAsync(accountList, logEntries);

            if (options.IsGetCustomer)
                await LoadCustomersAsync(accountList, logEntries);

            if (options.IsGetRolesOfUsers)
                await LoadRolesOfUsersAsync(accountList, logEntries);

            logEntries.Add(_logger.JsonLogInfo<AccountModel, BaseReturnAccountService>("Successfully retrieved accounts with navigation properties.", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResults(accountList, logEntries);
        }

        private async Task<ServiceResult<EmployeeModel>> TryLoadEmployeeAsync(uint accountId)
        {
            try
            {
                EmployeeModel? employee = await _specificEmpDAO.GetByAccountIdAsync(accountId);
                bool isEmployeeFound = employee != null;
                employee ??= new EmployeeModel();
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
                customer ??= new CustomerModel();
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
                    ?? _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"No roles found for accountId: {accountId}.", [], false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>(
                    $"Error occurred while retrieving roles for accountId: {accountId}.", [], false, ex);
            }
        }

        private async Task<IDictionary<uint, ServiceResult<EmployeeModel>>> TryLoadEmployeesAsync(IEnumerable<uint> accountIds)
        {
            uint firstId = accountIds.FirstOrDefault();
            try
            {
                IEnumerable<EmployeeModel> employees = await _specificEmpDAO.GetByAccountIdsAsync(accountIds) ?? [];
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
                IEnumerable<CustomerModel> customers = await _customerDAO.GetByAccountIdsAsync(accountIds) ?? [];
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
                    ?? _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"No roles found for provided accountIds.", [], false);
                bool isRolesFound = roles.Data!.Any();
                if (!isRolesFound)
                    return new Dictionary<uint, ServiceResults<RolesOfUserModel>>
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"No roles found for accountId: {firstId}.", [], false)
                    };
                return roles.Data!.GroupBy(role => role.AccountId)
                    .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<RolesOfUserModel>(
                        $"Successfully retrieved roles for accountId: {group.Key}.", group, isRolesFound));
            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResults<RolesOfUserModel>>
                {
                    [firstId] = _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"Error occurred while retrieving roles for accountId: {firstId}.", [], false, ex)
                };
            }
        }

        // Helper methods: mỗi method xử lý 1 navigation property

        private async Task LoadEmployeeAsync(AccountModel account, List<JsonLogEntry> logEntries)
        {
            var result = await TryLoadEmployeeAsync(account.AccountId);
            logEntries.AddRange(result.LogEntries!);
            account.Employee = result.Data!;
            logEntries.Add(_logger.JsonLogInfo<AccountModel, BaseReturnAccountService>($"Loaded Employee for account with id: {account.AccountId}"));
        }

        private async Task LoadCustomerAsync(AccountModel account, List<JsonLogEntry> logEntries)
        {
            var result = await TryLoadCustomerAsync(account.AccountId);
            logEntries.AddRange(result.LogEntries!);
            account.Customer = result.Data!;
            logEntries.Add(_logger.JsonLogInfo<AccountModel, BaseReturnAccountService>($"Loaded Customer for account with id: {account.AccountId}"));
        }

        private async Task LoadRolesOfUsersAsync(AccountModel account, List<JsonLogEntry> logEntries)
        {
            var result = await TryLoadRolesAsync(account.AccountId);
            logEntries.AddRange(result.LogEntries!);
            account.RolesOfUsers = (ICollection<RolesOfUserModel>)result.Data!;
            logEntries.Add(_logger.JsonLogInfo<AccountModel, BaseReturnAccountService>($"Loaded Roles for account with id: {account.AccountId}"));
        }

        private async Task LoadEmployeesAsync(IEnumerable<AccountModel> accounts, List<JsonLogEntry> logEntries)
        {
            var accountList = accounts.ToList();
            var accountIds = accountList.Select(a => a.AccountId).ToList();
            var employees = await TryLoadEmployeesAsync(accountIds);
            foreach (var account in accountList)
            {
                if (!employees.TryGetValue(account.AccountId, out var serviceResult) || serviceResult == null || serviceResult.Data == null)
                {
                    account.Employee = new();
                    continue;
                }
                logEntries.AddRange(serviceResult.LogEntries!);
                account.Employee = serviceResult.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<AccountModel, BaseReturnAccountService>("Loaded Employees for accounts."));
        }

        private async Task LoadCustomersAsync(IEnumerable<AccountModel> accounts, List<JsonLogEntry> logEntries)
        {
            var accountList = accounts.ToList();
            var accountIds = accountList.Select(a => a.AccountId).ToList();
            var customers = await TryLoadCustomersAsync(accountIds);
            foreach (var account in accountList)
            {
                if (!customers.TryGetValue(account.AccountId, out var serviceResult) || serviceResult == null || serviceResult.Data == null)
                {
                    account.Customer = new();
                    continue;
                }
                logEntries.AddRange(serviceResult.LogEntries!);
                account.Customer = serviceResult.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<AccountModel, BaseReturnAccountService>("Loaded Customers for accounts."));
        }

        private async Task LoadRolesOfUsersAsync(IEnumerable<AccountModel> accounts, List<JsonLogEntry> logEntries)
        {
            var accountList = accounts.ToList();
            var accountIds = accountList.Select(a => a.AccountId).ToList();
            var rolesOfUsers = await TryLoadRolesOfUsersAsync(accountIds);
            foreach (var account in accountList)
            {
                if (!rolesOfUsers.TryGetValue(account.AccountId, out var serviceResults) || serviceResults == null || serviceResults.Data == null)
                {
                    account.RolesOfUsers = [];
                    continue;
                }
                logEntries.AddRange(serviceResults.LogEntries!);
                account.RolesOfUsers = (ICollection<RolesOfUserModel>)serviceResults.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<AccountModel, BaseReturnAccountService>("Loaded Roles for accounts."));
        }
    }
}
