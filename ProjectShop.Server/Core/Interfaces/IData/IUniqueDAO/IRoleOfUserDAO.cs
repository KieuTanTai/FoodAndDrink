namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IRoleOfUserDAO<TEntity, TKey> : IGetDataByDateTimeAsync<TEntity>, IGetByKeysAsync<TEntity, TKey>, IDeleteByKeysAsync<TKey> where TEntity : class where TKey : struct
    {
        Task<IEnumerable<TEntity>> GetByAccountIdAsync(uint accountId);
        Task<IEnumerable<TEntity>> GetByRoleIdAsync(uint roleId);
        Task<int> DeleteByAccountIdAsync(uint accountId);
        Task<int> DeleteByRoleIdAsync(uint roleId);
    }
}
