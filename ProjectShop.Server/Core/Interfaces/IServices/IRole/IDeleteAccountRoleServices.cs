using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface IDeleteAccountRoleServices<TKey> where TKey : struct
    {
        Task<JsonLogEntry> DeleteAccountRoleAsync(TKey keys, CancellationToken cancellationToken = default);
        Task<JsonLogEntry> DeleteByAccountIdAsync(uint accountId, CancellationToken cancellationToken = default);
        Task<JsonLogEntry> DeleteByAccountIdsAsync(IEnumerable<uint> accountIds, CancellationToken cancellationToken = default);
        Task<JsonLogEntry> DeleteAccountRolesAsync(IEnumerable<TKey> Listkeys, CancellationToken cancellationToken = default);
        Task<JsonLogEntry> DeleteByRoleIdAsync(uint roleId, CancellationToken cancellationToken = default);
        Task<JsonLogEntry> DeleteByRoleIdsAsync(IEnumerable<uint> roleIds, CancellationToken cancellationToken = default);
    }
}
