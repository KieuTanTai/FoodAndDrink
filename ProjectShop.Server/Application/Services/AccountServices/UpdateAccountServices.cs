using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.PlatformRules;

namespace ProjectShop.Server.Application.Services.AccountServices
{
    public class UpdateAccountServices(IUnitOfWork unit, IHashPassword hashPassword, ILogService logger, IBasePasswordMappingServices basePasswordMappingServices,
        IBaseHelperServices<Account> helper)
        : IUpdateAccountServices
    {
        private readonly IUnitOfWork _unit = unit;
        private readonly IHashPassword _hashPassword = hashPassword;
        private readonly ILogService _logger = logger;
        private readonly IBasePasswordMappingServices _basePasswordMappingServices = basePasswordMappingServices;
        private readonly IBaseHelperServices<Account> _helper = helper;

        public async Task<JsonLogEntry> UpdateAccountStatusAsync(uint accountId, bool status, CancellationToken cancellationToken)
            => await UpdateAccountStatusAsync(accountId.ToString(), status, _unit.Accounts.GetByIdAsync, cancellationToken);

        public async Task<JsonLogEntry> UpdateAccountStatusByUserNameAsync(string userName, bool status, CancellationToken cancellationToken)
            => await UpdateAccountStatusAsync(userName, status, _unit.Accounts.GetByUserNameAsync, cancellationToken);

        public async Task<IEnumerable<JsonLogEntry>> UpdateAccountStatusByUserNamesAsync(IEnumerable<string> userNames, bool status, CancellationToken cancellationToken)
            => await UpdateAccountStatusAsync(userNames, status, (userNames, token) => _unit.Accounts.GetByUserNamesAsync(userNames, cancellationToken: token), cancellationToken);

        public async Task<IEnumerable<JsonLogEntry>> UpdateAccountStatusAsync(IEnumerable<uint> accountIds, bool status, CancellationToken cancellationToken)
            => await UpdateAccountStatusAsync(accountIds.Select(id => id.ToString()), status, _unit.Accounts.GetByIdsAsync, cancellationToken);

        // Helper properties to access DAOs
        private async Task<JsonLogEntry> UpdateAccountStatusAsync(string input, bool status, Func<string, CancellationToken, Task<Account?>> getFunc,
            CancellationToken cancellationToken)
        {
            await _unit.BeginTransactionAsync(cancellationToken);
            try
            {
                Account? account = await getFunc(input, cancellationToken);
                if (account == null)
                {
                    await _unit.RollbackTransactionAsync(cancellationToken);
                    return _logger.JsonLogWarning<Account, UpdateAccountServices>($"Account with input {input} does not exist.");
                }
                if (account.AccountStatus == status)
                {
                    await _unit.RollbackTransactionAsync(cancellationToken);
                    return _logger.JsonLogInfo<Account, UpdateAccountServices>($"Account {input} already has the desired status.");
                }

                account.AccountStatus = status;
                int affectedRows = await _unit.Accounts.UpdateAsync(account, cancellationToken);
                if (affectedRows == 0)
                {
                    await _unit.RollbackTransactionAsync(cancellationToken);
                    return _logger.JsonLogWarning<Account, UpdateAccountServices>($"Failed to update the account status for {input}.");
                }

                await _unit.CommitTransactionAsync(cancellationToken);
                return _logger.JsonLogInfo<Account, UpdateAccountServices>($"Updated status for account {input}.", affectedRows: affectedRows);
            }
            catch (Exception ex)
            {
                await _unit.RollbackTransactionAsync(cancellationToken);
                return _logger.JsonLogError<Account, UpdateAccountServices>($"An error occurred while updating the account status for {input}.", ex);
            }
        }

        private async Task<IEnumerable<JsonLogEntry>> UpdateAccountStatusAsync(IEnumerable<string> inputs, bool status,
            Func<IEnumerable<string>, CancellationToken, Task<IEnumerable<Account>>> getFunc, CancellationToken cancellationToken)
        {
            List<JsonLogEntry> logEntries = [];
            await _unit.BeginTransactionAsync(cancellationToken);
            try
            {
                IEnumerable<Account> accounts = await getFunc(inputs, cancellationToken);
                if (accounts == null || !accounts.Any())
                {
                    await _unit.RollbackTransactionAsync(cancellationToken);
                    logEntries.Add(_logger.JsonLogWarning<Account, UpdateAccountServices>("No accounts found for the provided inputs."));
                    return logEntries;
                }

                foreach (Account account in accounts)
                    account.AccountStatus = status;
                int affectedRows = await _unit.Accounts.UpdateRangeAsync(accounts, cancellationToken);
                if (affectedRows == 0)
                {
                    await _unit.RollbackTransactionAsync(cancellationToken);
                    logEntries.Add(_logger.JsonLogWarning<Account, UpdateAccountServices>("Failed to update statuses for multiple accounts."));
                    return logEntries;
                }
                await _unit.CommitTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogInfo<Account, UpdateAccountServices>("Updated statuses for multiple accounts.", affectedRows: affectedRows));
                return logEntries;
            }
            catch (TaskCanceledException ex)
            {
                await _unit.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, UpdateAccountServices>("An error occurred while updating the account statuses.", ex));
                return logEntries;
            }
            catch (OperationCanceledException ex)
            {
                await _unit.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, UpdateAccountServices>("An error occurred while updating the account statuses.", ex));
                return logEntries;
            }
            catch (Exception ex)
            {
                await _unit.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, UpdateAccountServices>("An error occurred while updating the account statuses.", ex));
                return logEntries;
            }
        }

        private async Task<JsonLogEntry> UpdateAccountPasswordAsync(string input, string newPassword, Func<string, CancellationToken, Task<Account?>> getFunc,
            CancellationToken cancellationToken)
        {
            await _unit.BeginTransactionAsync(cancellationToken);
            try
            {
                Account? account = await getFunc(input, cancellationToken);
                if (account == null)
                {
                    await _unit.RollbackTransactionAsync(cancellationToken);
                    return _logger.JsonLogWarning<Account, UpdateAccountServices>($"Account with input {input} does not exist.");
                }

                if (!await _hashPassword.IsPasswordValidAsync(newPassword, cancellationToken))
                    account.Password = await _hashPassword.HashPasswordAsync(newPassword, cancellationToken);
                else
                    account.Password = newPassword;

                int affectedRows = await _unit.Accounts.UpdateAsync(account, cancellationToken);
                if (affectedRows == 0)
                {
                    await _unit.RollbackTransactionAsync(cancellationToken);
                    return _logger.JsonLogWarning<Account, UpdateAccountServices>($"Failed to update the account password for {input}.");
                }

                await _unit.CommitTransactionAsync(cancellationToken);
                return _logger.JsonLogInfo<Account, UpdateAccountServices>($"Updated password for account {input}.", affectedRows: affectedRows);
            }
            catch (Exception ex)
            {
                await _unit.RollbackTransactionAsync(cancellationToken);
                return _logger.JsonLogError<Account, UpdateAccountServices>($"An error occurred while updating the account password for {input}.", ex);
            }
        }
    }
}
