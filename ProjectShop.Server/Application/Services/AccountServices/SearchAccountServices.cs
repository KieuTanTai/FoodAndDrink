using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.AccountServices
{
    public class SearchAccountServices(IUnitOfWork unit,
        IBaseHelperServices<Account> helper, IServiceResultFactory<SearchAccountServices> serviceResultFactory,
        IBaseGetByTimeServices<Account, AccountNavigationOptions> byTimeService, ILogService logService) : ISearchAccountServices
    {
        private readonly IUnitOfWork _unit = unit;
        private readonly IBaseHelperServices<Account> _helper = helper;
        private readonly ILogService _logService = logService;
        private readonly IBaseGetByTimeServices<Account, AccountNavigationOptions> _byTimeService = byTimeService;
        private readonly IServiceResultFactory<SearchAccountServices> _serviceResultFactory = serviceResultFactory;

        public async Task<ServiceResults<Account>> GetAllAsync(AccountNavigationOptions? options,CancellationToken cancellationToken)
            => await GenericGetEntitiesAsync(_unit.Accounts.GetAllAsync, options, "No accounts found.", cancellationToken);
        public async Task<ServiceResults<Account>> GetByStatusAsync(bool status, AccountNavigationOptions? options,CancellationToken cancellationToken)
            => await GenericGetEntitiesAsync((token) => _unit.Accounts.GetByStatusAsync(status, token), options,
                $"No accounts found with status {status}.", cancellationToken);

        public async Task<ServiceResult<Account>> GetByUserNameAsync(string userName, AccountNavigationOptions? options,CancellationToken cancellationToken)
            => await GenericGetEntityAsync((token) => _unit.Accounts.GetByUserNameAsync(userName, token), options,
                $"No account found with username {userName}.", cancellationToken);

        public async Task<ServiceResult<Account>> GetByAccountIdAsync(uint accountId, AccountNavigationOptions? options,CancellationToken cancellationToken)
            => await GenericGetEntityAsync((token) => _unit.Accounts.GetByIdAsync(accountId, token), options,
                $"No account found with ID {accountId}.", cancellationToken);

        public async Task<ServiceResults<Account>> GetByCreatedDateMonthAndYearAsync(int year, int month, ECompareType compareType, AccountNavigationOptions? options, CancellationToken cancellationToken)
            => await GenericGetEntitiesAsync((token) => _unit.Accounts.GetByCreatedMonthAndYearAsync(year, month, compareType, token), options,
                $"No accounts found created in {month}/{year} with comparison type {compareType}.", cancellationToken);

        public async Task<ServiceResults<Account>> GetByCreatedYearAsync(int year, ECompareType compareType, AccountNavigationOptions? options,CancellationToken cancellationToken) 
            => await GenericGetEntitiesAsync((token) => _unit.Accounts.GetByCreatedYearAsync(year, compareType, token), options,
                $"No accounts found created in year {year} with comparison type {compareType}.", cancellationToken);

        public async Task<ServiceResults<Account>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, AccountNavigationOptions? options, CancellationToken cancellationToken)
            => await GenericGetEntitiesAsync((token) => _unit.Accounts.GetByCreatedDateRangeAsync(startDate, endDate, token), options,
                $"No accounts found created between {startDate} and {endDate}.", cancellationToken);

        public async Task<ServiceResults<Account>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, ECompareType compareType, AccountNavigationOptions? options, CancellationToken cancellationToken)
            => await GenericGetEntitiesAsync((token) => _unit.Accounts.GetByLastUpdatedMonthAndYearAsync(year, month, compareType, token), options,
                $"No accounts found last updated in {month}/{year} with comparison type {compareType}.", cancellationToken);

        public async Task<ServiceResults<Account>> GetByLastUpdatedYearAsync(int year, ECompareType compareType, AccountNavigationOptions? options, CancellationToken cancellationToken)
            => await GenericGetEntitiesAsync((token) => _unit.Accounts.GetByLastUpdatedYearAsync(year, compareType, token), options,
                $"No accounts found last updated in year {year} with comparison type {compareType}.", cancellationToken);

        public async Task<ServiceResults<Account>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, AccountNavigationOptions? options, CancellationToken cancellationToken)
            => await GenericGetEntitiesAsync((token) => _unit.Accounts.GetByLastUpdatedDateRangeAsync(startDate, endDate, token), options,
                $"No accounts found last updated between {startDate} and {endDate}.", cancellationToken);

        // Helper method to reduce code duplication
        private async Task<ServiceResult<Account>> GenericGetEntityAsync(
            Func<CancellationToken, Task<Account?>> getFunc,
            AccountNavigationOptions? options,
            string notFoundMessage,
            CancellationToken cancellationToken = default)
        {
            ServiceResult<Account> result = new(true);
            try
            {
                Account? account = await getFunc(cancellationToken);
                if (account == null)
                {
                    result.IsSuccess = false;
                    result.LogEntries = [_logService.JsonLogWarning<Account, SearchAccountServices>(notFoundMessage)];
                    return result;
                }

                if (options != null)
                    account = await _unit.Accounts.ExplicitLoadAsync(account, options, cancellationToken);
                result.Data = account;
                return result;
            }
            catch (TaskCanceledException ex)
            {
                result.IsSuccess = false;
                result.LogEntries = [_logService.JsonLogError<Account, SearchAccountServices>("The operation was canceled.", ex)];
                return result;
            }
            catch (OperationCanceledException ex)
            {
                result.IsSuccess = false;
                result.LogEntries = [_logService.JsonLogError<Account, SearchAccountServices>("The operation was canceled.", ex)];
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.LogEntries = [_logService.JsonLogError<Account, SearchAccountServices>("An error occurred while retrieving the account.", ex)];
                return result;
            }
        }

        private async Task<ServiceResults<Account>> GenericGetEntitiesAsync(
            Func<CancellationToken, Task<IEnumerable<Account>>> getFunc,
            AccountNavigationOptions? options,
            string notFoundMessage,
            CancellationToken cancellationToken = default)
        {
            ServiceResults<Account> results = new(true);
            try
            {
                IEnumerable<Account> accounts = await getFunc(cancellationToken);
                if (!accounts.Any())
                {
                    results.IsSuccess = false;
                    results.LogEntries = [_logService.JsonLogWarning<Account, SearchAccountServices>(notFoundMessage)];
                    return results;
                }

                if (options != null)
                    accounts = await _unit.Accounts.ExplicitLoadAsync(accounts, options, cancellationToken);
                results.Data = accounts;
                return results;
            }
            catch (TaskCanceledException ex)
            {
                results.IsSuccess = false;
                results.LogEntries = [_logService.JsonLogError<Account, SearchAccountServices>("The operation was canceled.", ex)];
                return results;
            }
            catch (OperationCanceledException ex)
            {
                results.IsSuccess = false;
                results.LogEntries = [_logService.JsonLogError<Account, SearchAccountServices>("The operation was canceled.", ex)];
                return results;
            }
            catch (Exception ex)
            {
                results.IsSuccess = false;
                results.LogEntries = [_logService.JsonLogError<Account, SearchAccountServices>("An error occurred while retrieving accounts.", ex)];
                return results;
            }
        }
    }
}
