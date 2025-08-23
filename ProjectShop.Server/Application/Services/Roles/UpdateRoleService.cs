git using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using System.ComponentModel;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class UpdateRoleService : IUpdateRoleService
    {
        private readonly IDAO<RoleModel> _baseDAO;

        public UpdateRoleService(IDAO<RoleModel> baseDAO)
        {
            _baseDAO = baseDAO ?? throw new ArgumentNullException(nameof(baseDAO), "Base DAO cannot be null.");
        }

        public async Task<int> UpdateRoleStatusAsync(uint roleId, bool status)
            => await UpdateRoleStatusAsync(roleId.ToString(), status, _baseDAO.GetSingleDataAsync);

        public async Task<int> UpdateRoleStatusAsync(IEnumerable<uint> roleIds, bool status)
            => await UpdateRoleStatusAsync(roleIds.Select(id => id.ToString()), status, _baseDAO.GetByInputsAsync);

        public async Task<int> UpdateRoleNameAsync(uint roleId, string newRoleName)
            => await UpdateRoleNameAsync(roleId.ToString(), newRoleName, _baseDAO.GetSingleDataAsync);

        public async Task<int> UpdateRoleNamesAsync(IEnumerable<uint> roleIds, IEnumerable<string> newRoleNames)
            => await UpdateRoleNamesAsync(roleIds.Select(id => id.ToString()), newRoleNames, _baseDAO.GetByInputsAsync);

        private async Task<int> UpdateRoleNameAsync(string input, string newName, Func<string, Task<RoleModel?>> daoFunc)
        {
            try
            {
                RoleModel? role = await daoFunc(input);
                if (role == null)
                    throw new InvalidOperationException($"Role with input {input} does not exist.");
                role.RoleName = newName;
                int affectedRows = await _baseDAO.UpdateAsync(role);
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while updating the role name for {input}.", ex);
            }
        }

        private async Task<int> UpdateRoleNamesAsync(IEnumerable<string> inputs, IEnumerable<string> newNames, Func<IEnumerable<string>, int?, Task<IEnumerable<RoleModel>>> daoFunc)
        {
            try
            {
                var roles = await daoFunc(inputs, null);
                if (roles == null || !roles.Any())
                    throw new InvalidOperationException($"No roles found for inputs: {string.Join(", ", inputs)}.");
                int index = 0;
                int totalNames = newNames.Count();
                foreach (RoleModel role in roles)
                    if (index < totalNames)
                        role.RoleName = newNames.ElementAt(index++);
                    else
                        throw new InvalidOperationException("Not enough new names provided for the roles.");

                int affectedRows = await _baseDAO.UpdateAsync(roles);
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while updating the role names for inputs: {string.Join(", ", inputs)}.", ex);
            }
        } 

        private async Task<int> UpdateRoleStatusAsync(string input, bool status, Func<string, Task<RoleModel?>> daoFunc)
        {
            try
            {
                RoleModel? role = await daoFunc(input);
                if (role == null)
                    throw new InvalidOperationException($"Role with input {input} does not exist.");
                role.RoleStatus = status;
                int affectedRows = await _baseDAO.UpdateAsync(role);
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while updating the role status for {input}.", ex);
            }
        }

        private async Task<int> UpdateRoleStatusAsync(IEnumerable<string> inputs, bool status, Func<IEnumerable<string>, int?, Task<IEnumerable<RoleModel>>> daoFunc)
        {
            try
            {
                var roles = await daoFunc(inputs, null);
                if (roles == null || !roles.Any())
                    throw new InvalidOperationException($"No roles found for inputs: {string.Join(", ", inputs)}.");

                foreach (RoleModel role in roles)
                    role.RoleStatus = status;

                int affectedRows = await _baseDAO.UpdateAsync(roles);
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while updating the role status for inputs: {string.Join(", ", inputs)}.", ex);
            }
        }
    }
}
