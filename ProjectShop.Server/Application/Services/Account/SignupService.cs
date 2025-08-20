using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.EntitiesRequest;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Account
{
    public class SignupService : BaseHelperService<AccountModel>, ISignupService<AccountModel>
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

        public async Task<IEnumerable<BatchItemResult<AccountModel>>> AddAccountsAsync(IEnumerable<AccountModel> entities)
        {
            try
            {
                var filteredEntities = await FilterValidEntities(entities, entity => entity.UserName, _accountDAO.GetByUserNameAsync);
                filteredEntities.TryGetValue(filteredEntities.Keys.FirstOrDefault(), out var batchObjectResult);
                if (batchObjectResult == null)
                    throw new InvalidOperationException("No valid accounts found to add.");
                if (!batchObjectResult.ValidEntities.Any())
                    throw new InvalidOperationException("No valid accounts found to add.");

                // Hash passwords for valid entities
                var validEntities = batchObjectResult.ValidEntities;
                validEntities = await HashPasswordAsync(validEntities);
                int affectedRows = await _baseDAO.InsertAsync(validEntities);
                if (affectedRows == 0)
                    throw new InvalidOperationException("Failed to insert the accounts.");
                return batchObjectResult.BatchResults;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while inserting multiple accounts.", ex);
            }
        }

        private async Task<IEnumerable<AccountModel>> HashPasswordAsync(IEnumerable<AccountModel> entities)
        {
            foreach (AccountModel entity in entities)
            {
                if (!await hashPassword.IsPasswordValidAsync(entity.Password))
                    throw new ArgumentException($"Password for user {entity.UserName} does not meet the required criteria.", nameof(entity.Password));
                entity.Password = await hashPassword.HashPasswordAsync(entity.Password);
            }
            return entities;
        }
    }
}
