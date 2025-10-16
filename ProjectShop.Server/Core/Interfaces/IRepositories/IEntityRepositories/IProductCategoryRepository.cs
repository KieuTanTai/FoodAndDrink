using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        Task<IEnumerable<ProductCategory>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductCategory>> GetByCategoryIdAsync(uint categoryId, CancellationToken cancellationToken = default);
        Task<ProductCategory?> GetByProductBarcodeAndCategoryIdAsync(string productBarcode, uint categoryId, CancellationToken cancellationToken = default);
    }
}
