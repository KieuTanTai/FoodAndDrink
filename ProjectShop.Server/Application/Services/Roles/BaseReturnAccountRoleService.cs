using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;

namespace ProjectShop.Server.Application.Services.Roles
{
    public abstract class BaseReturnAccountRoleService : BaseGetByTimeService<RolesOfUserModel, RolesOfUserNavigationOptions>
    {
        protected readonly IDAO<RolesOfUserModel> _baseDAO;
        protected readonly IDAO<RoleModel> _baseRoleDAO;
        protected readonly IDAO<AccountModel> _baseAccountDAO;
        protected readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUserDAO;
        public BaseReturnAccountRoleService(IDAO<RolesOfUserModel> baseDAO, IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO,
            IDAO<RoleModel> baseRoleDAO, IDAO<AccountModel> baseAccountDAO)
        {
            _baseDAO = baseDAO ?? throw new ArgumentNullException(nameof(baseDAO));
            _roleOfUserDAO = roleOfUserDAO ?? throw new ArgumentNullException(nameof(roleOfUserDAO));
            _baseRoleDAO = baseRoleDAO ?? throw new ArgumentNullException(nameof(baseRoleDAO));
            _baseAccountDAO = baseAccountDAO ?? throw new ArgumentNullException(nameof(baseAccountDAO));
        }
        protected override async Task<RolesOfUserModel> GetNavigationPropertyByOptionsAsync(RolesOfUserModel role, RolesOfUserNavigationOptions? options)
        {
            if (options?.IsGetRole == true)
                role.Role = await TryLoadRoleAsync(role.RoleId);

            if (options?.IsGetAccount == true)
                role.Account = await TryLoadAccountAsync(role.AccountId);
            return role;
        }
        protected override async Task<IEnumerable<RolesOfUserModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<RolesOfUserModel> roles, RolesOfUserNavigationOptions? options)
        {
            if (options?.IsGetRole == true)
            {
                var roleList = roles.ToList();
                var roleIds = roleList.Select(r => r.RoleId).ToList();
                var rolesDict = await TryLoadRolesAsyncs(roleIds);
                foreach (var role in roleList)
                {
                    rolesDict.TryGetValue(role.RoleId, out var loadedRole);
                    role.Role = loadedRole ?? new RoleModel();
                }
            }

            if (options?.IsGetAccount == true)
            {
                var roleList = roles.ToList();
                var accountIds = roleList.Select(r => r.AccountId).ToList();
                var accountsDict = await TryLoadAccountsAsyncs(accountIds);
                foreach (var role in roleList)
                {
                    accountsDict.TryGetValue(role.AccountId, out var loadedAccount);
                    role.Account = loadedAccount ?? new AccountModel();
                }
            }

            return roles;
        }

        private async Task<RoleModel> TryLoadRoleAsync(uint roleId)
        {
            try
            {
                return await _baseRoleDAO.GetSingleDataAsync(roleId.ToString()) ?? new();
            }
            catch (Exception)
            {
                return new();
            }
        }

        private async Task<AccountModel> TryLoadAccountAsync(uint accountId)
        {
            try
            {
                return await _baseAccountDAO.GetSingleDataAsync(accountId.ToString()) ?? new();
            }
            catch (Exception)
            {
                return new();
            }
        }

        private async Task<IDictionary<uint, RoleModel>> TryLoadRolesAsyncs(IEnumerable<uint> roleIds)
        {
            try
            {
                return (await _baseRoleDAO.GetByInputsAsync(roleIds.Select(id => id.ToString())))
                    .ToDictionary(role => role.RoleId, role => role) ?? new Dictionary<uint, RoleModel>();
            }
            catch (Exception)
            {
                return new Dictionary<uint, RoleModel>();
            }
        }

        private async Task<IDictionary<uint, AccountModel>> TryLoadAccountsAsyncs(IEnumerable<uint> accountIds)
        {
            try
            {
                return (await _baseAccountDAO.GetByInputsAsync(accountIds.Select(id => id.ToString())))
                    .ToDictionary(account => account.AccountId, account => account) ?? new Dictionary<uint, AccountModel>();
            }
            catch (Exception)
            {
                return new Dictionary<uint, AccountModel>();
            }
        }
    }
}
