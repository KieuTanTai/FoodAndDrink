using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
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
    public class UpdatePasswordServices(IUnitOfWork unitOfWork, IHashPassword hashPassword, ILogService logger, IBasePasswordMappingServices basePasswordMappingServices) : IUpdatePasswordServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHashPassword _hashPassword = hashPassword;
        private readonly ILogService _logger = logger;
        private readonly IBasePasswordMappingServices _basePasswordMappingServices = basePasswordMappingServices;

        public async Task<JsonLogEntry> UpdatePasswordAsync(string userName, string password, CancellationToken cancellationToken)
        {
            _logger.LogInfo<Account, UpdatePasswordServices>($"Starting transaction to update password for account with userName {userName}.");
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                Account account = await HelperUpdatePasswordForAccountAsync(userName, cancellationToken);
                int affectedRows = await _unitOfWork.Accounts.UpdateAsync(account ,cancellationToken);
                if (affectedRows == 0)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return _logger.JsonLogWarning<Account, UpdatePasswordServices>($"Failed to update the account with userName {userName}.");
                }
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return _logger.JsonLogInfo<Account, UpdatePasswordServices>($"Successfully updated the password for account with userName {userName}.", affectedRows: affectedRows);
            }
            catch (SqlNullValueException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _logger.JsonLogError<Account, UpdatePasswordServices>($"Account with userName {userName} not found.", ex);
            }
            catch (TaskCanceledException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _logger.JsonLogError<Account, UpdatePasswordServices>($"Operation to update the account with userName {userName} was canceled.", ex);
            }
            catch (OperationCanceledException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _logger.JsonLogError<Account, UpdatePasswordServices>($"Operation to update the account with userName {userName} was canceled.", ex);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _logger.JsonLogError<Account, UpdatePasswordServices>($"An error occurred while updating the account with userName {userName}.", ex);
            }
        }

        public async Task<IEnumerable<JsonLogEntry>> UpdatePasswordAsync(List<FrontEndUpdatePasswordAccount> frontEndUpdatePasswordAccounts,
            CancellationToken cancellationToken)
        {
            List<JsonLogEntry> logEntries = [];
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                List<Account> accounts = await HelperUpdatePasswordForAccountsAsync(frontEndUpdatePasswordAccounts, cancellationToken);
                int affectedRows = await _unitOfWork.Accounts.UpdateRangeAsync(accounts, cancellationToken);
                if (affectedRows == 0)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    logEntries.Add(_logger.JsonLogWarning<Account, UpdatePasswordServices>("Failed to update passwords for multiple accounts."));
                    return logEntries;
                }
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogInfo<Account, UpdatePasswordServices>($"Successfully updated passwords for {affectedRows} accounts.",
                    affectedRows: affectedRows));
                return logEntries;
            }
            catch (SqlNullValueException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, UpdatePasswordServices>("One or more accounts not found while updating multiple accounts.", ex));
                return logEntries;
            }
            catch (TaskCanceledException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, UpdatePasswordServices>("Operation to update multiple accounts was canceled.", ex));
                return logEntries;
            }
            catch (OperationCanceledException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, UpdatePasswordServices>("Operation to update multiple accounts was canceled.", ex));
                return logEntries;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, UpdatePasswordServices>("An error occurred while updating multiple accounts.", ex));
                return logEntries;
            }
        }

        #region Helper methods for main public methods (for isolate from main methods)
        private async Task<Account> HelperUpdatePasswordForAccountAsync(string userName, CancellationToken cancellationToken = default)
        {
            Account account = await HelperGetAccountByUserNameAsync(userName, cancellationToken);
            if (!await _hashPassword.IsPasswordHashedAsync(account.Password, cancellationToken))
                account.Password = await _hashPassword.HashPasswordAsync(account.Password, cancellationToken);
            return account;
        }

        private async Task<List<Account>> HelperUpdatePasswordForAccountsAsync(IEnumerable<FrontEndUpdatePasswordAccount> frontEndUpdatePasswordAccounts,
        CancellationToken cancellationToken = default)
        {
            List<string> userNames = [.. frontEndUpdatePasswordAccounts.Select(account => account.UserName)];
            List<string> newPasswords = [.. frontEndUpdatePasswordAccounts.Select(account => account.Password)];
            List<Account> entities = await HelperGetAccountsByUserNamesAsync(userNames, cancellationToken);
            entities = await _basePasswordMappingServices.HelperPasswordMappingAsync(entities, newPasswords, cancellationToken);
            return entities;
        }
        #endregion

        #region  Helper methods fetching accounts and password mapping (for isolate from main methods) 
        private async Task<Account> HelperGetAccountByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            Account entity = await _unitOfWork.Accounts.GetByUserNameAsync(userName, cancellationToken)
                ?? throw new SqlNullValueException("Account not found!");
            return entity;
        }

        private async Task<List<Account>> HelperGetAccountsByUserNamesAsync(List<string> userNames, CancellationToken cancellationToken)
        {
            IEnumerable<Account> entities = await _unitOfWork.Accounts.GetByUserNamesAsync(userNames, cancellationToken);
            if (entities == null || !entities.Any())
                throw new SqlNullValueException("No accounts found for the provided userNames.");
            return [..entities];
        }

        #endregion
    }
}
