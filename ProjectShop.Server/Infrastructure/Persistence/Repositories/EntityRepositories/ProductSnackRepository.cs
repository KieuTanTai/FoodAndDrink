using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class ProductSnackRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<ProductSnack>(context, maxGetRecord), IProductSnackRepository
    {
        public async Task<ProductSnack?> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductSnack>> GetByWeightRangeAsync(decimal minWeight, decimal maxWeight, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
