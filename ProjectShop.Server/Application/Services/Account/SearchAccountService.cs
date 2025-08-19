using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Account
{
    public class SearchAccountService : BaseReturnAccountService<AccountModel>, ISearchAccountService<AccountModel, AccountNavigationOptions>
    {


        public SearchAccountService(
            IDAO<AccountModel> baseDAO,
            IAccountDAO<AccountModel> accountDAO,
            IPersonDAO<CustomerModel> customerDAO,
            IPersonDAO<EmployeeModel> employeeDAO,
            IEmployeeDAO<EmployeeModel> specificEmpDAO,
            IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO) : base(baseDAO, accountDAO, customerDAO, employeeDAO, specificEmpDAO, roleOfUserDAO)
        {
        }

        public async Task<IEnumerable<AccountModel>> GetAllAsync(AccountNavigationOptions? options)
        {
            try
            {
                IEnumerable<AccountModel> accounts = await _baseDAO.GetAllAsync();
                if (options != null)
                    accounts = await GetNavigationPropertyByOptions(accounts, options);
                return accounts;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving all accounts.", ex);
            }
        }

        public async Task<IEnumerable<AccountModel>> GetAllAsync(int maxGetCount, AccountNavigationOptions? options)
        {
            try
            {
                IEnumerable<AccountModel> accounts = await _baseDAO.GetAllAsync(maxGetCount);
                if (options != null)
                    accounts = await GetNavigationPropertyByOptions(accounts, options);
                return accounts;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving all accounts with a limit.", ex);
            }
        }

        public async Task<IEnumerable<AccountModel>> GetByStatusAsync(bool status, AccountNavigationOptions? options)
        {
            try
            {
                IEnumerable<AccountModel> accounts = await _accountDAO.GetByStatusAsync(status);
                if (options != null)
                    accounts = await GetNavigationPropertyByOptions(accounts, options);
                return accounts;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving accounts by status.", ex);
            }
        }

        public async Task<IEnumerable<AccountModel>> GetByStatusAsync(bool status, int maxGetCount, AccountNavigationOptions? options)
        {
            try
            {
                IEnumerable<AccountModel> accounts = await _accountDAO.GetByStatusAsync(status, maxGetCount);
                if (options != null)
                    accounts = await GetNavigationPropertyByOptions(accounts, options);
                return accounts;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving accounts by status with a limit.", ex);
            }
        }

        public async Task<AccountModel> GetByUserNameAsync(string userName, AccountNavigationOptions? options)
        {
            try
            {
                AccountModel? account = await _accountDAO.GetByUserNameAsync(userName);
                if (account == null)
                    throw new InvalidOperationException($"No account found with username: {userName}.");
                if (options != null)
                    account = await GetNavigationPropertyByOptions(account, options);
                return account;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving account by username.", ex);
            }
        }

        public async Task<AccountModel> GetByAccountIdAsync(uint accountId, AccountNavigationOptions? options)
        {
            try
            {
                AccountModel? account = await _baseDAO.GetSingleDataAsync(accountId.ToString());
                if (account == null)
                    throw new InvalidOperationException($"No account found with ID: {accountId}.");
                if (options != null)
                    account = await GetNavigationPropertyByOptions(account, options);
                return account;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving account by ID.", ex);
            }
        }

        public async Task<IEnumerable<AccountModel>> GetByCreatedDateMonthAndYearAsync(int year, int month, AccountNavigationOptions? options)
        {
            return await GetByMonthAndYearGenericAsync(
                _accountDAO.GetByCreatedDateAsync, year, month, options, $"No accounts found created in {month}/{year}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByCreatedYearAsync<TCompareType>(int year, TCompareType compareType, AccountNavigationOptions? options) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _accountDAO.GetByCreatedDateAsync(year, ct), compareType, options, $"No accounts found created in year {year} with comparison type {compareType}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, AccountNavigationOptions? options)
        {
            return await GetByDateTimeRangeGenericAsync(() => _accountDAO.GetByCreatedDateAsync(startDate, endDate), options, $"No accounts found created between {startDate} and {endDate}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByCreatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, AccountNavigationOptions? options) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _accountDAO.GetByCreatedDateAsync(dateTime, ct), compareType, options, $"No accounts found created at {dateTime} with comparison type {compareType}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, AccountNavigationOptions? options)
        {
            return await GetByMonthAndYearGenericAsync(
                _accountDAO.GetByLastUpdatedDateAsync, year, month, options, $"No accounts found last updated in {month}/{year}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, AccountNavigationOptions? options) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _accountDAO.GetByLastUpdatedDateAsync(year, ct), compareType, options, $"No accounts found last updated in year {year} with comparison type {compareType}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, AccountNavigationOptions? options)
        {
            return await GetByDateTimeRangeGenericAsync(() => _accountDAO.GetByLastUpdatedDateAsync(startDate, endDate), options, $"No accounts found last updated between {startDate} and {endDate}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, AccountNavigationOptions? options) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _accountDAO.GetByLastUpdatedDateAsync(dateTime, ct), compareType, options, $"No accounts found last updated at {dateTime} with comparison type {compareType}.");
        }

        // DRY
        private async Task<IEnumerable<AccountModel>> GetByDateTimeGenericAsync<TCompareType>(Func<TCompareType, Task<IEnumerable<AccountModel>>> daoFunc, TCompareType compareType, AccountNavigationOptions? options, string errorMsg) where TCompareType : Enum
        {
            try
            {
                IEnumerable<AccountModel> results = await daoFunc(compareType);
                if (options != null)
                    results = await GetNavigationPropertyByOptions(results, options);
                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }

        private async Task<IEnumerable<AccountModel>> GetByDateTimeRangeGenericAsync(Func<Task<IEnumerable<AccountModel>>> daoFunc, AccountNavigationOptions? options, string errorMsg)
        {
            try
            {
                IEnumerable<AccountModel> results = await daoFunc();
                if (options != null)
                    results = await GetNavigationPropertyByOptions(results, options);
                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }

        private async Task<IEnumerable<AccountModel>> GetByMonthAndYearGenericAsync(Func<int, int, Task<IEnumerable<AccountModel>>> daoFunc, int year, int month, AccountNavigationOptions? options, string errorMsg)
        {
            try
            {
                IEnumerable<AccountModel> results = await daoFunc(year, month);
                if (options != null)
                    results = await GetNavigationPropertyByOptions(results, options);
                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }
    }
}
