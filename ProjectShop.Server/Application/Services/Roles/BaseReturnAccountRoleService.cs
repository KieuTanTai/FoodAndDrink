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
            if (options?.IsGetRole == true)
                await LoadRoleAsync(role, logEntries);

            if (options?.IsGetAccount == true)
                await LoadAccountAsync(role, logEntries);

            logEntries.Add(_logger.JsonLogInfo<RolesOfUserModel, BaseReturnAccountRoleService>("Successfully retrieved account's roles with navigation properties.", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResult(role, logEntries);
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<RolesOfUserModel> roles, RolesOfUserNavigationOptions? options,
            [CallerMemberName] string? methodCall = null)
        {
            var roleList = roles.ToList();
            List<JsonLogEntry> logEntries = [];
            if (options?.IsGetRole == true)
                await LoadRolesAsync(roleList, logEntries);

            if (options?.IsGetAccount == true)
                await LoadAccountsAsync(roleList, logEntries);

            logEntries.Add(_logger.JsonLogInfo<RolesOfUserModel, BaseReturnAccountRoleService>("Successfully retrieved account's roles with navigation properties.", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResults(roleList, logEntries);
        }

        private async Task<ServiceResult<RoleModel>> TryLoadRoleAsync(uint roleId)
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync<uint, RoleModel>(roleId,
                (id) => _baseRoleDAO.GetSingleDataAsync(id.ToString()), () => new RoleModel(), nameof(TryLoadRoleAsync));

        private async Task<ServiceResult<AccountModel>> TryLoadAccountAsync(uint accountId)
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync<uint, AccountModel>(accountId,
                (id) => _baseAccountDAO.GetSingleDataAsync(id.ToString()), () => new AccountModel(), nameof(TryLoadAccountAsync));

        private async Task<IDictionary<uint, ServiceResult<RoleModel>>> TryLoadRolesAsyncs(IEnumerable<uint> roleIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync<uint, RoleModel>(roleIds,
                (ids) => _baseRoleDAO.GetByInputsAsync(ids.Select(id => id.ToString())), () => new RoleModel(), entity => entity.RoleId, nameof(TryLoadRolesAsyncs));

        private async Task<IDictionary<uint, ServiceResult<AccountModel>>> TryLoadAccountsAsyncs(IEnumerable<uint> accountIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync<uint, AccountModel>(accountIds,
                (ids) => _baseAccountDAO.GetByInputsAsync(ids.Select(id => id.ToString())), () => new AccountModel(), entity => entity.AccountId, nameof(TryLoadAccountsAsyncs));

        // Helper cho 1 role
        private async Task LoadRoleAsync(RolesOfUserModel role, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync<uint, RoleModel>(
                (result) => role.Role = result, () => role.RoleId, TryLoadRoleAsync, logEntries, nameof(LoadRoleAsync));

        private async Task LoadAccountAsync(RolesOfUserModel role, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync<uint, AccountModel>((result) => role.Account = result, 
                () => role.AccountId, TryLoadAccountAsync, logEntries, nameof(LoadAccountAsync));

        // Helper cho nhiều roles
        private async Task LoadRolesAsync(IEnumerable<RolesOfUserModel> roles, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync<uint, RolesOfUserModel, RoleModel>(
                roles, (role, result) => role.Role = result, (role) => role.RoleId, TryLoadRolesAsyncs, logEntries, nameof(LoadRolesAsync));

        private async Task LoadAccountsAsync(IEnumerable<RolesOfUserModel> roles, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync<uint, RolesOfUserModel, AccountModel>(
                roles, (role, result) => role.Account = result, (role) => role.AccountId, TryLoadAccountsAsyncs, logEntries, nameof(LoadAccountsAsync));
    }
}
