using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Account
{
    public class UpdateAccountService : BaseHelperService<AccountModel>, IUpdateAccountService
    {
        private readonly IDAO<AccountModel> _baseDAO;
        private readonly IAccountDAO<AccountModel> _accountDAO;

        public UpdateAccountService(IDAO<AccountModel> baseDAO, IAccountDAO<AccountModel> accountDAO)
        {
            _baseDAO = baseDAO ?? throw new ArgumentNullException(nameof(baseDAO), "Base DAO cannot be null.");
            _accountDAO = accountDAO ?? throw new ArgumentNullException(nameof(accountDAO), "Account DAO cannot be null.");
        }

        public async Task<int> UpdateAccountStatusAsync(uint accountId, bool status)
            => await UpdateAccountStatusAsync(accountId.ToString(), status, _baseDAO.GetSingleDataAsync);

        public async Task<int> UpdateAccountStatusByUserNameAsync(string userName, bool status)
            => await UpdateAccountStatusAsync(userName, status, _accountDAO.GetByUserNameAsync);

        public async Task<int> UpdateAccountStatusByUserNamesAsync(IEnumerable<string> userNames, bool status)
            => await UpdateAccountStatusAsync(userNames, status, _accountDAO.GetByUserNameAsync);

        public async Task<int> UpdateAccountStatusAsync(IEnumerable<uint> accountIds, bool status)
            => await UpdateAccountStatusAsync(accountIds.Select(id => id.ToString()), status, _baseDAO.GetByInputsAsync);

        public async Task<int> UpdateAccountPasswordAsync(uint accountId, string newPassword)
            => await UpdateAccountPasswordAsync(accountId.ToString(), newPassword, _baseDAO.GetSingleDataAsync);

        public async Task<int> UpdateAccountPasswordByUserNameAsync(string userName, string newPassword)
            => await UpdateAccountPasswordAsync(userName, newPassword, _accountDAO.GetByUserNameAsync);

        public async Task<int> UpdateAccountPasswordByUserNamesAsync(IEnumerable<string> userNames, IEnumerable<string> newPasswords)
            => await UpdateAccountPasswordAsync(userNames, newPasswords, _accountDAO.GetByUserNameAsync);

        public async Task<int> UpdateAccountPasswordAsync(IEnumerable<uint> accountIds, IEnumerable<string> newPasswords)
            => await UpdateAccountPasswordAsync(accountIds.Select(id => id.ToString()), newPasswords, _baseDAO.GetByInputsAsync);

        private async Task<int> UpdateAccountPasswordAsync(string input, string newPassword, Func<string, Task<AccountModel?>> daoFunc)
        {
            try
            {
                AccountModel? account = await daoFunc(input);
                if (account == null)
                    throw new InvalidOperationException($"Account with input {input} does not exist.");
                if (!await hashPassword.IsPasswordValidAsync(newPassword))
                    account.Password = await hashPassword.HashPasswordAsync(newPassword);
                account.Password = newPassword; // Assuming password is already hashed before this call
                int affectedRows = await _baseDAO.UpdateAsync(account);
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while updating the account password for {input}.", ex);
            }
        }

        private async Task<int> UpdateAccountPasswordAsync(IEnumerable<string> inputs, IEnumerable<string> newPasswords, Func<IEnumerable<string>, int?, Task<IEnumerable<AccountModel>>> daoFunc)
        {
            try
            {
                IEnumerable<AccountModel> accounts = await daoFunc(inputs, null);
                using (var enumerator = accounts.GetEnumerator())
                {
                    foreach (string newPassword in newPasswords)
                    {
                        if (!enumerator.MoveNext())
                            throw new InvalidOperationException("The number of new passwords does not match the number of accounts.");
                        AccountModel account = enumerator.Current;
                        if (!await hashPassword.IsPasswordValidAsync(newPassword))
                            account.Password = await hashPassword.HashPasswordAsync(newPassword);
                        account.Password = newPassword; // Assuming password is already hashed before this call
                    }
                }
                int affectedRows = await _baseDAO.UpdateAsync(accounts);
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the account passwords.", ex);
            }
        }

        private async Task<int> UpdateAccountStatusAsync(string input, bool status, Func<string, Task<AccountModel?>> daoFunc)
        {
            try
            {
                AccountModel? account = await daoFunc(input);
                if (account == null)
                    throw new InvalidOperationException($"Account with input {input} does not exist.");
                account.AccountStatus = status;
                int affectedRows = await _baseDAO.UpdateAsync(account);
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while updating the account status for {input}.", ex);
            }
        }

        private async Task<int> UpdateAccountStatusAsync(IEnumerable<string> inputs, bool status, Func<IEnumerable<string>, int?, Task<IEnumerable<AccountModel>>> daoFunc)
        {
            try
            {
                IEnumerable<AccountModel> accounts = await daoFunc(inputs, null);
                foreach (AccountModel account in accounts)
                    account.AccountStatus = status;
                int affectedRows = await _baseDAO.UpdateAsync(accounts);
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the account statuses.", ex);
            }
        }
    }
}
