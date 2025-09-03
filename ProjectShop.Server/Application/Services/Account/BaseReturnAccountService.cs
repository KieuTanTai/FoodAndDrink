using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
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
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync(accountId, _specificEmpDAO.GetByAccountIdAsync, 
                () => new EmployeeModel(), nameof(TryLoadEmployeeAsync));

        private async Task<ServiceResult<CustomerModel>> TryLoadCustomerAsync(uint accountId)
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync(accountId, _customerDAO.GetByAccountIdAsync, 
                () => new CustomerModel(), nameof(TryLoadCustomerAsync));

        private async Task<ServiceResults<RolesOfUserModel>> TryLoadRolesOfUserAsync(uint accountId)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync<uint, RolesOfUserModel>(accountId, 
                (id) => _roleOfUserService.GetByAccountIdAsync(id), nameof(TryLoadRolesOfUserAsync));

        private async Task<IDictionary<uint, ServiceResult<EmployeeModel>>> TryLoadEmployeesAsync(IEnumerable<uint> accountIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync(accountIds,(ids) => _specificEmpDAO.GetByAccountIdsAsync(ids), 
                () => new EmployeeModel(), (entity) => entity.AccountId, nameof(TryLoadEmployeesAsync));

        private async Task<IDictionary<uint, ServiceResult<CustomerModel>>> TryLoadCustomersAsync(IEnumerable<uint> accountIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync(accountIds, (ids) => _customerDAO.GetByAccountIdsAsync(ids), 
                () => new CustomerModel(), (entity) => entity.AccountId, nameof(TryLoadCustomersAsync));

        private async Task<IDictionary<uint, ServiceResults<RolesOfUserModel>>> TryLoadRolesOfUsersAsync(IEnumerable<uint> accountIds)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync<uint, RolesOfUserModel>(accountIds, 
                (ids) => _roleOfUserService.GetByAccountIdsAsync(ids), (entity) => entity.AccountId, nameof(TryLoadRolesOfUsersAsync));

        // Helper methods: mỗi method xử lý 1 navigation property

        private async Task LoadEmployeeAsync(AccountModel account, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync<uint, EmployeeModel>(employee => account.Employee = employee, 
                () => account.AccountId, TryLoadEmployeeAsync, logEntries, nameof(LoadEmployeeAsync));

        private async Task LoadCustomerAsync(AccountModel account, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync<uint, CustomerModel>(customer => account.Customer = customer, 
                () => account.AccountId, TryLoadCustomerAsync, logEntries, nameof(LoadCustomerAsync));

        private async Task LoadRolesOfUsersAsync(AccountModel account, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync<uint, RolesOfUserModel>(roles => account.RolesOfUsers = [..roles], 
                () => account.AccountId, TryLoadRolesOfUserAsync, logEntries, nameof(LoadRolesOfUsersAsync));

        private async Task LoadEmployeesAsync(IEnumerable<AccountModel> accounts, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync<uint, AccountModel, EmployeeModel>(accounts, 
                (account, employee) => account.Employee = employee, account => account.AccountId, TryLoadEmployeesAsync, logEntries, nameof(LoadEmployeesAsync));

        private async Task LoadCustomersAsync(IEnumerable<AccountModel> accounts, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync<uint, AccountModel, CustomerModel>(accounts, 
                (account, customer) => account.Customer = customer, account => account.AccountId, TryLoadCustomersAsync, logEntries, nameof(LoadCustomersAsync));

        private async Task LoadRolesOfUsersAsync(IEnumerable<AccountModel> accounts, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync<uint, AccountModel, RolesOfUserModel>(accounts, 
                (account, roles) => account.RolesOfUsers = [..roles], account => account.AccountId, TryLoadRolesOfUsersAsync, logEntries, nameof(LoadRolesOfUsersAsync));
    }
}
