using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Application.Services.AccountServices
{
    public class ForgotPasswordService : IForgotPasswordServices
    {
        private readonly IDBContext _dbContext;
        private readonly IHashPassword _hashPassword;
        private readonly ILogService _logger;

        public ForgotPasswordService(IDBContext dbContext, IHashPassword hashPassword, ILogService logger)
        {
            _dbContext = dbContext;
            _hashPassword = hashPassword;
            _logger = logger;
        }

        public async Task<JsonLogEntry> UpdatePasswordAsync(string username, string password)
        {
            await using var transaction = await _dbContext.BeginTransactionAsync();
            try
            {
                Account? entity = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.UserName == username);
                if (entity == null)
                    return _logger.JsonLogWarning<Account, ForgotPasswordService>($"Account with username {username} does not exist.");
                if (!await _hashPassword.IsPasswordHashed(password))
                    entity.Password = await _hashPassword.HashPasswordAsync(password);
                int affectedRows = await _dbContext.SaveChangesAsync();
                if (affectedRows == 0)
                {
                    await transaction.RollbackAsync();
                    return _logger.JsonLogWarning<Account, ForgotPasswordService>($"Failed to update the account with username {username}.");
                }
                await transaction.CommitAsync();
                return _logger.JsonLogInfo<Account, ForgotPasswordService>($"Successfully updated the password for account with username {username}.", affectedRows: affectedRows);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return _logger.JsonLogError<Account, ForgotPasswordService>($"An error occurred while updating the account with username {username}.", ex);
            }
        }

        public async Task<IEnumerable<JsonLogEntry>> UpdatePasswordAsync(List<string> usernames, List<string> passwords)
        {
            List<JsonLogEntry> logEntries = [];
            await using var transaction = await _dbContext.BeginTransactionAsync();
            try
            {
                IEnumerable<Account> entities = await _dbContext.Accounts
                    .Where(a => usernames.Contains(a.UserName))
                    .ToListAsync();

                if (entities == null || !entities.Any())
                {
                    logEntries.Add(_logger.JsonLogWarning<Account, ForgotPasswordService>("No accounts found for the provided usernames."));
                    return logEntries;
                }

                entities = await PasswordMapping(entities, passwords);
                int affectedRows = await _dbContext.SaveChangesAsync();
                if (affectedRows == 0)
                {
                    await transaction.RollbackAsync();
                    logEntries.Add(_logger.JsonLogWarning<Account, ForgotPasswordService>("Failed to update passwords for multiple accounts."));
                    return logEntries;
                }
                await transaction.CommitAsync();
                logEntries.Add(_logger.JsonLogInfo<Account, ForgotPasswordService>($"Successfully updated passwords for {affectedRows} accounts.", affectedRows: affectedRows));
                return logEntries;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logEntries.Add(_logger.JsonLogError<Account, ForgotPasswordService>("An error occurred while updating multiple accounts.", ex));
                return logEntries;
            }
        }

        private async Task<IEnumerable<Account>> PasswordMapping(IEnumerable<Account> entities, IEnumerable<string> passwords)
        {
            int length = passwords.Count();
            for (int i = 0; i < length; i++)
                if (!await _hashPassword.IsPasswordHashed(passwords.ElementAt(i)))
                    entities.ElementAt(i).Password = await _hashPassword.HashPasswordAsync(passwords.ElementAt(i));
            for (int i = 0; i < length; i++)
                if (entities.ElementAt(i) == null)
                    throw new InvalidOperationException($"Account with username {entities.ElementAt(i).UserName} does not exist.");
                else
                    entities.ElementAt(i).Password = passwords.ElementAt(i);
            return entities;
        }
    }
}
