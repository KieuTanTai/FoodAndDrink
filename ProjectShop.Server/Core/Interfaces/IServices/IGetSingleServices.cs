using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IGetSingleServices<TEntity, TServiceCall>
        where TEntity : class, new()
        where TServiceCall : class
    {
        Task<ServiceResult<TEntity>> GetAsync<TParam>(
            TParam param,
            Func<TParam, CancellationToken, Task<TEntity?>> queryFunc,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string? methodCall = null);
    }
}
