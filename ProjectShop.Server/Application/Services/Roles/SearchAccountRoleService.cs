using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;
using ProjectShop.Server.Core.ObjectValue.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class SearchAccountRoleService : ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey>
    {
        private readonly IDAO<RolesOfUserModel> _baseDAO;
        private readonly IBaseHelperService<RolesOfUserModel> _helper;
        private readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUserDAO;
        private readonly IBaseGetByTimeService<RolesOfUserModel, RolesOfUserNavigationOptions> _byTimeService;
        private readonly IServiceGetSingle<RolesOfUserModel, RolesOfUserNavigationOptions, SearchAccountRoleService> _getSingleService;
        private readonly IServiceGetMultiple<RolesOfUserModel, RolesOfUserNavigationOptions, SearchAccountRoleService> _getMultipleService;

        public SearchAccountRoleService(IDAO<RolesOfUserModel> baseDAO,
            IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO,
            IBaseHelperService<RolesOfUserModel> helper,
            IBaseGetByTimeService<RolesOfUserModel, RolesOfUserNavigationOptions> byTimeService,
            IServiceGetMultiple<RolesOfUserModel, RolesOfUserNavigationOptions, SearchAccountRoleService> getMultipleService,
            IServiceGetSingle<RolesOfUserModel, RolesOfUserNavigationOptions, SearchAccountRoleService> getSingleService)
        {
            _helper = helper;
            _baseDAO = baseDAO;
            _roleOfUserDAO = roleOfUserDAO;
            _byTimeService = byTimeService;
            _getSingleService = getSingleService;
            _getMultipleService = getMultipleService;
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetAllAsync(RolesOfUserNavigationOptions? options, int? maxGetCount)
            => await _getMultipleService.GetManyAsync(_baseDAO.GetAllAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RolesOfUserModel>> GetByAccountIdAsync(uint accountId, RolesOfUserNavigationOptions? options, int? maxGetCount)
            => await _getMultipleService.GetManyAsync((maxCount) => _roleOfUserDAO.GetByAccountIdAsync(accountId, maxCount), options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RolesOfUserModel>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, RolesOfUserNavigationOptions? options, int? maxGetCount)
            => await _getMultipleService.GetManyAsync((maxCount) => _roleOfUserDAO.GetByAccountIdsAsync(accountIds, maxCount), options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RolesOfUserModel>> GetByAddedDateMonthAndYearAsync(int year, int month, RolesOfUserNavigationOptions? options, int? maxGetCount)
            => await _byTimeService.GetByMonthAndYearGenericAsync(
                _roleOfUserDAO.GetByMonthAndYearAsync, year, month, options, $"No roles found added in {month}/{year}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RolesOfUserModel>> GetByAddedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, RolesOfUserNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
            => await _byTimeService.GetByDateTimeGenericAsync((compareType, maxGetCount) => _roleOfUserDAO.GetByDateTimeAsync(dateTime, compareType, maxGetCount), 
                compareType, options, $"No roles found added at {dateTime} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RolesOfUserModel>> GetByAddedDateTimeRangeAsync(DateTime startDate, DateTime endDate, RolesOfUserNavigationOptions? options, int? maxGetCount)
            => await _byTimeService.GetByDateTimeRangeGenericAsync((maxGetCount) => _roleOfUserDAO.GetByDateTimeRangeAsync(startDate, endDate, maxGetCount), 
                options, $"No account roles found created between {startDate} and {endDate}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RolesOfUserModel>> GetByAddedYearAsync<TCompareType>(int year, TCompareType compareType, RolesOfUserNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
            => await _byTimeService.GetByDateTimeGenericAsync((compareType, maxGetCount) => _roleOfUserDAO.GetByYearAsync(year, compareType, maxGetCount), 
                compareType, options, $"No roles found added in year {year} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResult<RolesOfUserModel>> GetByKeysAsync(RolesOfUserKey keys, RolesOfUserNavigationOptions? options)
            => await _getSingleService.GetAsync(keys, _roleOfUserDAO.GetByKeysAsync, options);

        public async Task<ServiceResults<RolesOfUserModel>> GetByListKeysAsync(IEnumerable<RolesOfUserKey> listKeys, RolesOfUserNavigationOptions? options, int? maxGetCount)
            => await _getMultipleService.GetManyAsync((maxCount) => _roleOfUserDAO.GetByListKeysAsync(listKeys, maxCount), options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RolesOfUserModel>> GetByRoleIdAsync(uint roleId, RolesOfUserNavigationOptions? options, int? maxGetCount)
            => await _getMultipleService.GetManyAsync((maxCount) => _roleOfUserDAO.GetByRoleIdAsync(roleId, maxCount), options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RolesOfUserModel>> GetByRoleIdsAsync(IEnumerable<uint> roleIds, RolesOfUserNavigationOptions? options, int? maxGetCount)
            => await _getMultipleService.GetManyAsync((maxCount) => _roleOfUserDAO.GetByRoleIdsAsync(roleIds, maxCount), options, _helper.GetValidMaxRecord(maxGetCount));
    }
}
