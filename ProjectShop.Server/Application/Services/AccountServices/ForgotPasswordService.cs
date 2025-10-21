using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.PlatformRules;

namespace ProjectShop.Server.Application.Services.AccountServices
{
    public class ForgotPasswordService : IForgotPasswordServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashPassword _hashPassword;
        private readonly ILogService _logger;

        public ForgotPasswordService(IUnitOfWork unitOfWork, IHashPassword hashPassword, ILogService logger)
        {
            _unitOfWork = unitOfWork;
            _hashPassword = hashPassword;
            _logger = logger;
        }

        public async Task<JsonLogEntry> UpdatePasswordAsync(string userName, string password, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            _logger.LogInfo<Account, ForgotPasswordService>($"Starting transaction to update password for account with userName {userName}.");
            try
            {
                await HelperUpdatePasswordForAccountAsync(userName, cancellationToken);
                int affectedRows = await _unitOfWork.SaveChangesAsync(cancellationToken);
                if (affectedRows == 0)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return _logger.JsonLogWarning<Account, ForgotPasswordService>($"Failed to update the account with userName {userName}.");
                }
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return _logger.JsonLogInfo<Account, ForgotPasswordService>($"Successfully updated the password for account with userName {userName}.", affectedRows: affectedRows);
            }
            catch (SqlNullValueException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _logger.JsonLogError<Account, ForgotPasswordService>($"Account with userName {userName} not found.", ex);
            }
            catch (TaskCanceledException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _logger.JsonLogError<Account, ForgotPasswordService>($"Operation to update the account with userName {userName} was canceled.", ex);
            }
            catch (OperationCanceledException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _logger.JsonLogError<Account, ForgotPasswordService>($"Operation to update the account with userName {userName} was canceled.", ex);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _logger.JsonLogError<Account, ForgotPasswordService>($"An error occurred while updating the account with userName {userName}.", ex);
            }
        }

        public async Task<IEnumerable<JsonLogEntry>> UpdatePasswordAsync(List<FrontEndUpdatePasswordAccount> frontEndUpdatePasswordAccounts,
            CancellationToken cancellationToken)
        {
            List<JsonLogEntry> logEntries = [];
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                int affectedRows = await _unitOfWork.SaveChangesAsync(cancellationToken);
                if (affectedRows == 0)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    logEntries.Add(_logger.JsonLogWarning<Account, ForgotPasswordService>("Failed to update passwords for multiple accounts."));
                    return logEntries;
                }
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogInfo<Account, ForgotPasswordService>($"Successfully updated passwords for {affectedRows} accounts.",
                    affectedRows: affectedRows));
                return logEntries;
            }
            catch (SqlNullValueException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, ForgotPasswordService>("One or more accounts not found while updating multiple accounts.", ex));
                return logEntries;
            }
            catch (TaskCanceledException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, ForgotPasswordService>("Operation to update multiple accounts was canceled.", ex));
                return logEntries;
            }
            catch (OperationCanceledException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, ForgotPasswordService>("Operation to update multiple accounts was canceled.", ex));
                return logEntries;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, ForgotPasswordService>("An error occurred while updating multiple accounts.", ex));
                return logEntries;
            }
        }

        #region Helper methods for main public methods (for isolate from main methods)
        private async Task HelperUpdatePasswordForAccountAsync(string userName, CancellationToken cancellationToken = default)
        {
            Account account = await HelperGetAccountByUserNameAsync(userName, cancellationToken);
            if (!await _hashPassword.IsPasswordHashed(account.Password))
                account.Password = await _hashPassword.HashPasswordAsync(account.Password);
        }

        private async Task HelperUpdatePasswordForAccountsAsync(IEnumerable<FrontEndUpdatePasswordAccount> frontEndUpdatePasswordAccounts,
        CancellationToken cancellationToken = default)
        {
            List<string> userNames = [.. frontEndUpdatePasswordAccounts.Select(account => account.UserName)];
            List<Account> entities = await HelperGetAccountsByUserNamesAsync(userNames, cancellationToken);
            entities = await HelperPasswordMappingAsync(entities);
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

        private async Task<List<Account>> HelperPasswordMappingAsync(List<Account> entities)
        {
            foreach (var account in entities)
                if (!await _hashPassword.IsPasswordHashed(account.Password))
                    account.Password = await _hashPassword.HashPasswordAsync(account.Password);
            return entities;
        }
        #endregion
    }
}
