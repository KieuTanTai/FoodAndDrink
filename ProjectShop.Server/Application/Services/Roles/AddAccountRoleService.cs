using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class AddAccountRoleService : IAddAccountRoleServices<RolesOfUserModel, RolesOfUserKey>
    {
        private readonly INoneUpdateDAO<RolesOfUserModel> _baseDAO;
        private readonly IBaseHelperServices<RolesOfUserModel> _helper;
        private readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUserDAO;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<AddAccountRoleService> _serviceResultFactory;

        public AddAccountRoleService(INoneUpdateDAO<RolesOfUserModel> baseDAO, IBaseHelperServices<RolesOfUserModel> helper, IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO, ILogService logger, IServiceResultFactory<AddAccountRoleService> serviceResultFactory)
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
                    return _serviceResultFactory.CreateServiceResult($"The account role with AccountId {keys.AccountId} and RoleId {keys.RoleId} already exists.",
                            new RolesOfUserModel(keys.AccountId, keys.RoleId), false);
                // Add the new account role
                int affectedRows = await _baseDAO.InsertAsync(new RolesOfUserModel { AccountId = keys.AccountId, RoleId = keys.RoleId });
                if (affectedRows == 0)
                    return _serviceResultFactory.CreateServiceResult($"Failed to add account role with AccountId {keys.AccountId} and RoleId {keys.RoleId}.",
                        new RolesOfUserModel(keys.AccountId, keys.RoleId), false);
                return _serviceResultFactory.CreateServiceResult($"Successfully added account role with AccountId {keys.AccountId} and RoleId {keys.RoleId}.",
                    new RolesOfUserModel(keys.AccountId, keys.RoleId), true, affectedRows: affectedRows);
            }
            catch (Exception ex)
            {
                _logger.LogError<RolesOfUserModel, AddAccountRoleService>($"An error occurred while adding account role with AccountId {keys.AccountId} and RoleId {keys.RoleId}.", ex);
                return _serviceResultFactory.CreateServiceResult($"An error occurred while adding account role with AccountId {keys.AccountId} and RoleId {keys.RoleId}.",
                    new RolesOfUserModel(keys.AccountId, keys.RoleId), false);
            }
        }

        // BUG: It may not work correctly if the entities are not unique by AccountId and RoleId.
        public async Task<ServiceResults<RolesOfUserModel>> AddAccountRolesAsync(IEnumerable<RolesOfUserModel> entities)
        {
            List<JsonLogEntry> logEntries = [];
            try
            {
                var filteredEntities = await _helper.FilterValidEntities(entities, (entity)
                    => new RolesOfUserKey(entity.AccountId, entity.RoleId), _roleOfUserDAO.GetByListKeysAsync);
                filteredEntities.TryGetValue(filteredEntities.Keys.FirstOrDefault(), out var serviceResults);
                if (serviceResults == null || serviceResults.Data == null || !serviceResults.Data.Any())
                {
                    logEntries = serviceResults?.LogEntries?.ToList() ?? [];
                    logEntries.Add(_logger.JsonLogWarning<RolesOfUserModel, AddAccountRoleService>("No valid account roles to add."));
                    return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>([], logEntries, false);
                }

                // Insert
                int affectedRows = await _baseDAO.InsertAsync(serviceResults.Data!);
                if (affectedRows == 0)
                {
                    logEntries.Add(_logger.JsonLogWarning<RolesOfUserModel, AddAccountRoleService>("Failed to add account roles.", affectedRows: affectedRows));
                    return _serviceResultFactory.CreateServiceResults(serviceResults.Data, logEntries, false);
                }
                logEntries.Add(_logger.JsonLogInfo<RolesOfUserModel, AddAccountRoleService>($"Successfully added account roles.", affectedRows: affectedRows));
                return _serviceResultFactory.CreateServiceResults(serviceResults.Data, logEntries, true);
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<RolesOfUserModel, AddAccountRoleService>("An error occurred while adding account roles.", ex));
                return _serviceResultFactory.CreateServiceResults<RolesOfUserModel>([], logEntries, false);
            }
        }
    }
}
