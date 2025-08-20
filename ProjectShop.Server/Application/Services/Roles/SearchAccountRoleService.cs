using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices.Role;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class SearchAccountRoleService : BaseReturnAccountRoleService, ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey>
    {
        public SearchAccountRoleService(IDAO<RolesOfUserModel> baseDAO, IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO,
            IDAO<RoleModel> baseRoleDAO, IDAO<AccountModel> baseAccountDAO) : base(baseDAO, roleOfUserDAO, baseRoleDAO, baseAccountDAO)
        {
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetAllAsync(int? maxGetCount = null, RolesOfUserNavigationOptions? options = null)
        {
            try
            {
                IEnumerable<RolesOfUserModel> roles = maxGetCount.HasValue
                    ? await _baseDAO.GetAllAsync(maxGetCount.Value)
                    : await _baseDAO.GetAllAsync();
                if (options != null)
                    roles = await GetNavigationPropertyByOptionsAsync(roles, options);
                return roles;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles with a limit.", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAccountIdAsync(uint accountId, int? maxGetCount = null, RolesOfUserNavigationOptions? options = null)
        {
            try
            {
                IEnumerable<RolesOfUserModel> roles = maxGetCount.HasValue
                    ? await _roleOfUserDAO.GetByAccountIdAsync(accountId, maxGetCount.Value)
                    : await _roleOfUserDAO.GetByAccountIdAsync(accountId);
                if (options != null)
                    roles = await GetNavigationPropertyByOptionsAsync(roles, options);
                return roles;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles by account ID.", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, RolesOfUserNavigationOptions? options = null)
        {
            try
            {
                IEnumerable<RolesOfUserModel> roles = await _roleOfUserDAO.GetByAccountIdsAsync(accountIds);
                if (options != null)
                    roles = await GetNavigationPropertyByOptionsAsync(roles, options);
                return roles;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles by account IDs.", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAddedDateMonthAndYearAsync(int year, int month, RolesOfUserNavigationOptions? options = null)
        {
            return await GetByMonthAndYearGenericAsync(
                _roleOfUserDAO.GetByMonthAndYearAsync, year, month, options, $"No roles found added in {month}/{year}.");
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAddedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, RolesOfUserNavigationOptions? options = null) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _roleOfUserDAO.GetByDateTimeAsync(dateTime, ct), compareType, options, $"No roles found added at {dateTime} with comparison type {compareType}.");
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAddedDateTimeRangeAsync(DateTime startDate, DateTime endDate, RolesOfUserNavigationOptions? options = null)
        {
            return await GetByDateTimeRangeGenericAsync(
                () => _roleOfUserDAO.GetByDateTimeRangeAsync(startDate, endDate), options, $"No account roles found created between {startDate} and {endDate}.");
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAddedYearAsync<TCompareType>(int year, TCompareType compareType, RolesOfUserNavigationOptions? options = null) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _roleOfUserDAO.GetByYearAsync(year, ct), compareType, options, $"No roles found added in year {year} with comparison type {compareType}.");
        }

        public async Task<RolesOfUserModel?> GetByKeysAsync(RolesOfUserKey keys, RolesOfUserNavigationOptions? options = null)
        {
            try
            {
                RolesOfUserModel? rolesOfUser = await _roleOfUserDAO.GetByKeysAsync(keys);
                if (options != null)
                    rolesOfUser = await GetNavigationPropertyByOptionsAsync(rolesOfUser, options);
                return rolesOfUser;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving the role by keys.", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByListKeysAsync(IEnumerable<RolesOfUserKey> listKeys, RolesOfUserNavigationOptions? options = null)
        {
            try
            {
                IEnumerable<RolesOfUserModel> rolesOfUsers = await _roleOfUserDAO.GetByListKeysAsync(listKeys);
                if (options != null)
                    rolesOfUsers = await GetNavigationPropertyByOptionsAsync(rolesOfUsers, options);
                return rolesOfUsers;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving the role by keys.", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByRoleIdAsync(uint roleId, int? maxGetCount = null, RolesOfUserNavigationOptions? options = null)
        {
            try
            {
                IEnumerable<RolesOfUserModel> rolesOfUserModels = maxGetCount.HasValue
                    ? await _roleOfUserDAO.GetByRoleIdAsync(roleId, maxGetCount.Value)
                    : await _roleOfUserDAO.GetByRoleIdAsync(roleId);
                if (options != null)
                    rolesOfUserModels = await GetNavigationPropertyByOptionsAsync(rolesOfUserModels, options);
                return rolesOfUserModels;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles by role ID.", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByRoleIdsAsync(IEnumerable<uint> roleIds, RolesOfUserNavigationOptions? options = null)
        {
            try
            {
                IEnumerable<RolesOfUserModel> rolesOfUserModels = await _roleOfUserDAO.GetByRoleIdsAsync(roleIds);
                if (options != null)
                    rolesOfUserModels = await GetNavigationPropertyByOptionsAsync(rolesOfUserModels, options);
                return rolesOfUserModels;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles by role IDs.", ex);
            }
        }
    }
}
