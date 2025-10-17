using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class ProductLotRepository(IDBContext context) : Repository<ProductLot>(context), IProductLotRepository
    {
        public async Task<IEnumerable<ProductLot>> GetByInventoryIdAsync(uint inventoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductLot>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductLot?> GetByIdWithDetailsAsync(uint productLotId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
