using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IProductMeatRepository : IRepository<ProductMeat>
    {
        Task<ProductMeat?> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductMeat>> GetByOriginAsync(string origin, CancellationToken cancellationToken = default);
    }
}
