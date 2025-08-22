using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.ObjectValue;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class AddAccountRoleService : IAddAccountRoleService<RolesOfUserModel, RolesOfUserKey>
    {
        private readonly IDAO<RolesOfUserModel> _baseDAO;
        private readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUserDAO;
        private readonly IBaseHelperService<RolesOfUserModel> _helper;

        public AddAccountRoleService(IDAO<RolesOfUserModel> baseDAO, IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO, IBaseHelperService<RolesOfUserModel> helper)
        {
            _baseDAO = baseDAO ?? throw new ArgumentNullException(nameof(baseDAO), "Base DAO cannot be null.");
            _roleOfUserDAO = roleOfUserDAO ?? throw new ArgumentNullException(nameof(roleOfUserDAO), "Role of User DAO cannot be null.");
            _helper = helper ?? throw new ArgumentNullException(nameof(helper), "Helper service cannot be null.");
        }

        public async Task<int> AddAccountRoleAsync(RolesOfUserKey keys)
        {
            try
            {
                if (await _helper.IsExistObject(keys, _roleOfUserDAO.GetByKeysAsync))
                    throw new InvalidOperationException("Account role already exists.");
                // Add the new account role
                int result = await _baseDAO.InsertAsync(new RolesOfUserModel { AccountId = keys.AccountId, RoleId = keys.RoleId });
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the account role.", ex);
            }
        }

        // BUG: It may not work correctly if the entities are not unique by AccountId and RoleId.
        public async Task<IEnumerable<BatchItemResult<RolesOfUserModel>>> AddAccountRolesAsync(IEnumerable<RolesOfUserModel> entities)
        {
            try
            {
                var filteredEntities = await _helper.FilterValidEntities(entities, (entity)
                    => new RolesOfUserKey(entity.AccountId, entity.RoleId), _roleOfUserDAO.GetByListKeysAsync);
                filteredEntities.TryGetValue(filteredEntities.Keys.FirstOrDefault(), out var batchObjectResult);
                if (batchObjectResult == null)
                    throw new InvalidOperationException("No valid account roles found to add.");
                if (!batchObjectResult.ValidEntities.Any())
                    throw new InvalidOperationException("No valid account roles found to add.");

                // Insert
                int result = await _baseDAO.InsertAsync(batchObjectResult.ValidEntities);
                return batchObjectResult.BatchResults;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the account roles.", ex);
            }
        }
    }
}
