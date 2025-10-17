using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class ProductDrinkRepository(IDBContext context) : Repository<ProductDrink>(context), IProductDrinkRepository
    {
        public async Task<ProductDrink?> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDrink>> GetByVolumeRangeAsync(decimal minVolume, decimal maxVolume, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDrink>> GetByAlcoholContentRangeAsync(decimal minAlcoholContent, decimal maxAlcoholContent, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
