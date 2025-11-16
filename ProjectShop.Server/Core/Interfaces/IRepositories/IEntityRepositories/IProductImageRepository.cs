using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IProductImageRepository : IRepository<ProductImage>, IBaseExplicitLoadRepository<ProductImage, uint, ProductImageNavigationOptions>, IBaseGetByCreatedAndLastUpdatedDate<ProductImage>
    {
        Task<IEnumerable<ProductImage>> GetByProductBarcodeAsync(string productBarcode, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
    }
}
