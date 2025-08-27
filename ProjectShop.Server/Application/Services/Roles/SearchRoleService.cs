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
    public class SearchRoleService : ISearchRoleService<RoleModel, RoleNavigationOptions>
    {
        private readonly IDAO<RoleModel> _baseDAO;
        private readonly IRoleDAO<RoleModel> _roleDAO;
        private readonly IBaseHelperService<RoleModel> _helper;
        private readonly IBaseGetNavigationPropertyService<RoleModel, RoleNavigationOptions> _navigationService;
        private readonly IBaseGetByTimeService<RoleModel, RoleNavigationOptions> _byTimeService;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<SearchRoleService> _serviceResultFactory;

        public SearchRoleService(IDAO<RoleModel> baseDAO, IRoleDAO<RoleModel> roleDAO,
            IBaseGetNavigationPropertyService<RoleModel, RoleNavigationOptions> navigationService, IBaseHelperService<RoleModel> helper, 
            IBaseGetByTimeService<RoleModel, RoleNavigationOptions> byTimeService,
            ILogService logger, IServiceResultFactory<SearchRoleService> serviceResultFactory)
        {
            _helper = helper;
            _logger = logger;
            _roleDAO = roleDAO;
            _baseDAO = baseDAO;
            _byTimeService = byTimeService;
            _navigationService = navigationService;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResults<RoleModel>> GetAllAsync(RoleNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RoleModel> roles = await _baseDAO.GetAllAsync(_helper.GetValidMaxRecord(maxGetCount));
                if (roles == null || !roles.Any())
                    return _serviceResultFactory.CreateServiceResults<RoleModel>("No roles found.", [], false);

                ServiceResults<RoleModel> results = new ServiceResults<RoleModel>();
                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(roles, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<RoleModel, SearchRoleService>($"{results.Data!.Count()} roles retrieved successfully."));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RoleModel>("An error occurred while retrieving all roles.", [], false, ex);
            }
        }

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
                _roleDAO.GetByLastUpdatedMonthAndYearAsync, year, month, options, $"No roles found updated in {month}/{year}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RoleModel>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, RoleNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
            => await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleDAO.GetByLastUpdatedDateAsync(dateTime, compareType, maxGetCount), compareType, options, $"No roles found updated at {dateTime} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RoleModel>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, RoleNavigationOptions? options, int? maxGetCount)
            => await _byTimeService.GetByDateTimeRangeGenericAsync(
                (maxGetCount) => _roleDAO.GetByLastUpdatedDateTimeRangeAsync(startDate, endDate, maxGetCount), options, $"No roles found updated between {startDate} and {endDate}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<RoleModel>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, RoleNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
            => await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleDAO.GetByLastUpdatedYearAsync(year, compareType, maxGetCount), compareType, options, $"No roles found updated in year {year} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResult<RoleModel>> GetByRoleIdAsync(uint roleId, RoleNavigationOptions? options)
        {
            try
            {
                RoleModel? role = await _baseDAO.GetSingleDataAsync(roleId.ToString());
                if (role == null)
                    return _serviceResultFactory.CreateServiceResult<RoleModel>($"No role found with ID: {roleId}.", new RoleModel(), false);

                ServiceResult<RoleModel> result = new ServiceResult<RoleModel>();
                if (options != null)
                    result = await _navigationService.GetNavigationPropertyByOptionsAsync(role, options);
                result.LogEntries = result.LogEntries!.Append(_logger.JsonLogInfo<RoleModel, SearchRoleService>($"Role with ID: {roleId} retrieved successfully."));
                return result;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult<RoleModel>("An error occurred while retrieving role by ID.", new RoleModel(), false, ex);
            }
        }

        public async Task<ServiceResult<RoleModel>> GetByRoleNameAsync(string roleName, RoleNavigationOptions? options)
        {
            try
            {
                RoleModel? role = await _roleDAO.GetByRoleNameAsync(roleName);
                if (role == null)
                    return _serviceResultFactory.CreateServiceResult<RoleModel>($"No role found with name: {roleName}.", new RoleModel(), false);

                ServiceResult<RoleModel> result = new ServiceResult<RoleModel>();
                if (options != null)
                    result = await _navigationService.GetNavigationPropertyByOptionsAsync(role, options);
                result.LogEntries = result.LogEntries!.Append(_logger.JsonLogInfo<RoleModel, SearchRoleService>($"Role with name: {roleName} retrieved successfully."));
                return result;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult<RoleModel>("An error occurred while retrieving role by name.", new RoleModel(), false, ex);
            }
        }

        public async Task<ServiceResults<RoleModel>> GetByStatusAsync(bool status, RoleNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RoleModel> roles = await _roleDAO.GetByStatusAsync(status, _helper.GetValidMaxRecord(maxGetCount));
                if (roles == null || !roles.Any())
                    return _serviceResultFactory.CreateServiceResults<RoleModel>("No roles found.", [], false);

                ServiceResults<RoleModel> results = new ServiceResults<RoleModel>();
                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(roles, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<RoleModel, SearchRoleService>($"{results.Data!.Count()} roles with status: {status} retrieved successfully."));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RoleModel>("An error occurred while retrieving roles by status.", [], false, ex);
            }
        }

        public async Task<ServiceResults<RoleModel>> GetRelativeByRoleName(string roleName, RoleNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RoleModel> roles = await _roleDAO.GetByLikeStringAsync(roleName, _helper.GetValidMaxRecord(maxGetCount));
                if (roles == null || !roles.Any())
                    return _serviceResultFactory.CreateServiceResults<RoleModel>("No roles found relative to the role name.", [], false);

                ServiceResults<RoleModel> results = new ServiceResults<RoleModel>();
                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(roles, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<RoleModel, SearchRoleService>($"{results.Data!.Count()} roles relative to the role name: {roleName} retrieved successfully."));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RoleModel>("An error occurred while retrieving roles relative to the role name.", [], false, ex);
            }
        }
    }
}
