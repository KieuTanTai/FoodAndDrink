using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>, IBaseExplicitLoadRepository<ProductCategory, uint, ProductCategoryNavigationOptions>
    {
        Task<IEnumerable<ProductCategory>> GetByProductBarcodeAsync(string productBarcode, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductCategory>> GetByCategoryIdAsync(uint categoryId, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
        Task<ProductCategory?> GetByProductBarcodeAndCategoryIdAsync(string productBarcode, uint categoryId, CancellationToken cancellationToken = default);
    }
}
