using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface IUpdateRoleService
    {
        Task<JsonLogEntry> UpdateRoleStatusAsync(uint roleId, bool status);
        Task<JsonLogEntry> UpdateRoleNameAsync(uint roleId, string roleName);
        Task<IEnumerable<JsonLogEntry>> UpdateRoleNamesAsync(IEnumerable<uint> roleIds, IEnumerable<string> newRoleNames);
        Task<IEnumerable<JsonLogEntry>> UpdateRoleStatusAsync(IEnumerable<uint> roleIds, bool status);
    }
}
