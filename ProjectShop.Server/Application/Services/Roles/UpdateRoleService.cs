using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class UpdateRoleService : IUpdateRoleService
    {
        private readonly IDAO<RoleModel> _baseDAO;
        private readonly ILogService _logger;

        public UpdateRoleService(IDAO<RoleModel> baseDAO, ILogService logger)
        {
            _baseDAO = baseDAO;
            _logger = logger;
        }

        public async Task<JsonLogEntry> UpdateRoleStatusAsync(uint roleId, bool status)
            => await UpdateRoleStatusAsync(roleId.ToString(), status, _baseDAO.GetSingleDataAsync);

        public async Task<IEnumerable<JsonLogEntry>> UpdateRoleStatusAsync(IEnumerable<uint> roleIds, bool status)
            => await UpdateRoleStatusAsync(roleIds.Select(id => id.ToString()), status, _baseDAO.GetByInputsAsync);

        public async Task<JsonLogEntry> UpdateRoleNameAsync(uint roleId, string newRoleName)
            => await UpdateRoleNameAsync(roleId.ToString(), newRoleName, _baseDAO.GetSingleDataAsync);

        public async Task<IEnumerable<JsonLogEntry>> UpdateRoleNamesAsync(IEnumerable<uint> roleIds, IEnumerable<string> newRoleNames)
            => await UpdateRoleNamesAsync(roleIds.Select(id => id.ToString()), newRoleNames, _baseDAO.GetByInputsAsync);

        private async Task<JsonLogEntry> UpdateRoleNameAsync(string input, string newName, Func<string, Task<RoleModel?>> daoFunc)
        {
            try
            {
                RoleModel? role = await daoFunc(input);
                if (role == null)
                    return _logger.JsonLogWarning<RoleModel, UpdateRoleService>($"Role with input {input} does not exist.");

                role.RoleName = newName;
                int affectedRows = await _baseDAO.UpdateAsync(role);
                if (affectedRows <= 0)
                    return _logger.JsonLogWarning<RoleModel, UpdateRoleService>($"Failed to update the role name for {input}.");
                return _logger.JsonLogInfo<RoleModel, UpdateRoleService>($"Successfully updated the role name for {input}.", affectedRows: affectedRows);
            }
            catch (Exception ex)
            {
                return _logger.JsonLogError<RoleModel, UpdateRoleService>($"An error occurred while updating the role name for {input}.", ex);
            }
        }

        private async Task<IEnumerable<JsonLogEntry>> UpdateRoleNamesAsync(IEnumerable<string> inputs, IEnumerable<string> newNames, Func<IEnumerable<string>, int?, Task<IEnumerable<RoleModel>>> daoFunc)
        {
            List<JsonLogEntry> logEntries = new List<JsonLogEntry>();
            try
            {
                var roles = await daoFunc(inputs, null);
                if (roles == null || !roles.Any())
                {
                    logEntries.Add(_logger.JsonLogWarning<RoleModel, UpdateRoleService>($"No roles found for inputs: {string.Join(", ", inputs)}."));
                    return logEntries;
                }

                int index = 0;
                int totalNames = newNames.Count();
                foreach (RoleModel role in roles)
                {
                    if (index < totalNames)
                        role.RoleName = newNames.ElementAt(index++);
                    else
                    {
                        logEntries.Add(_logger.JsonLogWarning<RoleModel, UpdateRoleService>($"Not enough new names provided for all roles. Stopped at index {index}."));
                        break;
                    }
                }
                int affectedRows = await _baseDAO.UpdateAsync(roles);
                if (affectedRows <= 0)
                {
                    logEntries.Add(_logger.JsonLogWarning<RoleModel, UpdateRoleService>($"Failed to update role names for multiple roles."));
                    return logEntries;
                }
                logEntries.Add(_logger.JsonLogInfo<RoleModel, UpdateRoleService>($"Successfully updated role names for {affectedRows} roles.", affectedRows: affectedRows));
                return logEntries;
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<RoleModel, UpdateRoleService>($"An error occurred while updating role names for multiple roles.", ex));
                return logEntries;
            }
        }

        private async Task<JsonLogEntry> UpdateRoleStatusAsync(string input, bool status, Func<string, Task<RoleModel?>> daoFunc)
        {
            try
            {
                RoleModel? role = await daoFunc(input);
                if (role == null)
                    return _logger.JsonLogWarning<RoleModel, UpdateRoleService>($"Role with input {input} does not exist.");

                role.RoleStatus = status;
                int affectedRows = await _baseDAO.UpdateAsync(role);
                if (affectedRows <= 0)
                    return _logger.JsonLogWarning<RoleModel, UpdateRoleService>($"Failed to update the role status for {input}.");
                return _logger.JsonLogInfo<RoleModel, UpdateRoleService>($"Successfully updated the role status for {input}.", affectedRows: affectedRows);
            }
            catch (Exception ex)
            {
                return _logger.JsonLogError<RoleModel, UpdateRoleService>($"An error occurred while updating the role status for {input}.", ex);
            }
        }

        private async Task<IEnumerable<JsonLogEntry>> UpdateRoleStatusAsync(IEnumerable<string> inputs, bool status, Func<IEnumerable<string>, int?, Task<IEnumerable<RoleModel>>> daoFunc)
        {
            List<JsonLogEntry> logEntries = new List<JsonLogEntry>();
            try
            {
                var roles = await daoFunc(inputs, null);
                if (roles == null || !roles.Any())
                {
                    logEntries.Add(_logger.JsonLogWarning<RoleModel, UpdateRoleService>($"No roles found for inputs: {string.Join(", ", inputs)}."));
                    return logEntries;
                }

                foreach (RoleModel role in roles)
                    role.RoleStatus = status;
                int affectedRows = await _baseDAO.UpdateAsync(roles);
                if (affectedRows <= 0)
                {
                    logEntries.Add(_logger.JsonLogWarning<RoleModel, UpdateRoleService>($"Failed to update role statuses for multiple roles."));
                    return logEntries;
                }
                logEntries.Add(_logger.JsonLogInfo<RoleModel, UpdateRoleService>($"Successfully updated role statuses for {affectedRows} roles.", affectedRows: affectedRows));
                return logEntries;
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<RoleModel, UpdateRoleService>($"An error occurred while updating role statuses for multiple roles.", ex));
                return logEntries;
            }
        }
    }
}
