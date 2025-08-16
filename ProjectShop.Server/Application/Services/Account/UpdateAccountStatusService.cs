using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;

namespace ProjectShop.Server.Application.Services.Account
{
    public class UpdateAccountStatusService : IUpdateAccountStatusService
    {
        private readonly IDAO<AccountModel> _baseDAO;

        public UpdateAccountStatusService(IDAO<AccountModel> baseDAO)
        {
            _baseDAO = baseDAO ?? throw new ArgumentNullException(nameof(baseDAO), "Base DAO cannot be null.");
        }

        public async Task<int> UpdateAccountStatusAsync(string accountId, bool status)
        {
            try
            {
                AccountModel? account = await _baseDAO.GetSingleDataAsync(accountId);
                if (account == null)
                    throw new InvalidOperationException($"Account with ID {accountId} does not exist.");
                account.AccountStatus = status;
                int affectedRows = await _baseDAO.UpdateAsync(account);
                if (affectedRows == 0)
                    throw new InvalidOperationException("Failed to update the account status.");
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the account status.", ex); 
            }
        }

        public async Task<int> UpdateAccountStatusAsync(IEnumerable<string> accountIds, bool status)
        {
            try
            {
                IEnumerable<AccountModel> accounts = await _baseDAO.GetByInputsAsync(accountIds);
                if (accounts == null || !accounts.Any())
                    throw new InvalidOperationException("No accounts found with the provided IDs.");
                foreach (AccountModel account in accounts)
                    account.AccountStatus = status;
                int affectedRows = await _baseDAO.UpdateManyAsync(accounts);
                if (affectedRows == 0)
                    throw new InvalidOperationException("Failed to update the account statuses.");
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the account statuses.", ex);
            }
        }
    }
}
