using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.EntitiesRequest;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class AddRoleService : BaseHelperService<RoleModel>, IAddRoleService<RoleModel>
    {
        private readonly IDAO<RoleModel> _baseDAO;
        private readonly IRoleDAO<RoleModel> _roleDAO;
        public AddRoleService(IDAO<RoleModel> baseDAO, IRoleDAO<RoleModel> rolDAO)
        {
            _baseDAO = baseDAO ?? throw new ArgumentNullException(nameof(baseDAO), "Base DAO cannot be null.");
            _roleDAO = rolDAO ?? throw new ArgumentNullException(nameof(rolDAO), "Role DAO cannot be null.");
        }

        public async Task<int> AddRoleAsync(RoleModel role)
        {
            try
            {
                if (await IsExistObject(role.RoleId.ToString(), _baseDAO.GetSingleDataAsync))
                    throw new InvalidOperationException("Role already exists.");
                // Add the new role
                int result = await _baseDAO.InsertAsync(role);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the role.", ex);
            }
        }

        public async Task<IEnumerable<BatchItemResult<RoleModel>>> AddRolesAsync(IEnumerable<RoleModel> roles)
        {
            try
            {
                var filteredRoles = await FilterValidEntities(roles, (entity) => entity.RoleName, _roleDAO.GetByRoleNamesAsync);
                filteredRoles.TryGetValue(filteredRoles.Keys.FirstOrDefault(), out var batchObjectResult);
                if (batchObjectResult == null)
                    throw new InvalidOperationException("No valid roles found to add.");
                if (!batchObjectResult.ValidEntities.Any())
                    throw new InvalidOperationException("No valid roles found to add.");

                // insert
                int result = await _baseDAO.InsertAsync(batchObjectResult.ValidEntities);
                if (result <= 0)
                    throw new InvalidOperationException("Failed to add roles.");
                return batchObjectResult.BatchResults;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the roles.", ex);
            }
        }
    }
}
