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
            if (options?.IsGetRolesOfUsers == true)
                await LoadRolesOfUsersAsync(role, logEntries);

            logEntries.Add(_logger.JsonLogInfo<RoleModel, BaseReturnRoleService>("Successfully retrieved roles with navigation properties", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResult<RoleModel>(role, logEntries);
        }

        public async Task<ServiceResults<RoleModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<RoleModel> roles, RoleNavigationOptions? options, [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            var roleList = roles.ToList();
            if (options?.IsGetRolesOfUsers == true)
                await LoadRolesOfUsersAsync(roleList, logEntries);

            logEntries.Add(_logger.JsonLogInfo<RoleModel, BaseReturnRoleService>("Successfully retrieved roles with navigation properties", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResults<RoleModel>(roleList, logEntries);
        }

        // Lấy 1 roleId
        private async Task<ServiceResults<RolesOfUserModel>> TryLoadRolesOfUsersAsync(uint roleId)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync<uint, RolesOfUserModel>(roleId,
                (id) => _baseRoleOfUserDAO.GetByRoleIdAsync(id), nameof(TryLoadRolesOfUsersAsync));

        // Lấy nhiều roleId (khuyến nghị DAO trả về IEnumerable<RolesOfUserModel> cho nhiều roleId)
        private async Task<IDictionary<uint, ServiceResults<RolesOfUserModel>>> TryLoadRolesOfUsersAsync(IEnumerable<uint> roleIds)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync<uint, RolesOfUserModel>(roleIds,
                (ids) => _baseRoleOfUserDAO.GetByRoleIdsAsync(ids), entity => entity.RoleId, nameof(TryLoadRolesOfUsersAsync));

        // Helper cho 1 role
        private async Task LoadRolesOfUsersAsync(RoleModel role, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync<uint, RolesOfUserModel>((result) => role.RolesOfUsers = [..result], 
                () => role.RoleId, TryLoadRolesOfUsersAsync, logEntries, nameof(LoadRolesOfUsersAsync));

        // Helper cho nhiều role
        private async Task LoadRolesOfUsersAsync(IEnumerable<RoleModel> roles, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync<uint, RoleModel, RolesOfUserModel>(roles, (role, result) => role.RolesOfUsers = [..result],
                (role) => role.RoleId, TryLoadRolesOfUsersAsync, logEntries, nameof(LoadRolesOfUsersAsync));
    }
}
