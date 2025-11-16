using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class ProductFruitRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule, IDefaultPageSizeRule defaultPageSizeRule) : Repository<ProductFruit>(context, maxReturnRecordsRule, defaultPageSizeRule), IProductFruitRepository
    {
        public async Task<ProductFruit?> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductFruit>> GetByOriginAsync(string origin, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
