using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class AddAccountRoleService : IAddAccountRoleService<RolesOfUserModel, RolesOfUserKey>
    {
        private readonly IDAO<RolesOfUserModel> _baseDAO;
        private readonly IBaseHelperService<RolesOfUserModel> _helper;
        private readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUserDAO;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<AddAccountRoleService> _serviceResultFactory;

        public AddAccountRoleService(IDAO<RolesOfUserModel> baseDAO, IBaseHelperService<RolesOfUserModel> helper, IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO, ILogService logger, IServiceResultFactory<AddAccountRoleService> serviceResultFactory)
        {
            _baseDAO = baseDAO;
            _helper = helper;
            _roleOfUserDAO = roleOfUserDAO;
            _logger = logger;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResult<RolesOfUserModel>> AddAccountRoleAsync(RolesOfUserKey keys)
        {
            try
            {
                if (await _helper.IsExistObject(keys, _roleOfUserDAO.GetByKeysAsync))
                    return _serviceResultFactory.CreateServiceResult<RolesOfUserModel>($"The account role with AccountId {keys.AccountId} and RoleId {keys.RoleId} already exists.", new RolesOfUserModel(keys.AccountId, keys.RoleId), false);
                // Add the new account role
                int affectedRows = await _baseDAO.InsertAsync(new RolesOfUserModel { AccountId = keys.AccountId, RoleId = keys.RoleId });
                if (affectedRows <= 0)
                    return _serviceResultFactory.CreateServiceResult<RolesOfUserModel>($"Failed to add account role with AccountId {keys.AccountId} and RoleId {keys.RoleId}.", new RolesOfUserModel(keys.AccountId, keys.RoleId), false);
                return _serviceResultFactory.CreateServiceResult<RolesOfUserModel>($"Successfully added account role with AccountId {keys.AccountId} and RoleId {keys.RoleId}.", new RolesOfUserModel(keys.AccountId, keys.RoleId), true, affectedRows : affectedRows);
            }
            catch (Exception ex)
            {
                _logger.LogError<RolesOfUserModel, AddAccountRoleService>($"An error occurred while adding account role with AccountId {keys.AccountId} and RoleId {keys.RoleId}.", ex);
                return _serviceResultFactory.CreateServiceResult<RolesOfUserModel>($"An error occurred while adding account role with AccountId {keys.AccountId} and RoleId {keys.RoleId}.", new RolesOfUserModel(keys.AccountId, keys.RoleId), false);
            }
        }

        // BUG: It may not work correctly if the entities are not unique by AccountId and RoleId.
        public async Task<ServiceResults<RolesOfUserModel>> AddAccountRolesAsync(IEnumerable<RolesOfUserModel> entities)
        {
            try
            {
                var filteredEntities = await _helper.FilterValidEntities(entities, (entity)
                    => new RolesOfUserKey(entity.AccountId, entity.RoleId), _roleOfUserDAO.GetByListKeysAsync);
                filteredEntities.TryGetValue(filteredEntities.Keys.FirstOrDefault(), out var serviceResults);
                if (!serviceResults!.Data!.Any())
                    return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("No valid account roles to add.", new List<RolesOfUserModel>(), false);

                // Insert
                int affectedRows = await _baseDAO.InsertAsync(serviceResults.Data!);
                if (affectedRows <= 0)
                    return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("Failed to add account roles.", serviceResults.Data!, false);
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>($"Successfully added account roles.", serviceResults.Data!, true, affectedRows : affectedRows);
            }
            catch (Exception ex)
            {
                _logger.LogError<RolesOfUserModel, AddAccountRoleService>("An error occurred while adding account roles.", ex);
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>("An error occurred while adding account roles.", new List<RolesOfUserModel>(), false);
            }
        }
    }
}
