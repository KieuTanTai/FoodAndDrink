using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.ObjectValue.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class BaseReturnRoleService : IBaseGetNavigationPropertyService<RoleModel, RoleNavigationOptions>
    {
        private readonly IDAO<RoleModel> _baseDAO;
        private readonly IRoleDAO<RoleModel> _roleDAO;
        private readonly ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> _searchAccountRoleService;
        public BaseReturnRoleService(
            IDAO<RoleModel> baseDAO, 
            IRoleDAO<RoleModel> roleDAO, 
            ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> searchAccountRoleService)
        {
            _baseDAO = baseDAO;
            _roleDAO = roleDAO;
            //_roleOfUserDAO = roleOfUserDAO;
            _searchAccountRoleService = searchAccountRoleService 
                ?? throw new ArgumentNullException(nameof(searchAccountRoleService), "Search Account Role Service cannot be null.");
        }

        public async Task<RoleModel> GetNavigationPropertyByOptionsAsync(RoleModel role, RoleNavigationOptions? options)
        {
            if (options?.IsGetRolesOfUsers == true)
                role.RolesOfUsers = await TryLoadRolesOfUsersAsync(role.RoleId);

            return role;
        }

        public async Task<IEnumerable<RoleModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<RoleModel> roles, RoleNavigationOptions? options)
        {
            if (options?.IsGetRolesOfUsers == true)
            {
                IEnumerable<RoleModel> roleList = roles.ToList();
                var roleIds = roleList.Select(r => r.RoleId).ToList();

                var rolesDict = await TryLoadRolesOfUsersAsync(roleIds);
                foreach (var role in roleList)
                {
                    rolesDict.TryGetValue(role.RoleId, out var rolesOfUsers);
                    role.RolesOfUsers = rolesOfUsers ?? new List<RolesOfUserModel>();
                }
                return roleList;
            }

            return roles;
        }

        // Lấy 1 roleId
        private async Task<ICollection<RolesOfUserModel>> TryLoadRolesOfUsersAsync(uint roleId)
        {
            try
            {
                var roles = await _searchAccountRoleService.GetByRoleIdAsync(roleId);
                return roles != null && roles.Any() ? roles.ToList() : new List<RolesOfUserModel>();
            }
            catch
            {
                return new List<RolesOfUserModel>();
            }
        }

        // Lấy nhiều roleId (khuyến nghị DAO trả về IEnumerable<RolesOfUserModel> cho nhiều roleId)
        private async Task<IDictionary<uint, ICollection<RolesOfUserModel>>> TryLoadRolesOfUsersAsync(IEnumerable<uint> roleIds)
        {
            try
            {
                var rolesList = await _searchAccountRoleService.GetByRoleIdsAsync(roleIds) ?? Enumerable.Empty<RolesOfUserModel>();
                // Group theo RoleId
                return rolesList
                    .GroupBy(r => r.RoleId)
                    .ToDictionary(g => g.Key, g => (ICollection<RolesOfUserModel>)g.ToList());
            }
            catch
            {
                return new Dictionary<uint, ICollection<RolesOfUserModel>>();
            }
        }
    }
}
