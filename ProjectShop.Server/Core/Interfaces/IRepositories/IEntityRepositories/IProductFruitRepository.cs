using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IProductFruitRepository : IRepository<ProductFruit>
    {
        Task<ProductFruit?> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductFruit>> GetByOriginAsync(string origin, CancellationToken cancellationToken = default);
    }
}
