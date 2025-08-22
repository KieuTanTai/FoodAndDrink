using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class DeleteAccountRoleService : IDeleteAccountRoleService<RolesOfUserKey>
    {
        private readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUserDAO;
        private readonly IBaseHelperService<RolesOfUserModel> _helper;

        public DeleteAccountRoleService(IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO, IBaseHelperService<RolesOfUserModel> helper)
        {
            _roleOfUserDAO = roleOfUserDAO;
            _helper = helper;
        }

        public async Task<int> DeleteAccountRoleAsync(RolesOfUserKey keys)
        {
            try
            {
                // Check if the role exists
                if (!await _helper.IsExistObject(keys, _roleOfUserDAO.GetByKeysAsync))
                    throw new KeyNotFoundException("The specified account role does not exist.");
                // Delete the role
                int result = await _roleOfUserDAO.DeleteByKeysAsync(keys);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new InvalidOperationException("An error occurred while deleting the account role.", ex);
            }
        }

        public async Task<int> DeleteAccountRolesAsync(IEnumerable<RolesOfUserKey> Listkeys)
        {
            try
            {
                if (!await _helper.DoAllKeysExistAsync(Listkeys, _roleOfUserDAO.GetByListKeysAsync))
                    throw new KeyNotFoundException("One or more specified account roles do not exist.");
                // Delete the roles
                int result = await _roleOfUserDAO.DeleteByListKeysAsync(Listkeys);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting the account roles.", ex);
            }
        }

        public async Task<int> DeleteByAccountIdAsync(uint accountId)
            => await DeleteByIdGenericAsync(accountId, _roleOfUserDAO.GetByAccountIdAsync, _roleOfUserDAO.DeleteByAccountIdAsync);

        public async Task<int> DeleteByAccountIdsAsync(IEnumerable<uint> accountIds)
            => await DeleteByIdsGenericAsync(accountIds, _roleOfUserDAO.GetByAccountIdsAsync, _roleOfUserDAO.DeleteByAccountIdsAsync);

        public async Task<int> DeleteByRoleIdAsync(uint roleId)
            => await DeleteByIdGenericAsync(roleId, _roleOfUserDAO.GetByRoleIdAsync, _roleOfUserDAO.DeleteByRoleIdAsync);

        public async Task<int> DeleteByRoleIdsAsync(IEnumerable<uint> roleIds)
            => await DeleteByIdsGenericAsync(roleIds, _roleOfUserDAO.GetByRoleIdsAsync, _roleOfUserDAO.DeleteByRoleIdsAsync);

        //DRY:

        private async Task<int> DeleteByIdGenericAsync(uint input, Func<uint, int?, Task<IEnumerable<RolesOfUserModel>>> searchFunc, Func<uint, Task<int>> deleteFunc)
        {
            try
            {
                // Check if the role exists
                var existingRoles = await searchFunc(input, null);
                if (!existingRoles.Any())
                    throw new KeyNotFoundException("The specified account role does not exist.");
                // Delete the role
                int result = await deleteFunc(input);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new InvalidOperationException("An error occurred while deleting the account role.", ex);
            }
        }

        private async Task<int> DeleteByIdsGenericAsync(IEnumerable<uint> inputs, Func<IEnumerable<uint>, int?, Task<IEnumerable<RolesOfUserModel>>> searchFunc, Func<IEnumerable<uint>, Task<int>> deleteFunc)
        {
            try
            {
                // Check if all roles exist
                var existingRoles = await searchFunc(inputs, null);
                if (!existingRoles.Any())
                    throw new KeyNotFoundException("One or more specified account roles do not exist.");
                // Delete the roles
                int result = await deleteFunc(inputs);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting the account roles.", ex);
            }
        }
    }
}
