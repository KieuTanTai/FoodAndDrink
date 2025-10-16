using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IProductVegetableRepository : IRepository<ProductVegetable>
    {
        Task<ProductVegetable?> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductVegetable>> GetByOriginAsync(string origin, CancellationToken cancellationToken = default);
    }
}
