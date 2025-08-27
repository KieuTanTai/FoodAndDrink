using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;
using ProjectShop.Server.Core.ObjectValue.GetNavigationPropertyOptions;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class BaseReturnRoleService : IBaseGetNavigationPropertyService<RoleModel, RoleNavigationOptions>
    {
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<BaseReturnRoleService> _serviceResultFactory;
        private readonly ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> _searchAccountRoleService;
        public BaseReturnRoleService(
            ILogService logger,
            ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> searchAccountRoleService,
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
            {
                ServiceResults<RolesOfUserModel> results = await TryLoadRolesOfUsersAsync(role.RoleId, methodCall);
                logEntries.AddRange(results.LogEntries!);
                role.RolesOfUsers = (ICollection<RolesOfUserModel>) results.Data!;
            }
            logEntries.Add(_logger.JsonLogInfo<RoleModel, BaseReturnRoleService>("RoleNavigationOptions", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResult<RoleModel>(role, logEntries);
        }

        public async Task<ServiceResults<RoleModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<RoleModel> roles, RoleNavigationOptions? options, [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            IEnumerable<RoleModel> roleList = [.. roles];
            if (options?.IsGetRolesOfUsers == true)
            {
                var roleIds = roleList.Select(r => r.RoleId).ToList();
                var rolesDict = await TryLoadRolesOfUsersAsync(roleIds, methodCall);
                foreach (var role in roleList)
                {
                    rolesDict.TryGetValue(role.RoleId, out var serviceResults);
                    logEntries.AddRange(serviceResults!.LogEntries!);
                    if (!serviceResults.Data!.Any())
                        break;
                    role.RolesOfUsers = (ICollection<RolesOfUserModel>)serviceResults.Data!;
                }
            }
            logEntries.Add(_logger.JsonLogInfo<RoleModel, BaseReturnRoleService>("RoleNavigationOptions", methodCall: methodCall));    
            return _serviceResultFactory.CreateServiceResults<RoleModel>(roleList, logEntries);
        }

        // Lấy 1 roleId
        private async Task<ServiceResults<RolesOfUserModel>> TryLoadRolesOfUsersAsync(uint roleId, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                return await _searchAccountRoleService.GetByRoleIdAsync(roleId) 
                    ?? _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"No RolesOfUser found for provided RoleId.", [], false, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("Error occurred while fetching RolesOfUser.", [], false, ex, methodCall: methodCall);
            }
        }

        // Lấy nhiều roleId (khuyến nghị DAO trả về IEnumerable<RolesOfUserModel> cho nhiều roleId)
        private async Task<IDictionary<uint, ServiceResults<RolesOfUserModel>>> TryLoadRolesOfUsersAsync(IEnumerable<uint> roleIds, [CallerMemberName] string? methodCall = null)
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
                        [firstId] = _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"No RolesOfUser found for provided RoleIds.", [], false, methodCall: methodCall)
                    };
                }
                // Group theo RoleId
                return rolesList.Data!
                    .GroupBy(role => role.RoleId)
                    .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"RolesOfUser found for RoleId {group.Key}.", group, true, methodCall: methodCall));
            }
            catch
            {
                return new Dictionary<uint, ServiceResults<RolesOfUserModel>>
                {
                    [firstId] = _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("Error occurred while fetching RolesOfUser.", [], false, methodCall: methodCall)
                };
            }
        }
    }
}
