using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.ObjectValue.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class SearchRoleService : ISearchRoleService<RoleModel, RoleNavigationOptions>
    {
        private readonly IDAO<RoleModel> _baseDAO;
        private readonly IRoleDAO<RoleModel> _roleDAO;
        private readonly IBaseHelperService<RoleModel> _helper;
        private readonly ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> _searchAccountRoleService;
        private readonly IBaseGetNavigationPropertyService<RoleModel, RoleNavigationOptions> _navigationService;
        private readonly IBaseGetByTimeService<RoleModel, RoleNavigationOptions> _byTimeService;

        public SearchRoleService(IDAO<RoleModel> baseDAO, IRoleDAO<RoleModel> roleDAO, ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> searchAccountRoleService,
            IBaseGetNavigationPropertyService<RoleModel, RoleNavigationOptions> navigationService, IBaseHelperService<RoleModel> helper, IBaseGetByTimeService<RoleModel, RoleNavigationOptions> byTimeService)
        {
            _baseDAO = baseDAO;
            _roleDAO = roleDAO;
            _searchAccountRoleService = searchAccountRoleService;
            _navigationService = navigationService;
            _helper = helper;
            _byTimeService = byTimeService;
        }

        public async Task<IEnumerable<RoleModel>> GetAllAsync(RoleNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RoleModel> roles = await _baseDAO.GetAllAsync(_helper.GetValidMaxRecord(maxGetCount));

                if (options != null)
                    roles = await _navigationService.GetNavigationPropertyByOptionsAsync(roles, options);
                return roles;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles with a limit.", ex);
            }
        }

        public async Task<IEnumerable<RoleModel>> GetByCreatedDateMonthAndYearAsync(int year, int month, RoleNavigationOptions? options, int? maxGetCount)
        {
            return await _byTimeService.GetByMonthAndYearGenericAsync(
                _roleDAO.GetByMonthAndYearAsync, year, month, options, $"No roles found created in {month}/{year}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<IEnumerable<RoleModel>> GetByCreatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, RoleNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleDAO.GetByDateTimeAsync(dateTime, compareType, maxGetCount), compareType, options, $"No roles found created at {dateTime} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<IEnumerable<RoleModel>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, RoleNavigationOptions? options, int? maxGetCount)
        {
            return await _byTimeService.GetByDateTimeRangeGenericAsync(
                (maxGetCount) => _roleDAO.GetByDateTimeRangeAsync(startDate, endDate, maxGetCount), options, $"No roles found created between {startDate} and {endDate}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<IEnumerable<RoleModel>> GetByCreatedYearAsync<TCompareType>(int year, TCompareType compareType, RoleNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleDAO.GetByYearAsync(year, compareType, maxGetCount), compareType, options, $"No roles found created in year {year} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<IEnumerable<RoleModel>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, RoleNavigationOptions? options, int? maxGetCount)
        {
            return await _byTimeService.GetByMonthAndYearGenericAsync(
                _roleDAO.GetByLastUpdatedMonthAndYearAsync, year, month, options, $"No roles found updated in {month}/{year}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<IEnumerable<RoleModel>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, RoleNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleDAO.GetByLastUpdatedDateAsync(dateTime, compareType, maxGetCount), compareType, options, $"No roles found updated at {dateTime} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<IEnumerable<RoleModel>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, RoleNavigationOptions? options, int? maxGetCount)
        {
            return await _byTimeService.GetByDateTimeRangeGenericAsync(
                (maxGetCount) => _roleDAO.GetByLastUpdatedDateTimeRangeAsync(startDate, endDate, maxGetCount), options, $"No roles found updated between {startDate} and {endDate}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<IEnumerable<RoleModel>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, RoleNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleDAO.GetByLastUpdatedYearAsync(year, compareType, maxGetCount), compareType, options, $"No roles found updated in year {year} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<RoleModel> GetByRoleIdAsync(uint roleId, RoleNavigationOptions? options)
        {
            try
            {
                RoleModel? role = await _baseDAO.GetSingleDataAsync(roleId.ToString());
                if (role == null)
                    throw new InvalidOperationException($"No role found with ID: {roleId}.");
                if (options != null)
                    role = await _navigationService.GetNavigationPropertyByOptionsAsync(role, options);
                return role;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving role by ID.", ex);
            }
        }

        public async Task<RoleModel> GetByRoleNameAsync(string roleName, RoleNavigationOptions? options)
        {
            try
            {
                RoleModel? role = await _roleDAO.GetByRoleNameAsync(roleName);
                if (role == null)
                    throw new InvalidOperationException($"No role found with name: {roleName}.");
                if (options != null)
                    role = await _navigationService.GetNavigationPropertyByOptionsAsync(role, options);
                return role;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving role by name.", ex);
            }
        }

        public async Task<IEnumerable<RoleModel>> GetByStatusAsync(bool status, RoleNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RoleModel> roles = await _roleDAO.GetByStatusAsync(status, _helper.GetValidMaxRecord(maxGetCount));
                if (options != null)
                    roles = await _navigationService.GetNavigationPropertyByOptionsAsync(roles, options);
                return roles;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles by status with a limit.", ex);
            }
        }

        public async Task<IEnumerable<RoleModel>> GetRelativeByRoleName(string roleName, RoleNavigationOptions? options, int? maxGetCount)
        {
            try
            {
    
                IEnumerable<RoleModel> roles = await _roleDAO.GetByLikeStringAsync(roleName, _helper.GetValidMaxRecord(maxGetCount));
                if (options != null)
                    roles = await _navigationService.GetNavigationPropertyByOptionsAsync(roles, options);
                return roles;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles relative to the role name.", ex);
            }
        }
    }
}
