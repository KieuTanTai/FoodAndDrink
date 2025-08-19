using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Account
{
    public class SignupService : ValidateService<AccountModel>, ISignupService<AccountModel>
    {
        private readonly IDAO<AccountModel> _baseDAO;
        private readonly IAccountDAO<AccountModel> _accountDAO;

        public SignupService(
            IDAO<AccountModel> baseDAO,
            IAccountDAO<AccountModel> accountDAO)
        {
            _baseDAO = baseDAO ?? throw new ArgumentNullException(nameof(baseDAO), "Base DAO cannot be null.");
            _accountDAO = accountDAO ?? throw new ArgumentNullException(nameof(accountDAO), "Account DAO cannot be null.");
        }

        //NOTE: SIGNUP FUNCTIONALITY
        public async Task<int> AddAccountAsync(AccountModel entity)
        {
            try
            {
                if (await IsExistObject(entity.UserName, _accountDAO.GetByUserNameAsync))
                    throw new InvalidOperationException($"Account with username {entity.UserName} already exists.");
                if (!await hashPassword.IsPasswordValidAsync(entity.Password))
                    throw new ArgumentException("Password does not meet the required criteria.", nameof(entity.Password));
                entity.Password = await hashPassword.HashPasswordAsync(entity.Password);
                int affectedRows = await _baseDAO.InsertAsync(entity);
                if (affectedRows == 0)
                    throw new InvalidOperationException("Failed to insert the account.");
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while inserting the account.", ex);
            }
        }

        public async Task<int> AddAccountsAsync(IEnumerable<AccountModel> entities)
        {
            try
            {
                IEnumerable<string> usernames = entities.Select(e => e.UserName);
                if (await DoAllIdsExistAsync(usernames, _accountDAO.GetByUserNameAsync))
                    throw new InvalidOperationException("One or more accounts with the provided usernames already exist.");
                foreach (AccountModel entity in entities)
                {
                    if (!await hashPassword.IsPasswordValidAsync(entity.Password))
                        throw new ArgumentException($"Password for user {entity.UserName} does not meet the required criteria.", nameof(entity.Password));
                    entity.Password = await hashPassword.HashPasswordAsync(entity.Password);
                }
                int affectedRows = await _baseDAO.InsertManyAsync(entities);
                if (affectedRows == 0)
                    throw new InvalidOperationException("Failed to insert the accounts.");
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while inserting multiple accounts.", ex);
            }
        }
    }
}
