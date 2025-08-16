using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using System.Diagnostics;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Account
{
    public class SearchAccountService : ValidateService<AccountModel>, ISearchAccountService<AccountModel>
    {
        private readonly IDAO<AccountModel> _baseDAO;
        private readonly IAccountDAO<AccountModel> _accountDAO;

        public SearchAccountService(
            IDAO<AccountModel> baseDAO,
            IAccountDAO<AccountModel> accountDAO)
        {
            _baseDAO = baseDAO ?? throw new ArgumentNullException(nameof(baseDAO), "Base DAO cannot be null.");
            _accountDAO = accountDAO ?? throw new ArgumentNullException(nameof(accountDAO), "Account DAO cannot be null.");
        }

        public async Task<IEnumerable<AccountModel>> GetAllAsync()
        {
            try
            {
                IEnumerable<AccountModel> accounts = await _baseDAO.GetAllAsync();
                if (accounts == null || !accounts.Any())
                    throw new InvalidOperationException("No accounts found.");
                foreach (AccountModel account in accounts)
                    account.Password = await hashPassword.HashPasswordAsync(account.Password);
                int affectedRows = await _baseDAO.UpdateManyAsync(accounts);
                Debug.WriteLine($"Updated {affectedRows} accounts after retrieving them.");
                return accounts;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving all accounts.", ex);
            }
        }

        public async Task<IEnumerable<AccountModel>> GetByStatusAsync(bool status)
        {
            try
            {
                IEnumerable<AccountModel> accounts = await _accountDAO.GetByStatusAsync(status);
                if (accounts == null || !accounts.Any())
                    throw new InvalidOperationException($"No accounts found with status: {status}.");
                return accounts;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving accounts by status.", ex);
            }
        }

        public async Task<AccountModel> GetByUserNameAsync(string userName)
        {
            try
            {
                AccountModel? account = await _accountDAO.GetByUserNameAsync(userName);
                if (account == null)
                    throw new InvalidOperationException($"No account found with username: {userName}.");
                return account;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving account by username.", ex);
            }
        }

        public async Task<AccountModel> GetByAccountIdAsync(int accountId)
        {
            try
            {
                AccountModel? account = await _baseDAO.GetSingleDataAsync(accountId.ToString());
                if (account == null)
                    throw new InvalidOperationException($"No account found with ID: {accountId}.");
                return account;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving account by ID.", ex);
            }
        }

        public async Task<IEnumerable<AccountModel>> GetByCreatedDateMonthAndYearAsync(int year, int month)
        {
            return await GetByMonthAndYearGenericAsync(
                _accountDAO.GetByCreatedDateAsync, year, month, $"No accounts found created in {month}/{year}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByCreatedYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _accountDAO.GetByCreatedDateAsync(year, ct), compareType, $"No accounts found created in year {year} with comparison type {compareType}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await GetByDateTimeRangeGenericAsync(() => _accountDAO.GetByCreatedDateAsync(startDate, endDate), $"No accounts found created between {startDate} and {endDate}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByCreatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _accountDAO.GetByCreatedDateAsync(dateTime, ct), compareType, $"No accounts found created at {dateTime} with comparison type {compareType}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month)
        {
            return await GetByMonthAndYearGenericAsync(
                _accountDAO.GetByLastUpdatedDateAsync, year, month, $"No accounts found last updated in {month}/{year}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _accountDAO.GetByLastUpdatedDateAsync(year, ct), compareType, $"No accounts found last updated in year {year} with comparison type {compareType}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await GetByDateTimeRangeGenericAsync(() => _accountDAO.GetByLastUpdatedDateAsync(startDate, endDate), $"No accounts found last updated between {startDate} and {endDate}.");
        }

        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType) where TCompareType : Enum
        {
            return await GetByDateTimeGenericAsync(
                (ct) => _accountDAO.GetByLastUpdatedDateAsync(dateTime, ct), compareType, $"No accounts found last updated at {dateTime} with comparison type {compareType}.");
        }

        // DRY
        private async Task<IEnumerable<AccountModel>> GetByDateTimeGenericAsync<TCompareType>(Func<TCompareType, Task<IEnumerable<AccountModel>>> daoFunc, TCompareType compareType, string errorMsg) where TCompareType : Enum
        {
            try
            {
                IEnumerable<AccountModel> results = await daoFunc(compareType);
                if (results == null || !results.Any())
                    throw new InvalidOperationException(errorMsg);
                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }

        private async Task<IEnumerable<AccountModel>> GetByDateTimeRangeGenericAsync(Func<Task<IEnumerable<AccountModel>>> daoFunc, string errorMsg)
        {
            try
            {
                IEnumerable<AccountModel> results = await daoFunc();
                if (results == null || !results.Any())
                    throw new InvalidOperationException(errorMsg);
                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }

        private async Task<IEnumerable<AccountModel>> GetByMonthAndYearGenericAsync(Func<int, int, Task<IEnumerable<AccountModel>>> daoFunc, int year, int month, string errorMsg)
        {
            try
            {
                IEnumerable<AccountModel> results = await daoFunc(year, month);
                if (results == null || !results.Any())
                    throw new InvalidOperationException(errorMsg);
                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }

    }
}
