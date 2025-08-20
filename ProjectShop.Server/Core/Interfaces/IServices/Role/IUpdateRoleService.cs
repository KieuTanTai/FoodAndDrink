namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface IUpdateRoleService
    {
        Task<int> UpdateRoleStatusAsync(uint roleId, bool status);
        Task<int> UpdateRoleNameAsync(uint roleId, string roleName);
        Task<int> UpdateRoleNamesAsync(IEnumerable<uint> roleIds, IEnumerable<string> newRoleNames);
        Task<int> UpdateRoleStatusAsync(IEnumerable<uint> roleIds, bool status);
    }
}
