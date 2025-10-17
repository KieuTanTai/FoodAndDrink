using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class DetailInventoryRepository(IDBContext context) : Repository<DetailInventory>(context), IDetailInventoryRepository
    {
        public async Task<IEnumerable<DetailInventory>> GetByInventoryIdAsync(uint inventoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetailInventory>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<DetailInventory?> GetByInventoryIdAndProductBarcodeAsync(uint inventoryId, string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetailInventory>> GetByAddedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetailInventory>> GetLowStockItemsAsync(int threshold, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
