using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface IDeleteAccountRoleService<TKey> where TKey : struct
    {
        Task<JsonLogEntry> DeleteAccountRoleAsync(TKey keys);
        Task<JsonLogEntry> DeleteByAccountIdAsync(uint accountId);
        Task<JsonLogEntry> DeleteByAccountIdsAsync(IEnumerable<uint> accountIds);
        Task<JsonLogEntry> DeleteAccountRolesAsync(IEnumerable<TKey> Listkeys);
        Task<JsonLogEntry> DeleteByRoleIdAsync(uint roleId);
        Task<JsonLogEntry> DeleteByRoleIdsAsync(IEnumerable<uint> roleIds);
    }
}
