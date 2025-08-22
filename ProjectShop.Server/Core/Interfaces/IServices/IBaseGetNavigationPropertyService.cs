using static Dapper.SqlMapper;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IBaseGetNavigationPropertyService<TEntity, TOptions> where TEntity : class where TOptions : class
    {
        Task<TEntity> GetNavigationPropertyByOptionsAsync(TEntity entity, TOptions? options);
        Task<IEnumerable<TEntity>> GetNavigationPropertyByOptionsAsync(IEnumerable<TEntity> entities, TOptions? options);
    }
}
