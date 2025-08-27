using Org.BouncyCastle.Ocsp;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;
using ProjectShop.Server.Core.ObjectValue.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.Account
{
    public class SearchAccountService : ISearchAccountService<AccountModel, AccountNavigationOptions>
    {
        private readonly ILogService _logger;
        private readonly IDAO<AccountModel> _baseDAO;
        private readonly IAccountDAO<AccountModel> _accountDAO;
        private readonly IBaseHelperService<AccountModel> _helper;
        private readonly IServiceResultFactory<SearchAccountService> _serviceResultFactory;
        private readonly IBaseGetByTimeService<AccountModel, AccountNavigationOptions> _byTimeService;
        private readonly IBaseGetNavigationPropertyService<AccountModel, AccountNavigationOptions> _navigationService;

        public SearchAccountService(IDAO<AccountModel> baseDAO, IAccountDAO<AccountModel> accountDAO,
            ILogService logger,
            IBaseHelperService<AccountModel> helper,
            IBaseGetByTimeService<AccountModel, AccountNavigationOptions> byTimeService,
            IBaseGetNavigationPropertyService<AccountModel, AccountNavigationOptions> navigationService,
            IServiceResultFactory<SearchAccountService> serviceResultFactory)
        {
            _helper = helper;
            _logger = logger;
            _baseDAO = baseDAO;
            _accountDAO = accountDAO;
            _byTimeService = byTimeService;
            _navigationService = navigationService;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResults<AccountModel>> GetAllAsync(AccountNavigationOptions? options, int? maxGetCount)
        {
            ServiceResults<AccountModel> results = new();
            try
            {
                IEnumerable<AccountModel> accounts = await _baseDAO.GetAllAsync(_helper.GetValidMaxRecord(maxGetCount));
                if (accounts == null || !accounts.Any())
                    return _serviceResultFactory.CreateServiceResults<AccountModel>("No accounts found.", [], false);

                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(accounts, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<AccountModel, SearchAccountService>($"Retrieved all accounts with maxGetCount={maxGetCount}, options={options}."));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<AccountModel>("An error occurred while retrieving all accounts.", [], false, ex);
            }
        }

        public async Task<ServiceResults<AccountModel>> GetByStatusAsync(bool status, AccountNavigationOptions? options, int? maxGetCount)
        {
            ServiceResults<AccountModel> results = new();
            try
            {
                IEnumerable<AccountModel> accounts = await _accountDAO.GetByStatusAsync(status, _helper.GetValidMaxRecord(maxGetCount));
                if (accounts == null || !accounts.Any())
                    return _serviceResultFactory.CreateServiceResults<AccountModel>($"No accounts found with status={status}.", [], false);

                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(accounts, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<AccountModel, SearchAccountService>($"Retrieved accounts by status={status} with maxGetCount={maxGetCount}, options={options}."));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<AccountModel>($"An error occurred while retrieving accounts by status={status}.", [], false, ex);
            }
        }

        public async Task<ServiceResult<AccountModel>> GetByUserNameAsync(string userName, AccountNavigationOptions? options)
        {
            ServiceResult<AccountModel> result = new();
            try
            {
                AccountModel? account = await _accountDAO.GetByUserNameAsync(userName);
                if (account == null)
                    return _serviceResultFactory.CreateServiceResult<AccountModel>($"No account found with username: {userName}.", new AccountModel(), false);

                if (options != null)
                    result = await _navigationService.GetNavigationPropertyByOptionsAsync(account, options);
                result.LogEntries = result.LogEntries!.Append(_logger.JsonLogInfo<AccountModel, SearchAccountService>($"Retrieved account by username={userName} with options={options}."));
                return result;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult<AccountModel>($"An error occurred while retrieving account by username: {userName}.", new AccountModel(), false, ex);
            }
        }

        public async Task<ServiceResult<AccountModel>> GetByAccountIdAsync(uint accountId, AccountNavigationOptions? options)
        {
            ServiceResult<AccountModel> result = new();
            try
            {
                AccountModel? account = await _baseDAO.GetSingleDataAsync(accountId.ToString());
                if (account == null)
                    return _serviceResultFactory.CreateServiceResult<AccountModel>($"No account found with ID: {accountId}.", new AccountModel(), false);

                if (options != null)
                    result = await _navigationService.GetNavigationPropertyByOptionsAsync(account, options);
                result.LogEntries = result.LogEntries!.Append(_logger.JsonLogInfo<AccountModel, SearchAccountService>($"Retrieved account by ID={accountId} with options={options}."));
                return result;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult<AccountModel>($"An error occurred while retrieving account by ID: {accountId}.", new AccountModel(), false, ex);
            }
        }

        public async Task<ServiceResults<AccountModel>> GetByCreatedDateMonthAndYearAsync(int year, int month, AccountNavigationOptions? options, int? maxGetCount)
        {
            return await _byTimeService.GetByMonthAndYearGenericAsync(
                _accountDAO.GetByMonthAndYearAsync, year, month, options, $"No accounts found created in {month}/{year}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<ServiceResults<AccountModel>> GetByCreatedYearAsync<TCompareType>(int year, TCompareType compareType, AccountNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await _byTimeService.GetByDateTimeGenericAsync(
                (ct, maxGetCount) => _accountDAO.GetByYearAsync(year, ct, maxGetCount), compareType, options, $"No accounts found created in year {year} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<ServiceResults<AccountModel>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, AccountNavigationOptions? options, int? maxGetCount)
        {
            return await _byTimeService.GetByDateTimeRangeGenericAsync((maxGetCount) => _accountDAO.GetByDateTimeRangeAsync(startDate, endDate, maxGetCount), options, $"No accounts found created between {startDate} and {endDate}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<ServiceResults<AccountModel>> GetByCreatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, AccountNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await _byTimeService.GetByDateTimeGenericAsync(
                (ct, maxGetCount) => _accountDAO.GetByDateTimeAsync(dateTime, ct, maxGetCount), compareType, options, $"No accounts found created at {dateTime} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<ServiceResults<AccountModel>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, AccountNavigationOptions? options, int? maxGetCount)
        {
            return await _byTimeService.GetByMonthAndYearGenericAsync(
                _accountDAO.GetByMonthAndYearLastUpdatedDateAsync, year, month, options, $"No accounts found last updated in {month}/{year}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<ServiceResults<AccountModel>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, AccountNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await _byTimeService.GetByDateTimeGenericAsync(
                (ct, maxGetCount) => _accountDAO.GetByYearLastUpdatedDateAsync(year, ct, maxGetCount), compareType, options, $"No accounts found last updated in year {year} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<ServiceResults<AccountModel>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, AccountNavigationOptions? options, int? maxGetCount)
        {
            return await _byTimeService.GetByDateTimeRangeGenericAsync((maxGet) => _accountDAO.GetByLastUpdatedDateRangeAsync(startDate, endDate, maxGet), options, $"No accounts found last updated between {startDate} and {endDate}.", _helper.GetValidMaxRecord(maxGetCount));
        }

        public async Task<ServiceResults<AccountModel>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, AccountNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
        {
            return await _byTimeService.GetByDateTimeGenericAsync(
                (ct, maxGetCount) => _accountDAO.GetByLastUpdatedDateAsync(dateTime, ct, maxGetCount), compareType, options, $"No accounts found last updated at {dateTime} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));
        }
    }
}
