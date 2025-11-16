using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Core.Interfaces.IServices._IBase
{
    public interface IBaseHelperServices<TEntity> where TEntity : class
    {
        Task<bool> IsExistObject(string input, Func<string, CancellationToken, Task<TEntity?>> daoFunc, 
            CancellationToken cancellationToken = default);
        Task<bool> IsExistObject(uint input, Func<uint, CancellationToken, Task<TEntity?>> daoFunc, 
            CancellationToken cancellationToken = default);
        Task<bool> DoAllIdsExistAsync(IEnumerable<string> ids, Func<IEnumerable<string>, CancellationToken, 
            Task<IEnumerable<TEntity>>> daoFunc, CancellationToken cancellationToken = default);
        Task<bool> DoAllIdsExistAsync(IEnumerable<uint> ids, Func<IEnumerable<uint>, CancellationToken,
            Task<IEnumerable<TEntity>>> daoFunc, CancellationToken cancellationToken = default);
            
        Task<bool> DoNoneOfIdsExistAsync(IEnumerable<string> ids, Func<IEnumerable<string>, CancellationToken, 
            Task<IEnumerable<TEntity>>> daoFunc, CancellationToken cancellationToken = default);
        Task<bool> DoNoneOfIdsExistAsync(IEnumerable<uint> ids, Func<IEnumerable<uint>, CancellationToken, 
            Task<IEnumerable<TEntity>>> daoFunc, CancellationToken cancellationToken = default);
    }
}
