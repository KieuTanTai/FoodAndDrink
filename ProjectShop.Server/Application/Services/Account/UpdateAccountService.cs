using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Application.Services.Account
{
    public class UpdateAccountService : IUpdateAccountService
    {
        private readonly IDAO<AccountModel> _baseDAO;
        private readonly IAccountDAO<AccountModel> _accountDAO;
        private readonly IHashPassword _hashPassword;
        private readonly ILogService _logger;

        public UpdateAccountService(IDAO<AccountModel> baseDAO, IAccountDAO<AccountModel> accountDAO, IHashPassword hashPassword, ILogService logger)
        {
            _logger = logger;
            _baseDAO = baseDAO;
            _accountDAO = accountDAO;
            _hashPassword = hashPassword;
        }

        public async Task<JsonLogEntry> UpdateAccountStatusAsync(uint accountId, bool status)
            => await UpdateAccountStatusAsync(accountId.ToString(), status, _baseDAO.GetSingleDataAsync);

        public async Task<JsonLogEntry> UpdateAccountStatusByUserNameAsync(string userName, bool status)
            => await UpdateAccountStatusAsync(userName, status, _accountDAO.GetByUserNameAsync);

        public async Task<IEnumerable<JsonLogEntry>> UpdateAccountStatusByUserNamesAsync(IEnumerable<string> userNames, bool status)
            => await UpdateAccountStatusAsync(userNames, status, _accountDAO.GetByUserNameAsync);

        public async Task<IEnumerable<JsonLogEntry>> UpdateAccountStatusAsync(IEnumerable<uint> accountIds, bool status)
            => await UpdateAccountStatusAsync(accountIds.Select(id => id.ToString()), status, _baseDAO.GetByInputsAsync);

        public async Task<JsonLogEntry> UpdateAccountPasswordAsync(uint accountId, string newPassword)
            => await UpdateAccountPasswordAsync(accountId.ToString(), newPassword, _baseDAO.GetSingleDataAsync);

        public async Task<JsonLogEntry> UpdateAccountPasswordByUserNameAsync(string userName, string newPassword)
            => await UpdateAccountPasswordAsync(userName, newPassword, _accountDAO.GetByUserNameAsync);

        public async Task<IEnumerable<JsonLogEntry>> UpdateAccountPasswordByUserNamesAsync(IEnumerable<string> userNames, IEnumerable<string> newPasswords)
            => await UpdateAccountPasswordAsync(userNames, newPasswords, _accountDAO.GetByUserNameAsync);

        public async Task<IEnumerable<JsonLogEntry>> UpdateAccountPasswordAsync(IEnumerable<uint> accountIds, IEnumerable<string> newPasswords)
            => await UpdateAccountPasswordAsync(accountIds.Select(id => id.ToString()), newPasswords, _baseDAO.GetByInputsAsync);

        private async Task<JsonLogEntry> UpdateAccountPasswordAsync(string input, string newPassword, Func<string, Task<AccountModel?>> daoFunc)
        {
            try
            {
                AccountModel? account = await daoFunc(input);
                if (account == null)
                    return _logger.JsonLogWarning<AccountModel, UpdateAccountService>($"Account with input {input} does not exist.", null);

                if (!await _hashPassword.IsPasswordValidAsync(newPassword))
                    account.Password = await _hashPassword.HashPasswordAsync(newPassword);
                account.Password = newPassword;
                int affectedRows = await _baseDAO.UpdateAsync(account);
                if (affectedRows == 0)
                    return _logger.JsonLogWarning<AccountModel, UpdateAccountService>($"Failed to update the account password for {input}.");
                return _logger.JsonLogInfo<AccountModel, UpdateAccountService>($"Updated password for account {input}.", affectedRows: affectedRows);
            }
            catch (Exception ex)
            {
                return _logger.JsonLogError<AccountModel, UpdateAccountService>($"An error occurred while updating the account password for {input}.", ex);
            }
        }

        private async Task<IEnumerable<JsonLogEntry>> UpdateAccountPasswordAsync(IEnumerable<string> inputs, IEnumerable<string> newPasswords, Func<IEnumerable<string>, int?, Task<IEnumerable<AccountModel>>> daoFunc)
        {
            List<JsonLogEntry> logEntries = [];
            try
            {
                IEnumerable<AccountModel> accounts = await daoFunc(inputs, null);
                if (accounts == null || !accounts.Any())
                {
                    logEntries.Add(_logger.JsonLogWarning<AccountModel, UpdateAccountService>("No accounts found for the provided inputs."));
                    return logEntries;
                }

                using (var enumerator = accounts.GetEnumerator())
                {
                    foreach (string newPassword in newPasswords)
                    {
                        if (!enumerator.MoveNext())
                        {
                            logEntries.Add(_logger.JsonLogWarning<AccountModel, UpdateAccountService>("The number of new passwords exceeds the number of accounts found."));
                            return logEntries;
                        }

                        AccountModel account = enumerator.Current;
                        if (!await _hashPassword.IsPasswordValidAsync(newPassword))
                            account.Password = await _hashPassword.HashPasswordAsync(newPassword);
                        account.Password = newPassword;
                        logEntries.Add(_logger.JsonLogInfo<AccountModel, UpdateAccountService>($"Prepared password update for account {account.UserName}."));
                    }
                }
                int affectedRows = await _baseDAO.UpdateAsync(accounts);
                if (affectedRows == 0)
                {
                    logEntries.Add(_logger.JsonLogWarning<AccountModel, UpdateAccountService>("Failed to update passwords for multiple accounts."));
                    return logEntries;
                }

                logEntries.Add(_logger.JsonLogInfo<AccountModel, UpdateAccountService>("Updated passwords for multiple accounts.", affectedRows: affectedRows));
                return logEntries;
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<AccountModel, UpdateAccountService>("An error occurred while updating the account passwords.", ex));
                return logEntries;
            }
        }

        private async Task<JsonLogEntry> UpdateAccountStatusAsync(string input, bool status, Func<string, Task<AccountModel?>> daoFunc)
        {
            try
            {
                AccountModel? account = await daoFunc(input);
                if (account == null)
                    return _logger.JsonLogWarning<AccountModel, UpdateAccountService>($"Account with input {input} does not exist.", null);

                account.AccountStatus = status;
                int affectedRows = await _baseDAO.UpdateAsync(account);
                if (affectedRows == 0)
                    return _logger.JsonLogWarning<AccountModel, UpdateAccountService>($"Failed to update the account status for {input}.");
                return _logger.JsonLogInfo<AccountModel, UpdateAccountService>($"Updated status for account {input}.", affectedRows: affectedRows);
            }
            catch (Exception ex)
            {
                return _logger.JsonLogError<AccountModel, UpdateAccountService>($"An error occurred while updating the account status for {input}.", ex);
            }
        }

        private async Task<IEnumerable<JsonLogEntry>> UpdateAccountStatusAsync(IEnumerable<string> inputs, bool status, Func<IEnumerable<string>, int?, Task<IEnumerable<AccountModel>>> daoFunc)
        {
            List<JsonLogEntry> logEntries = [];
            try
            {
                IEnumerable<AccountModel> accounts = await daoFunc(inputs, null);
                if (accounts == null || !accounts.Any())
                {
                    logEntries.Add(_logger.JsonLogWarning<AccountModel, UpdateAccountService>("No accounts found for the provided inputs."));
                    return logEntries;
                }

                foreach (AccountModel account in accounts)
                {
                    account.AccountStatus = status;
                    logEntries.Add(_logger.JsonLogInfo<AccountModel, UpdateAccountService>($"Prepared status update for account {account.UserName}."));
                }
                int affectedRows = await _baseDAO.UpdateAsync(accounts);
                if (affectedRows == 0)
                {
                    logEntries.Add(_logger.JsonLogWarning<AccountModel, UpdateAccountService>("Failed to update statuses for multiple accounts."));
                    return logEntries;
                }

                logEntries.Add(_logger.JsonLogInfo<AccountModel, UpdateAccountService>("Updated statuses for multiple accounts.", affectedRows: affectedRows));
                return logEntries;
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<AccountModel, UpdateAccountService>("An error occurred while updating the account statuses.", ex));
                return logEntries;
            }
        }
    }
}
