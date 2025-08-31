using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IBaseHelperService<TEntity> where TEntity : class
    {
        Task<bool> IsExistObject(string input, Func<string, Task<TEntity?>> daoFunc);
        Task<bool> IsExistObject(uint input, Func<uint, Task<TEntity?>> daoFunc);
        Task<bool> IsExistObject<TKey>(TKey keys, Func<TKey, Task<TEntity?>> daoFunc) where TKey : struct;

        Task<bool> DoAllIdsExistAsync(IEnumerable<string> ids, Func<IEnumerable<string>, Task<IEnumerable<TEntity>>> daoFunc);
        Task<bool> DoAllIdsExistAsync(IEnumerable<uint> ids, Func<IEnumerable<uint>, Task<IEnumerable<TEntity>>> daoFunc);
        Task<bool> DoAllKeysExistAsync<TKey>(IEnumerable<TKey> keys, Func<IEnumerable<TKey>, int?, Task<IEnumerable<TEntity>>> daoFunc) where TKey : struct;

        Task<bool> DoNoneOfIdsExistAsync(IEnumerable<string> ids, Func<IEnumerable<string>, Task<IEnumerable<TEntity>>> daoFunc);
        Task<bool> DoNoneOfIdsExistAsync(IEnumerable<uint> ids, Func<IEnumerable<uint>, Task<IEnumerable<TEntity>>> daoFunc);
        Task<bool> DoNoneOfKeysExistAsync<TKey>(IEnumerable<TKey> keys, Func<IEnumerable<TKey>, int?, Task<IEnumerable<TEntity>>> daoFunc) where TKey : struct;

        int? GetValidMaxRecord(int? maxGetCount);

        Task<Dictionary<uint, ServiceResults<TEntity>>> FilterValidEntities(
            IEnumerable<TEntity> entities,
            Func<TEntity, string> fieldSelector,
            Func<IEnumerable<string>, int?, Task<IEnumerable<TEntity>>> daoFunc, [CallerMemberName] string? methodCall = null);

        Task<Dictionary<uint, ServiceResults<TEntity>>> FilterValidEntities<TKey>(
            IEnumerable<TEntity> entities,
            Func<TEntity, TKey> fieldSelector,
            Func<IEnumerable<TKey>, int?, Task<IEnumerable<TEntity>>> daoFunc, [CallerMemberName] string? methodCall = null) where TKey : struct;
    }
}
