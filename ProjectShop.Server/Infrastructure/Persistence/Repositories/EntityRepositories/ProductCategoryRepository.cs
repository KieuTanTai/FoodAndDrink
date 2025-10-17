using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class ProductCategoryRepository(IDBContext context) : Repository<ProductCategory>(context), IProductCategoryRepository
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
