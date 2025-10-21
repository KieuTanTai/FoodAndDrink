using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class DisposeProductRepository(IDBContext context, IMaxGetRecord maxGetRecord) : Repository<DisposeProduct>(context, maxGetRecord), IDisposeProductRepository
    {
        public async Task<IEnumerable<DisposeProduct>> GetByDisposeReasonIdAsync(uint disposeReasonId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DisposeProduct>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DisposeProduct>> GetByDisposeDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DisposeProduct>> GetByQuantityRangeAsync(int minQuantity, int maxQuantity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
