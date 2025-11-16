using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class ProductDrinkRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule) : Repository<ProductDrink>(context, maxReturnRecordsRule), IProductDrinkRepository
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
