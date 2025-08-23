using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface IDeleteAccountRoleService<TKey> where TKey : struct
    {
        Task<JsonLogEntry> DeleteAccountRoleAsync(TKey keys);
        Task<JsonLogEntry> DeleteByAccountIdAsync(uint accountId);
        Task<IEnumerable<JsonLogEntry>> DeleteByAccountIdsAsync(IEnumerable<uint> accountIds);
        Task<IEnumerable<JsonLogEntry>> DeleteAccountRolesAsync(IEnumerable<TKey> Listkeys);
        Task<JsonLogEntry> DeleteByRoleIdAsync(uint roleId);
        Task<IEnumerable<JsonLogEntry>> DeleteByRoleIdsAsync(IEnumerable<uint> roleIds);
    }
}
