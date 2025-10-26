using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface IUpdateRoleServices
    {
        Task<JsonLogEntry> UpdateRoleStatusAsync(uint roleId, bool status, CancellationToken cancellationToken = default);
        Task<JsonLogEntry> UpdateRoleNameAsync(uint roleId, string roleName, CancellationToken cancellationToken = default);
        Task<IEnumerable<JsonLogEntry>> UpdateRoleNamesAsync(IEnumerable<uint> roleIds, IEnumerable<string> newRoleNames, CancellationToken cancellationToken = default);
        Task<IEnumerable<JsonLogEntry>> UpdateRoleStatusAsync(IEnumerable<uint> roleIds, bool status, CancellationToken cancellationToken = default);
    }
}
