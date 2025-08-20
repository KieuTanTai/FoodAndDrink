namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IRoleOfUserDAO<TEntity, TKey> : IGetDataByDateTimeAsync<TEntity>, IGetByKeysAsync<TEntity, TKey>, IDeleteByKeysAsync<TKey> where TEntity : class where TKey : struct
    {
        Task<IEnumerable<TEntity>> GetByAccountIdAsync(uint accountId);
        Task<IEnumerable<TEntity>> GetByAccountIdAsync(uint accountId, int maxGetCount);
        Task<IEnumerable<TEntity>> GetByAccountIdsAsync(IEnumerable<uint> accountIds);
        Task<IEnumerable<TEntity>> GetByRoleIdAsync(uint roleId);
        Task<IEnumerable<TEntity>> GetByRoleIdAsync(uint roleId, int maxGetCount);
        Task<IEnumerable<TEntity>> GetByRoleIdsAsync(IEnumerable<uint> roleIds);
        Task<int> DeleteByAccountIdAsync(uint accountId);
        Task<int> DeleteByAccountIdsAsync(IEnumerable<uint> accountIds);
        Task<int> DeleteByRoleIdAsync(uint roleId);
        Task<int> DeleteByRoleIdsAsync(IEnumerable<uint> roleIds);
        Task<int> DeleteByListKeysAsync(IEnumerable<TKey> keys);
    }
}
