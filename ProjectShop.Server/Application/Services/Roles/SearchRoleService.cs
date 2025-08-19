using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices.Role;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class SearchRoleService : BaseReturnRoleService, ISearchRoleService<RoleModel, RoleNavigationOptions>
    {
        public SearchRoleService(IDAO<RoleModel> baseDAO, IRoleDAO<RoleModel> roleDAO, IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO)
            : base(baseDAO, roleDAO, roleOfUserDAO)
        {
        }

        public async Task<IEnumerable<RoleModel>> GetAllAsync(int? maxGetCount, RoleNavigationOptions? options = null)
        {
            try
            {
                IEnumerable<RoleModel> roles = maxGetCount.HasValue
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

        public async Task<IEnumerable<RoleModel>> GetByCreatedDateMonthAndYearAsync(int year, int month, RoleNavigationOptions? options = null)
        {
            return await GetByMonthAndYearGenericAsync(
                _roleDAO.GetByMonthAndYearAsync, year, month, options, $"No roles found created in {month}/{year}.");
        }

        public async Task<IEnumerable<RoleModel>> GetByCreatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, RoleNavigationOptions? options = null) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _roleDAO.GetByDateTimeAsync(dateTime, ct), compareType, options, $"No roles found created at {dateTime} with comparison type {compareType}.");
        }

        public async Task<IEnumerable<RoleModel>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, RoleNavigationOptions? options = null)
        {
            return await GetByDateTimeRangeGenericAsync(
                () => _roleDAO.GetByDateTimeRangeAsync(startDate, endDate), options, $"No roles found created between {startDate} and {endDate}.");
        }

        public async Task<IEnumerable<RoleModel>> GetByCreatedYearAsync<TCompareType>(int year, TCompareType compareType, RoleNavigationOptions? options = null) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _roleDAO.GetByYearAsync(year, ct), compareType, options, $"No roles found created in year {year} with comparison type {compareType}.");
        }

        public async Task<IEnumerable<RoleModel>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, RoleNavigationOptions? options = null)
        {
            return await GetByMonthAndYearGenericAsync(
                _roleDAO.GetByLastUpdatedMonthAndYearAsync, year, month, options, $"No roles found updated in {month}/{year}.");
        }

        public async Task<IEnumerable<RoleModel>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, RoleNavigationOptions? options = null) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _roleDAO.GetByLastUpdatedDateAsync(dateTime, ct), compareType, options, $"No roles found updated at {dateTime} with comparison type {compareType}.");
        }

        public async Task<IEnumerable<RoleModel>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, RoleNavigationOptions? options = null)
        {
            return await GetByDateTimeRangeGenericAsync(
                () => _roleDAO.GetByLastUpdatedDateTimeRangeAsync(startDate, endDate), options, $"No roles found updated between {startDate} and {endDate}.");
        }

        public async Task<IEnumerable<RoleModel>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, RoleNavigationOptions? options = null) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _roleDAO.GetByLastUpdatedYearAsync(year, ct), compareType, options, $"No roles found updated in year {year} with comparison type {compareType}.");
        }

        public async Task<RoleModel> GetByRoleIdAsync(uint roleId, RoleNavigationOptions? options = null)
        {
            try
            {
                RoleModel? role = await _baseDAO.GetSingleDataAsync(roleId.ToString());
                if (role == null)
                    throw new InvalidOperationException($"No role found with ID: {roleId}.");
                if (options != null)
                    role = await GetNavigationPropertyByOptionsAsync(role, options);
                return role;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving role by ID.", ex);
            }
        }

        public async Task<RoleModel> GetByRoleNameAsync(string roleName, RoleNavigationOptions? options = null)
        {
            try
            {
                RoleModel? role = await _roleDAO.GetByRoleNameAsync(roleName);
                if (role == null)
                    throw new InvalidOperationException($"No role found with name: {roleName}.");
                if (options != null)
                    role = await GetNavigationPropertyByOptionsAsync(role, options);
                return role;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving role by name.", ex);
            }
        }

        public async Task<IEnumerable<RoleModel>> GetByStatusAsync(bool status, int? maxGetCount, RoleNavigationOptions? options = null)
        {
            try
            {
                IEnumerable<RoleModel> roles = maxGetCount.HasValue
                    ? await _roleDAO.GetByStatusAsync(status, maxGetCount.Value)
                    : await _roleDAO.GetByStatusAsync(status);
                if (options != null)
                    roles = await GetNavigationPropertyByOptionsAsync(roles, options);
                return roles;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles by status with a limit.", ex);
            }
        }

        public async Task<IEnumerable<RoleModel>> GetRelativeByRoleName(string roleName, int? maxGetCount, RoleNavigationOptions? options = null)
        {
            try
            {
                maxGetCount ??= 100;
                IEnumerable<RoleModel> roles = await _roleDAO.GetByLikeStringAsync(roleName, maxGetCount.Value);
                if (options != null)
                    roles = await GetNavigationPropertyByOptionsAsync(roles, options);
                return roles;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles relative to the role name.", ex);
            }
        }
    }
}
