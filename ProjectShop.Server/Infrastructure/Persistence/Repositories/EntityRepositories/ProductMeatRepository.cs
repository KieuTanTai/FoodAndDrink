using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class ProductMeatRepository(IDBContext context) : Repository<ProductMeat>(context), IProductMeatRepository
    {
        public async Task<ProductMeat?> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductMeat>> GetByOriginAsync(string origin, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
