using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class BaseReturnRoleService : IBaseGetNavigationPropertyServices<RoleModel, RoleNavigationOptions>
    {
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<BaseReturnRoleService> _serviceResultFactory;
        private readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _baseRoleOfUserDAO;
        private readonly IBaseHelperReturnTEntityService<BaseReturnRoleService> _baseHelperReturnTEntityService;
        public BaseReturnRoleService(
            ILogService logger,
            IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> baseRoleOfUserDAO,
            IServiceResultFactory<BaseReturnRoleService> serviceResultFactory,
            IBaseHelperReturnTEntityService<BaseReturnRoleService> baseHelperReturnTEntityService)
        {
            _logger = logger;
            _baseRoleOfUserDAO = baseRoleOfUserDAO;
            _serviceResultFactory = serviceResultFactory;
            _baseHelperReturnTEntityService = baseHelperReturnTEntityService;
        }

        public async Task<ServiceResult<RoleModel>> GetNavigationPropertyByOptionsAsync(RoleModel role, RoleNavigationOptions? options, [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            bool isSuccess = true;

            if (options?.IsGetRolesOfUsers == true)
            {
                await LoadRolesOfUsersAsync(role, logEntries);
                isSuccess = ValidateRolesOfUsersLoaded(role, isSuccess);
            }

            AddFinalLogEntry(logEntries, isSuccess, methodCall, "role");
            return _serviceResultFactory.CreateServiceResult(role, logEntries, isSuccess);
        }

        public async Task<ServiceResults<RoleModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<RoleModel> roles, RoleNavigationOptions? options, [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            var roleList = roles.ToList();
            bool isSuccess = true;

            if (options?.IsGetRolesOfUsers == true)
            {
                await LoadRolesOfUsersAsync(roleList, logEntries);
                isSuccess = ValidateRolesOfUsersLoaded(roleList, isSuccess);
            }

            AddFinalLogEntry(logEntries, isSuccess, methodCall, "roles");
            return _serviceResultFactory.CreateServiceResults(roleList, logEntries, isSuccess);
        }

        // Lấy 1 roleId
        private async Task<ServiceResults<RolesOfUserModel>> TryLoadRolesOfUsersAsync(uint roleId)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(roleId,
                (id) => _baseRoleOfUserDAO.GetByRoleIdAsync(id), nameof(TryLoadRolesOfUsersAsync));

        // Lấy nhiều roleId (khuyến nghị DAO trả về IEnumerable<RolesOfUserModel> cho nhiều roleId)
        private async Task<IDictionary<uint, ServiceResults<RolesOfUserModel>>> TryLoadRolesOfUsersAsync(IEnumerable<uint> roleIds)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(roleIds,
                (ids) => _baseRoleOfUserDAO.GetByRoleIdsAsync(ids), entity => entity.RoleId, nameof(TryLoadRolesOfUsersAsync));

        // Helper cho 1 role
        private async Task LoadRolesOfUsersAsync(RoleModel role, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync((result) => role.RolesOfUsers = [.. result],
                () => role.RoleId, TryLoadRolesOfUsersAsync, logEntries, nameof(LoadRolesOfUsersAsync));

        // Helper cho nhiều role
        private async Task LoadRolesOfUsersAsync(IEnumerable<RoleModel> roles, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(roles, (role, result) => role.RolesOfUsers = [.. result],
                (role) => role.RoleId, TryLoadRolesOfUsersAsync, logEntries, nameof(LoadRolesOfUsersAsync));

        // Validation helper methods for single entity
        private bool ValidateRolesOfUsersLoaded(RoleModel role, bool currentSuccess)
            => currentSuccess && role.RolesOfUsers != null && role.RolesOfUsers.Count > 0;

        // Validation helper methods for multiple entities
        private bool ValidateRolesOfUsersLoaded(IEnumerable<RoleModel> roles, bool currentSuccess)
            => currentSuccess && !roles.Any(role => role.RolesOfUsers == null || role.RolesOfUsers.Count == 0);

        // Final log entry helper
        private void AddFinalLogEntry(List<JsonLogEntry> logEntries, bool isSuccess, string? methodCall, string entityName)
        {
            if (!isSuccess)
                logEntries.Add(_logger.JsonLogWarning<RoleModel, BaseReturnRoleService>($"One or more navigation properties could not be loaded for {entityName}.", methodCall: methodCall));
            else
                logEntries.Add(_logger.JsonLogInfo<RoleModel, BaseReturnRoleService>($"Successfully retrieved {entityName} with navigation properties.", methodCall: methodCall));
        }
    }
}
