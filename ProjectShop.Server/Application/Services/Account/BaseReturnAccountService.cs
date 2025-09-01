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

        public async Task<ServiceResult<AccountModel>> GetNavigationPropertyByOptionsAsync(AccountModel account, AccountNavigationOptions options, [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            if (options.IsGetEmployee)
            {
                ServiceResult<EmployeeModel> result = await TryLoadEmployeeAsync(account.AccountId, methodCall);
                logEntries.AddRange(result.LogEntries!);
                account.Employee = result.Data!;
            }

            if (options.IsGetCustomer)
            {
                ServiceResult<CustomerModel> result = await TryLoadCustomerAsync(account.AccountId, methodCall);
                logEntries.AddRange(result.LogEntries!);
                account.Customer = result.Data!;
            }

            if (options.IsGetRolesOfUsers)
            {
                ServiceResults<RolesOfUserModel> result = await TryLoadRolesAsync(account.AccountId, methodCall);
                logEntries.AddRange(result.LogEntries!);
                account.RolesOfUsers = (ICollection<RolesOfUserModel>)result.Data!;
            }

            logEntries.Add(_logger.JsonLogInfo<AccountModel, BaseReturnAccountService>("Successfully retrieved account with navigation properties.", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResult<AccountModel>(account, logEntries);
        }

        public async Task<ServiceResults<AccountModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<AccountModel> accounts, AccountNavigationOptions options, [CallerMemberName] string? methodCall = null)
        {
            var accountList = accounts.ToList();
            var accountIds = accountList.Select(a => a.AccountId).ToList();
            List<JsonLogEntry> logEntries = [];

            if (options.IsGetEmployee)
            {
                var employees = await TryLoadEmployeesAsync(accountIds, methodCall);
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
                var customers = await TryLoadCustomersAsync(accountIds, methodCall);
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
                var rolesOfUsers = await TryLoadRolesOfUsersAsync(accountIds, methodCall);
                foreach (var account in accountList)
                {
                    rolesOfUsers.TryGetValue(account.AccountId, out var serviceResults);
                    logEntries.AddRange(serviceResults!.LogEntries!);
                    if (!serviceResults.Data!.Any())
                        break;
                    account.RolesOfUsers = (ICollection<RolesOfUserModel>)serviceResults.Data!;
                }
            }

            logEntries.Add(_logger.JsonLogInfo<AccountModel, BaseReturnAccountService>("Successfully retrieved accounts with navigation properties.", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResults(accountList, logEntries);
        }

        private async Task<ServiceResult<EmployeeModel>> TryLoadEmployeeAsync(uint accountId, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                EmployeeModel? employee = await _specificEmpDAO.GetByAccountIdAsync(accountId);
                bool isEmployeeFound = employee != null;
                employee ??= new EmployeeModel();
                string message = isEmployeeFound ? $"Successfully retrieved employee for accountId: {accountId}." : $"Employee not found for accountId: {accountId}.";
                return _serviceResultFactory.CreateServiceResult<EmployeeModel>(message, employee, isEmployeeFound, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult<EmployeeModel>(
                    $"Error occurred while retrieving employee for accountId: {accountId}.", new EmployeeModel(), false, ex, methodCall: methodCall);
            }
        }

        private async Task<ServiceResult<CustomerModel>> TryLoadCustomerAsync(uint accountId, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                CustomerModel? customer = await _customerDAO.GetByAccountIdAsync(accountId);
                bool isCustomerFound = customer != null;
                customer ??= new CustomerModel();
                string message = isCustomerFound ? $"Successfully retrieved customer for accountId: {accountId}." : $"Customer not found for accountId: {accountId}.";
                return _serviceResultFactory.CreateServiceResult<CustomerModel>(message, customer, isCustomerFound, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult<CustomerModel>(
                    $"Error occurred while retrieving customer for accountId: {accountId}.", new CustomerModel(), false, ex, methodCall: methodCall);
            }
        }

        private async Task<ServiceResults<RolesOfUserModel>> TryLoadRolesAsync(uint accountId, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                return await _roleOfUserService.GetByAccountIdAsync(accountId) 
                    ?? _serviceResultFactory.CreateServiceResults<RolesOfUserModel>( $"No roles found for accountId: {accountId}.", [], false, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>(
                    $"Error occurred while retrieving roles for accountId: {accountId}.", [], false, ex, methodCall: methodCall);
            }
        }

        private async Task<IDictionary<uint, ServiceResult<EmployeeModel>>> TryLoadEmployeesAsync(IEnumerable<uint> accountIds, [CallerMemberName] string? methodCall = null)
        {
            uint firstId = accountIds.FirstOrDefault();
            try
            {
                IEnumerable<EmployeeModel> employees = await _specificEmpDAO.GetByAccountIdsAsync(accountIds) ?? [];
                bool isEmployeesFound = employees.Any();
                if (!isEmployeesFound)
                    return new Dictionary<uint, ServiceResult<EmployeeModel>>
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResult<EmployeeModel>($"No employees found for accountId: {firstId}.", new EmployeeModel(), false, methodCall: methodCall)
                    };

                return employees.ToDictionary(entity => entity.AccountId, entity => _serviceResultFactory.CreateServiceResult<EmployeeModel>(
                    $"Successfully retrieved employee for accountId: {entity.AccountId}.", entity, isEmployeesFound, methodCall: methodCall));
            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResult<EmployeeModel>>
                {
                    [firstId] = _serviceResultFactory.CreateServiceResult<EmployeeModel>($"Error occurred while retrieving employees for accountId: {firstId}.", new EmployeeModel(), false, ex, methodCall: methodCall)
                };
            }
        }

        private async Task<IDictionary<uint, ServiceResult<CustomerModel>>> TryLoadCustomersAsync(IEnumerable<uint> accountIds, [CallerMemberName] string? methodCall = null)
        {
            uint firstId = accountIds.FirstOrDefault();
            try
            {
                IEnumerable<CustomerModel> customers = await _customerDAO.GetByAccountIdsAsync(accountIds) ?? [];
                bool isCustomersFound = customers.Any();
                if (!isCustomersFound)
                    return new Dictionary<uint, ServiceResult<CustomerModel>>
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResult<CustomerModel>($"No customers found for accountId: {firstId}.", new CustomerModel(), false, methodCall: methodCall) 
                    };
                return customers.ToDictionary(entity => entity.AccountId, entity => _serviceResultFactory.CreateServiceResult<CustomerModel>(
                    $"Successfully retrieved customer for accountId: {entity.AccountId}.", entity, isCustomersFound, methodCall: methodCall));

            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResult<CustomerModel>>
                {
                    [firstId] = _serviceResultFactory.CreateServiceResult<CustomerModel>($"Error occurred while retrieving customers for accountId: {firstId}.", new CustomerModel(), false, ex, methodCall: methodCall)
                };
            }
        }

        private async Task<IDictionary<uint, ServiceResults<RolesOfUserModel>>> TryLoadRolesOfUsersAsync(IEnumerable<uint> accountIds, [CallerMemberName] string? methodCall = null)
        {
            uint firstId = accountIds.FirstOrDefault();
            try
            {
                ServiceResults<RolesOfUserModel> roles = await _roleOfUserService.GetByAccountIdsAsync(accountIds) 
                    ?? _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"No roles found for provided accountIds.", [], false, methodCall: methodCall);
                bool isRolesFound = roles.Data!.Any();
                if (!isRolesFound)
                    return new Dictionary<uint, ServiceResults<RolesOfUserModel>>
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"No roles found for accountId: {firstId}.", [], false, methodCall: methodCall)
                    };
                return roles.Data!.GroupBy(role => role.AccountId)
                    .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<RolesOfUserModel>(
                        $"Successfully retrieved roles for accountId: {group.Key}.", group, isRolesFound, methodCall: methodCall));
            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResults<RolesOfUserModel>>
                {
                     [firstId] = _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"Error occurred while retrieving roles for accountId: {firstId}.", [], false, ex, methodCall: methodCall)
                };
            }
        }
    }
}
