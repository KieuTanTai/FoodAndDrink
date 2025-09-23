using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class DeleteAccountRoleService : IDeleteAccountRoleServices<RolesOfUserKey>
    {
        private readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUserDAO;
        private readonly IBaseHelperServices<RolesOfUserModel> _helper;
        private readonly ILogService _logger;
        public DeleteAccountRoleService(IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUserDAO, IBaseHelperServices<RolesOfUserModel> helper, ILogService logger)
        {
            _helper = helper;
            _logger = logger;
            _roleOfUserDAO = roleOfUserDAO;
        }

        public async Task<JsonLogEntry> DeleteAccountRoleAsync(RolesOfUserKey keys)
        {
            try
            {
                // Check if the role exists
                if (!await _helper.IsExistObject(keys, _roleOfUserDAO.GetByKeysAsync))
                    return _logger.JsonLogWarning<RolesOfUserKey, DeleteAccountRoleService>("The specified account role does not exist.");
                // Delete the role
                int result = await _roleOfUserDAO.DeleteByKeysAsync(keys);
                if (result == 0)
                    return _logger.JsonLogWarning<RolesOfUserKey, DeleteAccountRoleService>("No account role was deleted.");
                return _logger.JsonLogInfo<RolesOfUserKey, DeleteAccountRoleService>($"Successfully deleted account role with keys: {keys}", affectedRows: result);
            }
            catch (Exception ex)
            {
                return _logger.JsonLogError<RolesOfUserKey, DeleteAccountRoleService>("An error occurred while deleting the account role.", ex);
            }
        }

        public async Task<JsonLogEntry> DeleteAccountRolesAsync(IEnumerable<RolesOfUserKey> Listkeys)
        {
            try
            {
                if (!await _helper.DoAllKeysExistAsync(Listkeys, _roleOfUserDAO.GetByListKeysAsync))
                    return _logger.JsonLogWarning<RolesOfUserKey, DeleteAccountRoleService>("One or more of the specified account roles do not exist.");
                // Delete the roles
                int result = await _roleOfUserDAO.DeleteByListKeysAsync(Listkeys);
                if (result == 0)
                    return _logger.JsonLogWarning<RolesOfUserKey, DeleteAccountRoleService>("No account roles were deleted.");
                return _logger.JsonLogInfo<RolesOfUserKey, DeleteAccountRoleService>($"Successfully deleted account roles with keys: {string.Join(", ", Listkeys)}", affectedRows: result);
            }
            catch (Exception ex)
            {
                return _logger.JsonLogError<RolesOfUserKey, DeleteAccountRoleService>("An error occurred while deleting the account roles.", ex);
            }
        }

        public async Task<JsonLogEntry> DeleteByAccountIdAsync(uint accountId)
            => await DeleteByIdGenericAsync(accountId, _roleOfUserDAO.GetByAccountIdAsync, _roleOfUserDAO.DeleteByAccountIdAsync);

        public async Task<JsonLogEntry> DeleteByAccountIdsAsync(IEnumerable<uint> accountIds)
            => await DeleteByIdsGenericAsync(accountIds, _roleOfUserDAO.GetByAccountIdsAsync, _roleOfUserDAO.DeleteByAccountIdsAsync);

        public async Task<JsonLogEntry> DeleteByRoleIdAsync(uint roleId)
            => await DeleteByIdGenericAsync(roleId, _roleOfUserDAO.GetByRoleIdAsync, _roleOfUserDAO.DeleteByRoleIdAsync);

        public async Task<JsonLogEntry> DeleteByRoleIdsAsync(IEnumerable<uint> roleIds)
            => await DeleteByIdsGenericAsync(roleIds, _roleOfUserDAO.GetByRoleIdsAsync, _roleOfUserDAO.DeleteByRoleIdsAsync);

        //DRY:
        private async Task<JsonLogEntry> DeleteByIdGenericAsync(uint input, Func<uint, int?, Task<IEnumerable<RolesOfUserModel>>> searchFunc, Func<uint, Task<int>> deleteFunc)
        {
            try
            {
                // Check if the role exists
                var existingRoles = await searchFunc(input, null);
                if (existingRoles == null || !existingRoles.Any())
                    return _logger.JsonLogWarning<RolesOfUserModel, DeleteAccountRoleService>("The specified account role does not exist.");
                // Delete the role
                int result = await deleteFunc(input);
                if (result == 0)
                    return _logger.JsonLogWarning<RolesOfUserModel, DeleteAccountRoleService>("No account role was deleted.");
                return _logger.JsonLogInfo<RolesOfUserModel, DeleteAccountRoleService>($"Successfully deleted account role with ID: {input}", affectedRows: result);
            }
            catch (Exception ex)
            {
                return _logger.JsonLogError<RolesOfUserModel, DeleteAccountRoleService>("An error occurred while deleting the account role.", ex);
            }
        }

        private async Task<JsonLogEntry> DeleteByIdsGenericAsync(IEnumerable<uint> inputs, Func<IEnumerable<uint>, int?, Task<IEnumerable<RolesOfUserModel>>> searchFunc, Func<IEnumerable<uint>, Task<int>> deleteFunc)
        {
            try
            {
                // Check if all roles exist
                var existingRoles = await searchFunc(inputs, null);
                if (existingRoles == null || !existingRoles.Any())
                    return _logger.JsonLogWarning<RolesOfUserModel, DeleteAccountRoleService>("The specified account roles do not exist.");
                // Delete the roles
                int result = await deleteFunc(inputs);
                if (result == 0)
                    return _logger.JsonLogWarning<RolesOfUserModel, DeleteAccountRoleService>("No account roles were deleted.");
                return _logger.JsonLogInfo<RolesOfUserModel, DeleteAccountRoleService>($"Successfully deleted account roles with IDs: {string.Join(", ", inputs)}", affectedRows: result);
            }
            catch (Exception ex)
            {
                return _logger.JsonLogError<RolesOfUserModel, DeleteAccountRoleService>("An error occurred while deleting the account roles.", ex);
            }
        }
    }
}
