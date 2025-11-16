using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IProductDrinkRepository : IRepository<ProductDrink>
    {
        Task<ProductDrink?> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductDrink>> GetByVolumeRangeAsync(decimal minVolume, decimal maxVolume, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductDrink>> GetByAlcoholContentRangeAsync(decimal minAlcoholContent, decimal maxAlcoholContent, CancellationToken cancellationToken = default);
    }
}
