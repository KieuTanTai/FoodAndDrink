using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class ProductCategoryRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<ProductCategory>(context, maxGetRecord), IProductCategoryRepository
    {
        public async Task<IEnumerable<ProductCategory>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductCategory>> GetByCategoryIdAsync(uint categoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductCategory?> GetByProductBarcodeAndCategoryIdAsync(string productBarcode, uint categoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
