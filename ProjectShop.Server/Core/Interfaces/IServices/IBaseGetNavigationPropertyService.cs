using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IBaseGetNavigationPropertyService<TEntity, TOptions> where TEntity : class where TOptions : class
    {
        Task<ServiceResult<TEntity>> GetNavigationPropertyByOptionsAsync(TEntity entity, TOptions options, [CallerMemberName] string? methodCall = null);
        Task<ServiceResults<TEntity>> GetNavigationPropertyByOptionsAsync(IEnumerable<TEntity> entities, TOptions options, [CallerMemberName] string? methodCall = null);
    }
}
