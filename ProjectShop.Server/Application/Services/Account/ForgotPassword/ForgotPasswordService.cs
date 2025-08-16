using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount.IForgotPassword;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Account.ForgotPassword
{
    public class ForgotPasswordService : ValidateService<AccountModel>, IForgotPasswordService
    {
        private readonly IDAO<AccountModel> _baseDAO;
        private readonly IAccountDAO<AccountModel> _accountDAO;

        public ForgotPasswordService(
            IDAO<AccountModel> baseDAO,
            IAccountDAO<AccountModel> accountDAO)
        {
            _baseDAO = baseDAO ?? throw new ArgumentNullException(nameof(baseDAO), "Base DAO cannot be null.");
            _accountDAO = accountDAO ?? throw new ArgumentNullException(nameof(accountDAO), "Account DAO cannot be null.");
        }

        public async Task<int> UpdatePasswordAsync(string username, string password)
        {
            try
            {
                AccountModel? entity = await _accountDAO.GetByUserNameAsync(username);
                if (entity == null)
                    throw new InvalidOperationException($"Account with username {username} does not exist.");
                if (!await hashPassword.IsPasswordHashed(password))
                    entity.Password = await hashPassword.HashPasswordAsync(password);
                int affectedRows = await _baseDAO.UpdateAsync(entity);
                if (affectedRows == 0)
                    throw new InvalidOperationException("Failed to update the account.");
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the password.", ex);
            }
        }

        public async Task<int> UpdatePasswordAsync(List<string> usernames, List<string> passwords)
        {
            try
            {
                IEnumerable<AccountModel> entities = await _accountDAO.GetByUserNameAsync(usernames);
                if (entities == null || !entities.Any())
                    throw new InvalidOperationException("No accounts found with the provided usernames.");
                entities = await PasswordMapping(entities, passwords);
                
                int affectedRows = await _baseDAO.UpdateManyAsync(entities);
                if (affectedRows == 0)
                    throw new InvalidOperationException("Failed to update the accounts.");
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating multiple accounts.", ex);
            }
        }

        private async Task<IEnumerable<AccountModel>> PasswordMapping(IEnumerable<AccountModel> entities, IEnumerable<string> passwords)
        {
            int length = passwords.Count();
            for (int i = 0; i < length; i++)
                if (!await hashPassword.IsPasswordHashed(passwords.ElementAt(i)))
                    entities.ElementAt(i).Password = await hashPassword.HashPasswordAsync(passwords.ElementAt(i));
            for (int i = 0; i < length; i++)
                if (entities.ElementAt(i) == null)
                    throw new InvalidOperationException($"Account with username {entities.ElementAt(i).UserName} does not exist.");
                else
                    entities.ElementAt(i).Password = passwords.ElementAt(i);
            return entities;
        }
    }
}
