using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.IProduct
{
    public interface IAddProductServices<TEntity> where TEntity : class
    {
        Task<ServiceResult<TEntity>> AddProductAsync(TEntity entity);
        Task<ServiceResults<TEntity>> AddProductsAsync(IEnumerable<TEntity> entities);
    }
}
