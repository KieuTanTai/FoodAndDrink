using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class ProductVegetableRepository(IDBContext context) : Repository<ProductVegetable>(context), IProductVegetableRepository
    {
        public async Task<ProductVegetable?> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductVegetable>> GetByOriginAsync(string origin, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
