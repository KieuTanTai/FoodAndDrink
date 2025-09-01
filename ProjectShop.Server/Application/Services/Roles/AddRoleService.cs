using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using TLGames.Application.Services;

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
                    return _serviceResultFactory.CreateServiceResult<RoleModel>("Role with the same ID already exists.", role, false);
                // Add the new role
                int affectedRows = await _baseDAO.InsertAsync(role);
                if (affectedRows > 0) 
                    return _serviceResultFactory.CreateServiceResult<RoleModel>("Role added successfully.", role, true, affectedRows: affectedRows);
                return _serviceResultFactory.CreateServiceResult<RoleModel>("Failed to add role.", role, false);
            }
            catch (Exception ex)
            {
                _logger.LogError<RoleModel, AddRoleService>("An error occurred while adding the role.", ex);
                return _serviceResultFactory.CreateServiceResult<RoleModel>("An error occurred while adding the role.", role, false, ex);
            }
        }

        public async Task<ServiceResults<RoleModel>> AddRolesAsync(IEnumerable<RoleModel> roles)
        {
            try
            {
                var filteredRoles = await _helper.FilterValidEntities(roles, (entity) => entity.RoleName, _roleDAO.GetByRoleNamesAsync);
                filteredRoles.TryGetValue(filteredRoles.Keys.FirstOrDefault(), out var serviceResults);
                if (!serviceResults!.Data!.Any())
                    return _serviceResultFactory.CreateServiceResults<RoleModel>("No valid roles found to add.", [], false);

                // insert
                int affectedRows = await _baseDAO.InsertAsync(serviceResults.Data!);
                if (affectedRows == 0)
                    return _serviceResultFactory.CreateServiceResults<RoleModel>("Failed to add roles.", serviceResults.Data!, false);
                return _serviceResultFactory.CreateServiceResults<RoleModel>("Roles added successfully.", serviceResults.Data!, true, affectedRows: affectedRows);

            }
            catch (Exception ex)
            {
                _logger.LogError<IEnumerable<RoleModel>, AddRoleService>("An error occurred while adding the roles.", ex);
                return _serviceResultFactory.CreateServiceResults<RoleModel>("An error occurred while adding the roles.", [], false, ex);
            }
        }
    }
}
