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

        public async Task<IEnumerable<RolesOfUserModel>> GetAllAsync(RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RolesOfUserModel> roles = await _baseDAO.GetAllAsync(GetValidMaxRecord(maxGetCount));
                if (options != null)
                    roles = await GetNavigationPropertyByOptionsAsync(roles, options);
                return roles;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles with a limit.", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAccountIdAsync(uint accountId, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RolesOfUserModel> roles = await _roleOfUserDAO.GetByAccountIdAsync(accountId, GetValidMaxRecord(maxGetCount));
                if (options != null)
                    roles = await GetNavigationPropertyByOptionsAsync(roles, options);
                return roles;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles by account ID.", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RolesOfUserModel> roles = await _roleOfUserDAO.GetByAccountIdsAsync(accountIds, GetValidMaxRecord(maxGetCount));
                if (options != null)
                    roles = await GetNavigationPropertyByOptionsAsync(roles, options);
                return roles;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles by account IDs.", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAddedDateMonthAndYearAsync(int year, int month, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            return await GetByMonthAndYearGenericAsync(
                _roleOfUserDAO.GetByMonthAndYearAsync, year, month, options, $"No roles found added in {month}/{year}.", GetValidMaxRecord(maxGetCount));
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAddedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, RolesOfUserNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleOfUserDAO.GetByDateTimeAsync(dateTime, compareType, maxGetCount), compareType, options, $"No roles found added at {dateTime} with comparison type {compareType}.", GetValidMaxRecord(maxGetCount));
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAddedDateTimeRangeAsync(DateTime startDate, DateTime endDate, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            return await GetByDateTimeRangeGenericAsync(
                (maxGetCount) => _roleOfUserDAO.GetByDateTimeRangeAsync(startDate, endDate, maxGetCount), options, $"No account roles found created between {startDate} and {endDate}.", GetValidMaxRecord(maxGetCount));
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAddedYearAsync<TCompareType>(int year, TCompareType compareType, RolesOfUserNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleOfUserDAO.GetByYearAsync(year, compareType, maxGetCount), compareType, options, $"No roles found added in year {year} with comparison type {compareType}.", GetValidMaxRecord(maxGetCount));
        }

        public async Task<RolesOfUserModel?> GetByKeysAsync(RolesOfUserKey keys, RolesOfUserNavigationOptions? options)
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

        public async Task<IEnumerable<RolesOfUserModel>> GetByListKeysAsync(IEnumerable<RolesOfUserKey> listKeys, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RolesOfUserModel> rolesOfUsers = await _roleOfUserDAO.GetByListKeysAsync(listKeys, GetValidMaxRecord(maxGetCount));
                if (options != null)
                    rolesOfUsers = await GetNavigationPropertyByOptionsAsync(rolesOfUsers, options);
                return rolesOfUsers;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving the role by keys.", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByRoleIdAsync(uint roleId, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RolesOfUserModel> rolesOfUserModels = await _roleOfUserDAO.GetByRoleIdAsync(roleId, GetValidMaxRecord(maxGetCount));
                if (options != null)
                    rolesOfUserModels = await GetNavigationPropertyByOptionsAsync(rolesOfUserModels, options);
                return rolesOfUserModels;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles by role ID.", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByRoleIdsAsync(IEnumerable<uint> roleIds, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RolesOfUserModel> rolesOfUserModels = await _roleOfUserDAO.GetByRoleIdsAsync(roleIds, GetValidMaxRecord(maxGetCount));
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
