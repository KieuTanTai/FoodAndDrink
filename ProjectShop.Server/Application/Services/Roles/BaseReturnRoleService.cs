using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;

namespace ProjectShop.Server.Application.Services.Roles
{
    public abstract class BaseReturnRoleService : BaseGetByTimeService<RoleModel, RoleNavigationOptions>
    {
        protected readonly IDAO<RoleModel> _baseDAO;
        protected readonly IRoleDAO<RoleModel> _roleDAO;
        protected readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUserDAO;
        protected BaseReturnRoleService(IDAO<RoleModel> baseDAO, IRoleDAO<RoleModel> roleDAO, IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO)
        {
            _baseDAO = baseDAO;
            _roleDAO = roleDAO;
            _roleOfUserDAO = roleOfUserDAO;
        }

        protected override async Task<RoleModel> GetNavigationPropertyByOptionsAsync(RoleModel role, RoleNavigationOptions? options)
        {
            if (options?.IsGetRolesOfUsers == true)
                role.RolesOfUsers = await TryLoadRolesOfUsersAsync(role.RoleId);

            return role;
        }

        protected override async Task<IEnumerable<RoleModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<RoleModel> roles, RoleNavigationOptions? options)
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
                var roles = await _roleOfUserDAO.GetByRoleIdAsync(roleId);
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
                var rolesList = await _roleOfUserDAO.GetByRoleIdsAsync(roleIds) ?? Enumerable.Empty<RolesOfUserModel>();
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
