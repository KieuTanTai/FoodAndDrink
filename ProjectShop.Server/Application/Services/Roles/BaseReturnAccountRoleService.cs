using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class BaseReturnAccountRoleService : IBaseGetNavigationPropertyServices<RolesOfUserModel, RolesOfUserNavigationOptions>
    {
        private readonly ILogService _logger;
        private readonly IDAO<RoleModel> _baseRoleDAO;
        private readonly IDAO<AccountModel> _baseAccountDAO;
        private readonly IServiceResultFactory<BaseReturnAccountRoleService> _serviceResultFactory;
        private readonly IBaseHelperReturnTEntityService<BaseReturnAccountRoleService> _baseHelperReturnTEntityService;

        public BaseReturnAccountRoleService(IDAO<RoleModel> baseRoleDAO, IDAO<AccountModel> baseAccountDAO, ILogService logger,
            IServiceResultFactory<BaseReturnAccountRoleService> serviceResultFactory, IBaseHelperReturnTEntityService<BaseReturnAccountRoleService> baseHelperReturnTEntityService)
        {
            _logger = logger;
            _baseRoleDAO = baseRoleDAO;
            _baseAccountDAO = baseAccountDAO;
            _serviceResultFactory = serviceResultFactory;
            _baseHelperReturnTEntityService = baseHelperReturnTEntityService;
        }

        // Method orchestrator

        public async Task<ServiceResult<RolesOfUserModel>> GetNavigationPropertyByOptionsAsync(RolesOfUserModel role, RolesOfUserNavigationOptions? options, [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            bool isSuccess = true;

            if (options?.IsGetRole == true)
            {
                await LoadRoleAsync(role, logEntries);
                isSuccess = ValidateRoleLoaded(role, isSuccess);
            }

            if (options?.IsGetAccount == true)
            {
                await LoadAccountAsync(role, logEntries);
                isSuccess = ValidateAccountLoaded(role, isSuccess);
            }

            AddFinalLogEntry(logEntries, isSuccess, methodCall, "account's role");
            return _serviceResultFactory.CreateServiceResult(role, logEntries, isSuccess);
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<RolesOfUserModel> roles, RolesOfUserNavigationOptions? options,
            [CallerMemberName] string? methodCall = null)
        {
            var roleList = roles.ToList();
            List<JsonLogEntry> logEntries = [];
            bool isSuccess = true;

            if (options?.IsGetRole == true)
            {
                await LoadRolesAsync(roleList, logEntries);
                isSuccess = ValidateRolesLoaded(roleList, isSuccess);
            }

            if (options?.IsGetAccount == true)
            {
                await LoadAccountsAsync(roleList, logEntries);
                isSuccess = ValidateAccountsLoaded(roleList, isSuccess);
            }

            AddFinalLogEntry(logEntries, isSuccess, methodCall, "account's roles");
            return _serviceResultFactory.CreateServiceResults(roleList, logEntries, isSuccess);
        }

        private async Task<ServiceResult<RoleModel>> TryLoadRoleAsync(uint roleId)
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync(roleId,
                (id) => _baseRoleDAO.GetSingleDataAsync(id.ToString()), () => new RoleModel(), nameof(TryLoadRoleAsync));

        private async Task<ServiceResult<AccountModel>> TryLoadAccountAsync(uint accountId)
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync(accountId,
                (id) => _baseAccountDAO.GetSingleDataAsync(id.ToString()), () => new AccountModel(), nameof(TryLoadAccountAsync));

        private async Task<IDictionary<uint, ServiceResult<RoleModel>>> TryLoadRolesAsyncs(IEnumerable<uint> roleIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync(roleIds,
                (ids) => _baseRoleDAO.GetByInputsAsync(ids.Select(id => id.ToString())), () => new RoleModel(), entity => entity.RoleId, nameof(TryLoadRolesAsyncs));

        private async Task<IDictionary<uint, ServiceResult<AccountModel>>> TryLoadAccountsAsyncs(IEnumerable<uint> accountIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync(accountIds,
                (ids) => _baseAccountDAO.GetByInputsAsync(ids.Select(id => id.ToString())), () => new AccountModel(), entity => entity.AccountId, nameof(TryLoadAccountsAsyncs));

        // Helper cho 1 role
        private async Task LoadRoleAsync(RolesOfUserModel role, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync(
                (result) => role.Role = result, () => role.RoleId, TryLoadRoleAsync, logEntries, nameof(LoadRoleAsync));

        private async Task LoadAccountAsync(RolesOfUserModel role, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync((result) => role.Account = result,
                () => role.AccountId, TryLoadAccountAsync, logEntries, nameof(LoadAccountAsync));

        // Helper cho nhiều roles
        private async Task LoadRolesAsync(IEnumerable<RolesOfUserModel> roles, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync(
                roles, (role, result) => role.Role = result, (role) => role.RoleId, TryLoadRolesAsyncs, logEntries, nameof(LoadRolesAsync));

        private async Task LoadAccountsAsync(IEnumerable<RolesOfUserModel> roles, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync(
                roles, (role, result) => role.Account = result, (role) => role.AccountId, TryLoadAccountsAsyncs, logEntries, nameof(LoadAccountsAsync));

        // Validation helper methods for single entity
        private bool ValidateRoleLoaded(RolesOfUserModel role, bool currentSuccess)
            => currentSuccess && role.Role != null && role.Role.RoleId != 0;

        private bool ValidateAccountLoaded(RolesOfUserModel role, bool currentSuccess)
            => currentSuccess && role.Account != null && role.Account.AccountId != 0;

        // Validation helper methods for multiple entities
        private bool ValidateRolesLoaded(IEnumerable<RolesOfUserModel> roles, bool currentSuccess)
            => currentSuccess && !roles.Any(role => role.Role == null || role.Role.RoleId == 0);

        private bool ValidateAccountsLoaded(IEnumerable<RolesOfUserModel> roles, bool currentSuccess)
            => currentSuccess && !roles.Any(role => role.Account == null || role.Account.AccountId == 0);

        // Final log entry helper
        private void AddFinalLogEntry(List<JsonLogEntry> logEntries, bool isSuccess, string? methodCall, string entityName)
        {
            if (!isSuccess)
                logEntries.Add(_logger.JsonLogWarning<RolesOfUserModel, BaseReturnAccountRoleService>($"One or more navigation properties could not be loaded for {entityName}.", methodCall: methodCall));
            else
                logEntries.Add(_logger.JsonLogInfo<RolesOfUserModel, BaseReturnAccountRoleService>($"Successfully retrieved {entityName} with navigation properties.", methodCall: methodCall));
        }
    }
}
