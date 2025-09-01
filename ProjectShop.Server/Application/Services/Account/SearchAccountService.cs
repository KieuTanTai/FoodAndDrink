using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.Account
{
    public class SearchAccountService : ISearchAccountServices<AccountModel, AccountNavigationOptions>
    {
        private readonly IDAO<AccountModel> _baseDAO;
        private readonly IAccountDAO<AccountModel> _accountDAO;
        private readonly IBaseHelperServices<AccountModel> _helper;
        private readonly IGetSingleServices<AccountModel, AccountNavigationOptions, SearchAccountService> _getSingleService;
        private readonly IGetMultipleServices<AccountModel, AccountNavigationOptions, SearchAccountService> _getMultipleService;
        private readonly IBaseGetByTimeServices<AccountModel, AccountNavigationOptions> _byTimeService;

        public SearchAccountService(IDAO<AccountModel> baseDAO, IAccountDAO<AccountModel> accountDAO,
            IBaseHelperServices<AccountModel> helper,
            IBaseGetByTimeServices<AccountModel, AccountNavigationOptions> byTimeService,
            IGetSingleServices<AccountModel, AccountNavigationOptions, SearchAccountService> getSingleService,
            IGetMultipleServices<AccountModel, AccountNavigationOptions, SearchAccountService> getMultipleService)
        {
            _helper = helper;
            _baseDAO = baseDAO;
            _accountDAO = accountDAO;
            _byTimeService = byTimeService;
            _getSingleService = getSingleService;
            _getMultipleService = getMultipleService;
        }

        public async Task<ServiceResults<AccountModel>> GetAllAsync(AccountNavigationOptions? options, int? maxGetCount)
            => await _getMultipleService.GetManyAsync(_baseDAO.GetAllAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<AccountModel>> GetByStatusAsync(bool status, AccountNavigationOptions? options, int? maxGetCount)
            => await _getMultipleService.GetManyAsync(status, _accountDAO.GetByStatusAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResult<AccountModel>> GetByUserNameAsync(string userName, AccountNavigationOptions? options)
            => await _getSingleService.GetAsync(userName, _accountDAO.GetByUserNameAsync, options);

        public async Task<ServiceResult<AccountModel>> GetByAccountIdAsync(uint accountId, AccountNavigationOptions? options)
            => await _getSingleService.GetAsync(accountId, (accountId) => _baseDAO.GetSingleDataAsync(accountId.ToString()), options);

        public async Task<ServiceResults<AccountModel>> GetByCreatedDateMonthAndYearAsync(int year, int month, AccountNavigationOptions? options, int? maxGetCount)
            => await _byTimeService.GetByMonthAndYearGenericAsync(_accountDAO.GetByMonthAndYearAsync, year, month, options,
                $"No accounts found created in {month}/{year}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<AccountModel>> GetByCreatedYearAsync<TCompareType>(int year, TCompareType compareType, AccountNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
            => await _byTimeService.GetByDateTimeGenericAsync((type, maxGet) => _accountDAO.GetByYearAsync(year, type, maxGet),
                compareType, options, $"No accounts found created in year {year} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<AccountModel>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, AccountNavigationOptions? options, int? maxGetCount)
            => await _byTimeService.GetByDateTimeRangeGenericAsync((maxGet) => _accountDAO.GetByDateTimeRangeAsync(startDate, endDate, maxGet),
                options, $"No accounts found created between {startDate} and {endDate}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<AccountModel>> GetByCreatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, AccountNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
            => await _byTimeService.GetByDateTimeGenericAsync((type, maxGet) => _accountDAO.GetByDateTimeAsync(dateTime, type, maxGet),
                compareType, options, $"No accounts found created at {dateTime} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<AccountModel>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, AccountNavigationOptions? options, int? maxGetCount)
            => await _byTimeService.GetByMonthAndYearGenericAsync(_accountDAO.GetByMonthAndYearLastUpdatedDateAsync, year, month, options,
                $"No accounts found last updated in {month}/{year}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<AccountModel>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, AccountNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
            => await _byTimeService.GetByDateTimeGenericAsync((ct, maxGet) => _accountDAO.GetByYearLastUpdatedDateAsync(year, ct, maxGet),
                compareType, options, $"No accounts found last updated in year {year} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<AccountModel>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, AccountNavigationOptions? options, int? maxGetCount)
            => await _byTimeService.GetByDateTimeRangeGenericAsync((maxGet) => _accountDAO.GetByLastUpdatedDateRangeAsync(startDate, endDate, maxGet),
                options, $"No accounts found last updated between {startDate} and {endDate}.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<AccountModel>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, AccountNavigationOptions? options, int? maxGetCount) where TCompareType : Enum
            => await _byTimeService.GetByDateTimeGenericAsync((ct, maxGet) => _accountDAO.GetByLastUpdatedDateAsync(dateTime, ct, maxGet),
                compareType, options, $"No accounts found last updated at {dateTime} with comparison type {compareType}.", _helper.GetValidMaxRecord(maxGetCount));
    }
}
