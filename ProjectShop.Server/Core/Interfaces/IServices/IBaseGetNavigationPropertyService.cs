using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IBaseGetNavigationPropertyService<TEntity, TOptions> where TEntity : class where TOptions : class
    {
        Task<ServiceResult<TEntity>> GetNavigationPropertyByOptionsAsync(TEntity entity, TOptions options);
        Task<ServiceResults<TEntity>> GetNavigationPropertyByOptionsAsync(IEnumerable<TEntity> entities, TOptions options);
    }
}
