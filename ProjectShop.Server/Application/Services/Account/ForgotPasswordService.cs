using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Application.Services.Account
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly IDAO<AccountModel> _baseDAO;
        private readonly IAccountDAO<AccountModel> _accountDAO;
        private readonly IHashPassword _hashPassword;
        private readonly ILogService _logger;

        public ForgotPasswordService(
            IDAO<AccountModel> baseDAO,
            IAccountDAO<AccountModel> accountDAO,
            IHashPassword hashPassword,
            ILogService logger)
        {
            _baseDAO = baseDAO ?? throw new ArgumentNullException(nameof(baseDAO), "Base DAO cannot be null.");
            _accountDAO = accountDAO ?? throw new ArgumentNullException(nameof(accountDAO), "Account DAO cannot be null.");
            _hashPassword = hashPassword ?? throw new ArgumentNullException(nameof(hashPassword), "Hash password service cannot be null.");
            _logger = logger;
        }

        public async Task<JsonLogEntry> UpdatePasswordAsync(string username, string password)
        {
            JsonLogEntry logEntry = new JsonLogEntry();
            try
            {
                AccountModel? entity = await _accountDAO.GetByUserNameAsync(username);
                if (entity == null)
                    return _logger.JsonLogWarning<AccountModel, ForgotPasswordService>($"Account with username {username} does not exist.");
                if (!await _hashPassword.IsPasswordHashed(password))
                    entity.Password = await _hashPassword.HashPasswordAsync(password);
                int affectedRows = await _baseDAO.UpdateAsync(entity);
                if (affectedRows == 0)
                    return _logger.JsonLogWarning<AccountModel, ForgotPasswordService>($"Failed to update the account with username {username}.");
                return _logger.JsonLogInfo<AccountModel, ForgotPasswordService>($"Successfully updated the password for account with username {username}.", affectedRows: affectedRows);
            }
            catch (Exception ex)
            {
                return _logger.JsonLogError<AccountModel, ForgotPasswordService>($"An error occurred while updating the account with username {username}.", ex);
            }
        }

        public async Task<IEnumerable<JsonLogEntry>> UpdatePasswordAsync(List<string> usernames, List<string> passwords)
        {
            List<JsonLogEntry> logEntries = new List<JsonLogEntry>();
            try
            {
                IEnumerable<AccountModel> entities = await _accountDAO.GetByUserNameAsync(usernames);
                if (entities == null || !entities.Any())
                {
                    logEntries.Add(_logger.JsonLogWarning<AccountModel, ForgotPasswordService>("No accounts found for the provided usernames."));
                    return logEntries;
                }

                entities = await PasswordMapping(entities, passwords);
                int affectedRows = await _baseDAO.UpdateAsync(entities);
                if (affectedRows == 0)
                {
                    logEntries.Add(_logger.JsonLogWarning<AccountModel, ForgotPasswordService>("Failed to update passwords for multiple accounts."));
                    return logEntries;
                }
                logEntries.Add(_logger.JsonLogInfo<AccountModel, ForgotPasswordService>($"Successfully updated passwords for {affectedRows} accounts.", affectedRows: affectedRows));
                return logEntries;
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<AccountModel, ForgotPasswordService>("An error occurred while updating multiple accounts.", ex));
                return logEntries;
            }
        }

        private async Task<IEnumerable<AccountModel>> PasswordMapping(IEnumerable<AccountModel> entities, IEnumerable<string> passwords)
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
