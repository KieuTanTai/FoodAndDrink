using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services.Account
{
    public class BaseReturnAccountService : IBaseGetNavigationPropertyServices<AccountModel, AccountNavigationOptions>
    {
        private readonly ILogService _logger;
        private readonly IPersonDAO<CustomerModel> _customerDAO;
        private readonly IEmployeeDAO<EmployeeModel> _specificEmpDAO;
        private readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUserService;
        private readonly IServiceResultFactory<BaseReturnAccountService> _serviceResultFactory;
        private readonly IBaseHelperReturnTEntityService<BaseReturnAccountService> _baseHelperReturnTEntityService;

        public BaseReturnAccountService(ILogService logger, IPersonDAO<CustomerModel> customerDAO,
            IEmployeeDAO<EmployeeModel> specificEmpDAO,
            IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO,
            IBaseHelperReturnTEntityService<BaseReturnAccountService> baseHelperReturnTEntityService,
            IServiceResultFactory<BaseReturnAccountService> serviceResultFactory)
        {
            _logger = logger;
            _customerDAO = customerDAO;
            _specificEmpDAO = specificEmpDAO;
            _roleOfUserService = roleOfUserDAO;
            _serviceResultFactory = serviceResultFactory;
            _baseHelperReturnTEntityService = baseHelperReturnTEntityService;
        }

        // Method gốc: orchestrator, chỉ gọi các method đã tách nhỏ
        public async Task<ServiceResult<AccountModel>> GetNavigationPropertyByOptionsAsync(AccountModel account, AccountNavigationOptions options, [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            bool isSuccess = true;

            if (options.IsGetEmployee)
            {
                await LoadEmployeeAsync(account, logEntries);
                isSuccess = ValidateEmployeeLoaded(account, isSuccess);
            }

            if (options.IsGetCustomer)
            {
                await LoadCustomerAsync(account, logEntries);
                isSuccess = ValidateCustomerLoaded(account, isSuccess);
            }

            if (options.IsGetRolesOfUsers)
            {
                await LoadRolesOfUsersAsync(account, logEntries);
                isSuccess = ValidateRolesOfUsersLoaded(account, isSuccess);
            }

            AddFinalLogEntry(logEntries, isSuccess, methodCall);
            return _serviceResultFactory.CreateServiceResult(account, logEntries, isSuccess);
        }

        public async Task<ServiceResults<AccountModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<AccountModel> accounts, AccountNavigationOptions options, [CallerMemberName] string? methodCall = null)
        {
            var accountList = accounts.ToList();
            List<JsonLogEntry> logEntries = [];
            bool isSuccess = true;

            if (options.IsGetEmployee)
            {
                await LoadEmployeesAsync(accountList, logEntries);
                isSuccess = ValidateEmployeesLoaded(accountList, isSuccess);
            }

            if (options.IsGetCustomer)
            {
                await LoadCustomersAsync(accountList, logEntries);
                isSuccess = ValidateCustomersLoaded(accountList, isSuccess);
            }

            if (options.IsGetRolesOfUsers)
            {
                await LoadRolesOfUsersAsync(accountList, logEntries);
                isSuccess = ValidateRolesOfUsersLoaded(accountList, isSuccess);
            }

            AddFinalLogEntry(logEntries, isSuccess, methodCall);
            return _serviceResultFactory.CreateServiceResults(accountList, logEntries, isSuccess);
        }

        private async Task<ServiceResult<EmployeeModel>> TryLoadEmployeeAsync(uint accountId)
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync(accountId, _specificEmpDAO.GetByAccountIdAsync,
                () => new EmployeeModel(), nameof(TryLoadEmployeeAsync));

        private async Task<ServiceResult<CustomerModel>> TryLoadCustomerAsync(uint accountId)
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync(accountId, _customerDAO.GetByAccountIdAsync,
                () => new CustomerModel(), nameof(TryLoadCustomerAsync));

        private async Task<ServiceResults<RolesOfUserModel>> TryLoadRolesOfUserAsync(uint accountId)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(accountId,
                (id) => _roleOfUserService.GetByAccountIdAsync(id), nameof(TryLoadRolesOfUserAsync));

        private async Task<IDictionary<uint, ServiceResult<EmployeeModel>>> TryLoadEmployeesAsync(IEnumerable<uint> accountIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync(accountIds, (ids) => _specificEmpDAO.GetByAccountIdsAsync(ids),
                () => new EmployeeModel(), (entity) => entity.AccountId, nameof(TryLoadEmployeesAsync));

        private async Task<IDictionary<uint, ServiceResult<CustomerModel>>> TryLoadCustomersAsync(IEnumerable<uint> accountIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync(accountIds, (ids) => _customerDAO.GetByAccountIdsAsync(ids),
                () => new CustomerModel(), (entity) => entity.AccountId, nameof(TryLoadCustomersAsync));

        private async Task<IDictionary<uint, ServiceResults<RolesOfUserModel>>> TryLoadRolesOfUsersAsync(IEnumerable<uint> accountIds)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(accountIds,
                (ids) => _roleOfUserService.GetByAccountIdsAsync(ids), (entity) => entity.AccountId, nameof(TryLoadRolesOfUsersAsync));

        // Helper methods: mỗi method xử lý 1 navigation property
        private async Task LoadEmployeeAsync(AccountModel account, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync(employee => account.Employee = employee,
                () => account.AccountId, TryLoadEmployeeAsync, logEntries, nameof(LoadEmployeeAsync));

        private async Task LoadCustomerAsync(AccountModel account, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync(customer => account.Customer = customer,
                () => account.AccountId, TryLoadCustomerAsync, logEntries, nameof(LoadCustomerAsync));

        private async Task LoadRolesOfUsersAsync(AccountModel account, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(roles => account.RolesOfUsers = [.. roles],
                () => account.AccountId, TryLoadRolesOfUserAsync, logEntries, nameof(LoadRolesOfUsersAsync));

        private async Task LoadEmployeesAsync(IEnumerable<AccountModel> accounts, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync(accounts,
                (account, employee) => account.Employee = employee, account => account.AccountId, TryLoadEmployeesAsync, logEntries, nameof(LoadEmployeesAsync));

        private async Task LoadCustomersAsync(IEnumerable<AccountModel> accounts, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync(accounts,
                (account, customer) => account.Customer = customer, account => account.AccountId, TryLoadCustomersAsync, logEntries, nameof(LoadCustomersAsync));

        private async Task LoadRolesOfUsersAsync(IEnumerable<AccountModel> accounts, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(accounts,
                (account, roles) => account.RolesOfUsers = [.. roles], account => account.AccountId, TryLoadRolesOfUsersAsync, logEntries, nameof(LoadRolesOfUsersAsync));

        // Validation helper methods for single entity
        private static bool ValidateEmployeeLoaded(AccountModel account, bool currentSuccess)
            => currentSuccess && account.Employee.EmployeeId != 0;

        private static bool ValidateCustomerLoaded(AccountModel account, bool currentSuccess)
            => currentSuccess && account.Customer.CustomerId != 0;

        private static bool ValidateRolesOfUsersLoaded(AccountModel account, bool currentSuccess)
            => currentSuccess && account.RolesOfUsers.Count > 0;

        // Validation helper methods for multiple entities
        private static bool ValidateEmployeesLoaded(IEnumerable<AccountModel> accounts, bool currentSuccess)
            => currentSuccess && !accounts.Any(account => account.Employee == null || account.Employee.EmployeeId == 0);

        private static bool ValidateCustomersLoaded(IEnumerable<AccountModel> accounts, bool currentSuccess)
            => currentSuccess && !accounts.Any(account => account.Customer == null || account.Customer.CustomerId == 0);

        private static bool ValidateRolesOfUsersLoaded(IEnumerable<AccountModel> accounts, bool currentSuccess)
            => currentSuccess && !accounts.Any(account => account.RolesOfUsers == null || account.RolesOfUsers.Count == 0);

        // Final log entry helper
        private void AddFinalLogEntry(List<JsonLogEntry> logEntries, bool isSuccess, string? methodCall)
        {
            if (!isSuccess)
                logEntries.Add(_logger.JsonLogWarning<AccountModel, BaseReturnAccountService>("One or more navigation properties could not be loaded.", methodCall: methodCall));
            else
                logEntries.Add(_logger.JsonLogInfo<AccountModel, BaseReturnAccountService>("Successfully retrieved account with navigation properties.", methodCall: methodCall));
        }
    }
}
