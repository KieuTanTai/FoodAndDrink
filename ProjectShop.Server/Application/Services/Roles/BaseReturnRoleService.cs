using ProjectShop.Server.Core.Entities;
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
        private readonly ISearchAccountRoleServices<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> _searchAccountRoleService;
        public BaseReturnRoleService(
            ILogService logger,
            ISearchAccountRoleServices<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> searchAccountRoleService,
            IServiceResultFactory<BaseReturnRoleService> serviceResultFactory)
        {
            _logger = logger;
            _serviceResultFactory = serviceResultFactory;
            _searchAccountRoleService = searchAccountRoleService;
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
        {
            try
            {
                return await _searchAccountRoleService.GetByRoleIdAsync(roleId)
                    ?? _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"No RolesOfUser found for provided RoleId.", [], false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("Error occurred while fetching RolesOfUser.", [], false, ex);
            }
        }

        // Lấy nhiều roleId (khuyến nghị DAO trả về IEnumerable<RolesOfUserModel> cho nhiều roleId)
        private async Task<IDictionary<uint, ServiceResults<RolesOfUserModel>>> TryLoadRolesOfUsersAsync(IEnumerable<uint> roleIds)
        {
            uint firstId = roleIds.FirstOrDefault();
            try
            {
                var rolesList = await _searchAccountRoleService.GetByRoleIdsAsync(roleIds) ?? _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"No account roles found for provided roleIds.", new List<RolesOfUserModel>(), false);
                bool isExists = rolesList.Data!.Any();
                if (!isExists)
                {
                    return new Dictionary<uint, ServiceResults<RolesOfUserModel>>
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"No RolesOfUser found for provided RoleIds.", [], false)
                    };
                }
                // Group theo RoleId
                return rolesList.Data!
                    .GroupBy(role => role.RoleId)
                    .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"RolesOfUser found for RoleId {group.Key}.", group, true));
            }
            catch
            {
                return new Dictionary<uint, ServiceResults<RolesOfUserModel>>
                {
                    [firstId] = _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("Error occurred while fetching RolesOfUser.", [], false)
                };
            }
        }

        // Helper cho 1 role
        private async Task LoadRolesOfUsersAsync(RoleModel role, List<JsonLogEntry> logEntries)
        {
            var results = await TryLoadRolesOfUsersAsync(role.RoleId);
            logEntries.AddRange(results.LogEntries!);
            role.RolesOfUsers = (ICollection<RolesOfUserModel>) results.Data!;
            logEntries.Add(_logger.JsonLogInfo<RoleModel, BaseReturnRoleService>("Loaded account's roles for a single role."));
        }

        // Helper cho nhiều role
        private async Task LoadRolesOfUsersAsync(IEnumerable<RoleModel> roles, List<JsonLogEntry> logEntries)
        {
            var roleList = roles.ToList();
            var roleIds = roleList.Select(r => r.RoleId).ToList();
            var rolesDict = await TryLoadRolesOfUsersAsync(roleIds);
            foreach (var role in roleList)
            {
                if (!rolesDict.TryGetValue(role.RoleId, out var serviceResults) || serviceResults == null || serviceResults.Data == null)
                {
                    role.RolesOfUsers = [];
                    continue;
                }
                logEntries.AddRange(serviceResults.LogEntries!);
                role.RolesOfUsers = (ICollection<RolesOfUserModel>) serviceResults.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<RoleModel, BaseReturnRoleService>("Loaded account's roles for multiple roles."));
        }
    }
}
