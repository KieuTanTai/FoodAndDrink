using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IRoleOfUserDAO<T, TKey> : IGetDataByDateTimeAsync<T>, IGetByKeysAsync<T, TKey>, IDeleteByKeysAsync<TKey> where T : class where TKey : struct
    {
        Task<IEnumerable<T>> GetByAccountIdAsync(uint accountId);
        Task<IEnumerable<T>> GetByRoleIdAsync(uint roleId);
        Task<int> DeleteByAccountIdAsync(uint accountId);
        Task<int> DeleteByRoleIdAsync(uint roleId);
    }
}
