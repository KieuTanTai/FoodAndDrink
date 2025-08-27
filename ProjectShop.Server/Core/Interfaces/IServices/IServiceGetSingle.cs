using ProjectShop.Server.Core.ObjectValue;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IServiceGetSingle<TEntity, TOptions>
        where TEntity : class, new()
        where TOptions : class
    {
        Task<ServiceResult<TEntity>> QueryAsync<TParam>(
            TParam param,
            Func<TParam, Task<TEntity?>> queryFunc,
            TOptions? options = null, [CallerMemberName] string? methodCall = null);
    }
}
