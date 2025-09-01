using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class SearchRoleService : ISearchRoleServices<RoleModel, RoleNavigationOptions>
    {
        private readonly IDAO<RoleModel> _baseDAO;
        private readonly IRoleDAO<RoleModel> _roleDAO;
        private readonly IBaseHelperServices<RoleModel> _helper;
        private readonly IBaseGetByTimeServices<RoleModel, RoleNavigationOptions> _byTimeService;
        private readonly IGetSingleServices<RoleModel, RoleNavigationOptions, SearchRoleService> _getSingleService;
        private readonly IGetMultipleServices<RoleModel, RoleNavigationOptions, SearchRoleService> _getMultipleService;

        public SearchRoleService(IDAO<RoleModel> baseDAO, IRoleDAO<RoleModel> roleDAO, 
            IBaseHelperServices<RoleModel> helper,
            IBaseGetByTimeServices<RoleModel, RoleNavigationOptions> byTimeService,
            IGetMultipleServices<RoleModel, RoleNavigationOptions, SearchRoleService> getMultipleService,
            IGetSingleServices<RoleModel, RoleNavigationOptions, SearchRoleService> getSingleService)
        {
            _helper = helper;
            _roleDAO = roleDAO;
            _baseDAO = baseDAO;
            _byTimeService = byTimeService;
            _getMultipleService = getMultipleService;
            _getSingleService = getSingleService;
        }

        public async Task<ServiceResults<RoleModel>> GetAllAsync(RoleNavigationOptions? options, int? maxGetCount)
            => await _getMultipleService.GetManyAsync(_baseDAO.GetAllAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RoleModel>> GetByCreatedDateMonthAndYearAsync(int year, int month, RoleNavigationOptions? options, int? maxGetCount)
            => await _byTimeService.GetByMonthAndYearGenericAsync(
                _roleDAO.GetByMonthAndYearAsync, year, month, options, $"No roles found created in {month}/{year}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RoleModel>> GetByCreatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, RoleNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
            => await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleDAO.GetByDateTimeAsync(dateTime, compareType, maxGetCount), compareType, options, $"No roles found created at {dateTime} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RoleModel>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, RoleNavigationOptions? options, int? maxGetCount)
            => await _byTimeService.GetByDateTimeRangeGenericAsync(
                (maxGetCount) => _roleDAO.GetByDateTimeRangeAsync(startDate, endDate, maxGetCount), options, $"No roles found created between {startDate} and {endDate}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RoleModel>> GetByCreatedYearAsync<TCompareType>(int year, TCompareType compareType, RoleNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
            => await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleDAO.GetByYearAsync(year, compareType, maxGetCount), compareType, options, $"No roles found created in year {year} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RoleModel>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, RoleNavigationOptions? options, int? maxGetCount)
           => await _byTimeService.GetByMonthAndYearGenericAsync(
                _roleDAO.GetByMonthAndYearLastUpdatedDateAsync, year, month, options, $"No roles found updated in {month}/{year}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RoleModel>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, RoleNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
            => await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleDAO.GetByLastUpdatedDateAsync(dateTime, compareType, maxGetCount), compareType, options, $"No roles found updated at {dateTime} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RoleModel>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, RoleNavigationOptions? options, int? maxGetCount)
            => await _byTimeService.GetByDateTimeRangeGenericAsync(
                (maxGetCount) => _roleDAO.GetByLastUpdatedDateRangeAsync(startDate, endDate, maxGetCount), options, $"No roles found updated between {startDate} and {endDate}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RoleModel>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, RoleNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
            => await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleDAO.GetByYearLastUpdatedDateAsync(year, compareType, maxGetCount), compareType, options, $"No roles found updated in year {year} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResult<RoleModel>> GetByRoleIdAsync(uint roleId, RoleNavigationOptions? options)
            => await _getSingleService.GetAsync(roleId, (id) => _baseDAO.GetSingleDataAsync(id.ToString()), options);

        public async Task<ServiceResult<RoleModel>> GetByRoleNameAsync(string roleName, RoleNavigationOptions? options)
            => await _getSingleService.GetAsync(roleName, _roleDAO.GetByRoleNameAsync, options);

        public async Task<ServiceResults<RoleModel>> GetByStatusAsync(bool status, RoleNavigationOptions? options, int? maxGetCount)
            => await _getMultipleService.GetManyAsync((maxCount) => _roleDAO.GetByStatusAsync(status, maxCount), options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RoleModel>> GetRelativeByRoleName(string roleName, RoleNavigationOptions? options, int? maxGetCount)
            => await _getMultipleService.GetManyAsync((maxCount) => _roleDAO.GetByLikeStringAsync(roleName, maxCount), options, _helper.GetValidMaxRecord(maxGetCount));
    }
}
