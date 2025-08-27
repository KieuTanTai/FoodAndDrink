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
        private readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUserDAO;
        private readonly IBaseHelperService<RolesOfUserModel> _helper;
        private readonly IBaseGetNavigationPropertyService<RolesOfUserModel, RolesOfUserNavigationOptions> _navigationService;
        private readonly IBaseGetByTimeService<RolesOfUserModel, RolesOfUserNavigationOptions> _byTimeService;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<RolesOfUserModel> _serviceResultFactory;

        public SearchAccountRoleService(
            IDAO<RolesOfUserModel> baseDAO,
            IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO,
            IBaseHelperService<RolesOfUserModel> helper,
            IBaseGetNavigationPropertyService<RolesOfUserModel, RolesOfUserNavigationOptions> navigationService,
            IBaseGetByTimeService<RolesOfUserModel, RolesOfUserNavigationOptions> byTimeService,
            ILogService logger, IServiceResultFactory<RolesOfUserModel> serviceResultFactory)
        {
            _helper = helper;
            _logger = logger;
            _baseDAO = baseDAO;
            _roleOfUserDAO = roleOfUserDAO;
            _byTimeService = byTimeService;
            _navigationService = navigationService;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetAllAsync(RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RolesOfUserModel> roles = await _baseDAO.GetAllAsync(_helper.GetValidMaxRecord(maxGetCount));
                if (roles == null || !roles.Any())
                    return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("No roles found in the database.", [], false);
                ServiceResults<RolesOfUserModel> results = new();
                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(roles, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<RolesOfUserModel, SearchAccountRoleService>($"Retrieved {roles.Count()} roles from the database."));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("An error occurred while retrieving all roles.", [], false, ex);
            }
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetByAccountIdAsync(uint accountId, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RolesOfUserModel> roles = await _roleOfUserDAO.GetByAccountIdAsync(accountId, _helper.GetValidMaxRecord(maxGetCount));
                if (roles == null || !roles.Any())
                    return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("No roles found in the database.", [], false);
                ServiceResults<RolesOfUserModel> results = new() { Data = roles };
                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(roles, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<RolesOfUserModel, SearchAccountRoleService>($"Retrieved {roles.Count()} roles for account ID {accountId}."));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("An error occurred while retrieving roles by account ID.", [], false, ex);
            }
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RolesOfUserModel> roles = await _roleOfUserDAO.GetByAccountIdsAsync(accountIds, _helper.GetValidMaxRecord(maxGetCount));
                if (roles == null || !roles.Any())
                    return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("No roles found for provided account IDs.", [], false);
                ServiceResults<RolesOfUserModel> results = new() { Data = roles };
                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(roles, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<RolesOfUserModel, SearchAccountRoleService>($"Retrieved {roles.Count()} roles for account IDs."));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("An error occurred while retrieving roles by account IDs.", [], false, ex);
            }
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetByAddedDateMonthAndYearAsync(int year, int month, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            return await _byTimeService.GetByMonthAndYearGenericAsync(
                _roleOfUserDAO.GetByMonthAndYearAsync, year, month, options, $"No roles found added in {month}/{year}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetByAddedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, RolesOfUserNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleOfUserDAO.GetByDateTimeAsync(dateTime, compareType, maxGetCount), compareType, options, $"No roles found added at {dateTime} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetByAddedDateTimeRangeAsync(DateTime startDate, DateTime endDate, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            return await _byTimeService.GetByDateTimeRangeGenericAsync(
                (maxGetCount) => _roleOfUserDAO.GetByDateTimeRangeAsync(startDate, endDate, maxGetCount), options, $"No account roles found created between {startDate} and {endDate}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetByAddedYearAsync<TCompareType>(int year, TCompareType compareType, RolesOfUserNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await _byTimeService.GetByDateTimeGenericAsync(
                (compareType, maxGetCount) => _roleOfUserDAO.GetByYearAsync(year, compareType, maxGetCount), compareType, options, $"No roles found added in year {year} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<ServiceResult<RolesOfUserModel>> GetByKeysAsync(RolesOfUserKey keys, RolesOfUserNavigationOptions? options)
        {
            try
            {
                RolesOfUserModel? rolesOfUser = await _roleOfUserDAO.GetByKeysAsync(keys);
                if (rolesOfUser == null)
                    return _serviceResultFactory.CreateServiceResult<RolesOfUserModel>("No role found for provided keys.", new RolesOfUserModel(), false);

                ServiceResult<RolesOfUserModel> result = new();
                if (options != null)
                    result = await _navigationService.GetNavigationPropertyByOptionsAsync(rolesOfUser, options);
                result.LogEntries = result.LogEntries!.Append(_logger.JsonLogInfo<RolesOfUserModel, SearchAccountRoleService>($"Retrieved role for provided keys."));
                return result;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult<RolesOfUserModel>("An error occurred while retrieving the role by keys.", new RolesOfUserModel(), false, ex);
            }
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetByListKeysAsync(IEnumerable<RolesOfUserKey> listKeys, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RolesOfUserModel> rolesOfUsers = await _roleOfUserDAO.GetByListKeysAsync(listKeys, _helper.GetValidMaxRecord(maxGetCount));
                if (rolesOfUsers == null || !rolesOfUsers.Any())
                    return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("No roles found for provided account IDs.", [], false);

                ServiceResults<RolesOfUserModel> results = new() { Data = rolesOfUsers };
                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(rolesOfUsers, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<RolesOfUserModel, SearchAccountRoleService>($"Retrieved {rolesOfUsers.Count()} roles for provided keys."));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("An error occurred while retrieving the role by keys.", [], false, ex);
            }
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetByRoleIdAsync(uint roleId, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RolesOfUserModel> rolesOfUserModels = await _roleOfUserDAO.GetByRoleIdAsync(roleId, _helper.GetValidMaxRecord(maxGetCount));
                if (rolesOfUserModels == null || !rolesOfUserModels.Any())
                    return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("No roles found for provided account IDs.", [], false);

                ServiceResults<RolesOfUserModel> results = new() { Data = rolesOfUserModels };
                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(rolesOfUserModels, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<RolesOfUserModel, SearchAccountRoleService>($"Retrieved {rolesOfUserModels.Count()} roles for role ID {roleId}."));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("An error occurred while retrieving roles by role ID.", [], false, ex);
            }
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetByRoleIdsAsync(IEnumerable<uint> roleIds, RolesOfUserNavigationOptions? options, int? maxGetCount)
        {
            try
            {
                IEnumerable<RolesOfUserModel> rolesOfUserModels = await _roleOfUserDAO.GetByRoleIdsAsync(roleIds, _helper.GetValidMaxRecord(maxGetCount));
                if (rolesOfUserModels == null || !rolesOfUserModels.Any())
                    return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("No roles found for provided account IDs.", [], false);

                ServiceResults<RolesOfUserModel> results = new() { Data = rolesOfUserModels };
                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(rolesOfUserModels, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<RolesOfUserModel, SearchAccountRoleService>($"Retrieved {rolesOfUserModels.Count()} roles for role IDs."));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("An error occurred while retrieving roles by role IDs.", [], false, ex);
            }
        }
    }
}
