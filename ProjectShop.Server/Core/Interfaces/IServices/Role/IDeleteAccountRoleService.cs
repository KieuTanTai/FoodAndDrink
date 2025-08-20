namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface IDeleteAccountRoleService<TKey> where TKey : struct
    {
        Task<int> DeleteAccountRoleAsync(TKey keys);
        Task<int> DeleteByAccountIdAsync(uint accountId);
        Task<int> DeleteByAccountIdsAsync(IEnumerable<uint> accountIds);
        Task<int> DeleteAccountRolesAsync(IEnumerable<TKey> Listkeys);
        Task<int> DeleteByRoleIdAsync(uint roleId);
        Task<int> DeleteByRoleIdsAsync(IEnumerable<uint> roleIds);
    }
}
