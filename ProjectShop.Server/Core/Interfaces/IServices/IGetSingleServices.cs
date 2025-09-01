using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IGetSingleServices<TEntity, TOptions, TServiceCall>
        where TEntity : class, new()
        where TOptions : class
        where TServiceCall : class
    {
        Task<ServiceResult<TEntity>> GetAsync<TParam>(
            TParam param,
            Func<TParam, Task<TEntity?>> queryFunc,
            TOptions? options = null, [CallerMemberName] string? methodCall = null);
    }
}
