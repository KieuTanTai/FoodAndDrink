using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.ObjectValue.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class SearchAccountRoleService : ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey>
    {
        private readonly IDAO<RolesOfUserModel> _baseDAO;
        private readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUserDAO;
        private readonly IDAO<RoleModel> _baseRoleDAO;
        private readonly IDAO<AccountModel> _baseAccountDAO;
        private readonly IBaseHelperService<RolesOfUserModel> _helper;
        private readonly IBaseGetNavigationPropertyService<RolesOfUserModel, RolesOfUserNavigationOptions> _navigationService;
        private readonly IBaseGetByTimeService<RolesOfUserModel, RolesOfUserNavigationOptions> _byTimeService;

        public SearchAccountRoleService(
            IDAO<RolesOfUserModel> baseDAO,
            IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO,
            IDAO<RoleModel> baseRoleDAO,
            IDAO<AccountModel> baseAccountDAO,
            IBaseHelperService<RolesOfUserModel> helper,
            IBaseGetNavigationPropertyService<RolesOfUserModel, RolesOfUserNavigationOptions> navigationService,
            IBaseGetByTimeService<RolesOfUserModel, RolesOfUserNavigationOptions> byTimeService
        )
        {
            _baseDAO = baseDAO;
            _roleOfUserDAO = roleOfUserDAO;
            _baseRoleDAO = baseRoleDAO;
            _baseAccountDAO = baseAccountDAO;
            _helper = helper;
            _navigationService = navigationService;
            _byTimeService = byTimeService;
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetAllAsync(RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RolesOfUserModel> roles = await _baseDAO.GetAllAsync(_helper.GetValidMaxRecord(maxGetCount));
                if (options != null)
                    roles = await _navigationService.GetNavigationPropertyByOptionsAsync(roles, options);
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
                IEnumerable<RolesOfUserModel> roles = await _roleOfUserDAO.GetByAccountIdAsync(accountId, _helper.GetValidMaxRecord(maxGetCount));
                if (options != null)
                    roles = await _navigationService.GetNavigationPropertyByOptionsAsync(roles, options);
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
                IEnumerable<RolesOfUserModel> roles = await _roleOfUserDAO.GetByAccountIdsAsync(accountIds, _helper.GetValidMaxRecord(maxGetCount));
                if (options != null)
                    roles = await _navigationService.GetNavigationPropertyByOptionsAsync(roles, options);
                return roles;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles by account IDs.", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAddedDateMonthAndYearAsync(int year, int month, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            return await _byTimeService.GetByMonthAndYearGenericAsync(
                _roleOfUserDAO.GetByMonthAndYearAsync, year, month, options, $"No roles found added in {month}/{year}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAddedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, RolesOfUserNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleOfUserDAO.GetByDateTimeAsync(dateTime, compareType, maxGetCount), compareType, options, $"No roles found added at {dateTime} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAddedDateTimeRangeAsync(DateTime startDate, DateTime endDate, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            return await _byTimeService.GetByDateTimeRangeGenericAsync(
                (maxGetCount) => _roleOfUserDAO.GetByDateTimeRangeAsync(startDate, endDate, maxGetCount), options, $"No account roles found created between {startDate} and {endDate}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAddedYearAsync<TCompareType>(int year, TCompareType compareType, RolesOfUserNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleOfUserDAO.GetByYearAsync(year, compareType, maxGetCount), compareType, options, $"No roles found added in year {year} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<RolesOfUserModel?> GetByKeysAsync(RolesOfUserKey keys, RolesOfUserNavigationOptions? options)
        {
            try
            {
                RolesOfUserModel? rolesOfUser = await _roleOfUserDAO.GetByKeysAsync(keys);
                if (options != null)
                    rolesOfUser = await _navigationService.GetNavigationPropertyByOptionsAsync(rolesOfUser, options);
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
                IEnumerable<RolesOfUserModel> rolesOfUsers = await _roleOfUserDAO.GetByListKeysAsync(listKeys, _helper.GetValidMaxRecord(maxGetCount));
                if (options != null)
                    rolesOfUsers = await _navigationService.GetNavigationPropertyByOptionsAsync(rolesOfUsers, options);
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
                IEnumerable<RolesOfUserModel> rolesOfUserModels = await _roleOfUserDAO.GetByRoleIdAsync(roleId, _helper.GetValidMaxRecord(maxGetCount));
                if (options != null)
                    rolesOfUserModels = await _navigationService.GetNavigationPropertyByOptionsAsync(rolesOfUserModels, options);
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
                IEnumerable<RolesOfUserModel> rolesOfUserModels = await _roleOfUserDAO.GetByRoleIdsAsync(roleIds, _helper.GetValidMaxRecord(maxGetCount));
                if (options != null)
                    rolesOfUserModels = await _navigationService.GetNavigationPropertyByOptionsAsync(rolesOfUserModels, options);
                return rolesOfUserModels;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving roles by role IDs.", ex);
            }
        }
    }
}
