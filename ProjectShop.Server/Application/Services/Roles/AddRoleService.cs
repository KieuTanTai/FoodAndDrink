using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class AddRoleService : IAddRoleServices<RoleModel>
    {
        private readonly IDAO<RoleModel> _baseDAO;
        private readonly IRoleDAO<RoleModel> _roleDAO;
        private readonly IBaseHelperServices<RoleModel> _helper;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<AddRoleService> _serviceResultFactory;

        public AddRoleService(IDAO<RoleModel> baseDAO, IRoleDAO<RoleModel> roleDAO, IBaseHelperServices<RoleModel> helper, ILogService logger, IServiceResultFactory<AddRoleService> serviceResultFactory)
        {
            _baseDAO = baseDAO;
            _roleDAO = roleDAO;
            _helper = helper;
            _logger = logger;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResult<RoleModel>> AddRoleAsync(RoleModel role)
        {
            try
            {
                if (await _helper.IsExistObject(role.RoleId.ToString(), _baseDAO.GetSingleDataAsync))
                    return _serviceResultFactory.CreateServiceResult("Role with the same ID already exists.", role, false);
                // Add the new role
                int affectedRows = await _baseDAO.InsertAsync(role);
                if (affectedRows > 0)
                    return _serviceResultFactory.CreateServiceResult("Role added successfully.", role, true, affectedRows: affectedRows);
                return _serviceResultFactory.CreateServiceResult("Failed to add role.", role, false);
            }
            catch (Exception ex)
            {
                _logger.LogError<RoleModel, AddRoleService>("An error occurred while adding the role.", ex);
                return _serviceResultFactory.CreateServiceResult("An error occurred while adding the role.", role, false, ex);
            }
        }

        public async Task<ServiceResults<RoleModel>> AddRolesAsync(IEnumerable<RoleModel> roles)
        {
            List<JsonLogEntry> logEntries = [];
            try
            {
                var filteredRoles = await _helper.FilterValidEntities(roles, (entity) => entity.RoleName, _roleDAO.GetByRoleNamesAsync);
                filteredRoles.TryGetValue(filteredRoles.Keys.FirstOrDefault(), out var serviceResults);
                if (serviceResults == null || serviceResults.Data == null || !serviceResults.Data.Any())
                {
                    logEntries = serviceResults?.LogEntries?.ToList() ?? [];
                    logEntries.Add(_logger.JsonLogWarning<IEnumerable<RoleModel>, AddRoleService>("All roles already exist or no valid roles to add."));
                    return _serviceResultFactory.CreateServiceResults<RoleModel>([], logEntries, false);
                }

                // insert
                int affectedRows = await _baseDAO.InsertAsync(serviceResults.Data!);
                if (affectedRows == 0)
                {
                    logEntries.Add(_logger.JsonLogWarning<IEnumerable<RoleModel>, AddRoleService>("Failed to add the roles.", affectedRows: affectedRows));
                    return _serviceResultFactory.CreateServiceResults(serviceResults.Data, logEntries, false);
                }
                logEntries.Add(_logger.JsonLogInfo<IEnumerable<RoleModel>, AddRoleService>("Roles added successfully.", affectedRows: affectedRows));
                return _serviceResultFactory.CreateServiceResults(serviceResults.Data, logEntries, true);

            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<IEnumerable<RoleModel>, AddRoleService>("An error occurred while adding roles.", ex));
                return _serviceResultFactory.CreateServiceResults<RoleModel>([], logEntries, false);
            }
        }
    }
}
