using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IProductSnackRepository : IRepository<ProductSnack>
    {
        Task<ProductSnack?> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductSnack>> GetByWeightRangeAsync(decimal minWeight, decimal maxWeight, CancellationToken cancellationToken = default);
    }
}
