using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class DetailSaleEventRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<DetailSaleEvent>(context, maxGetRecord), IDetailSaleEventRepository
    {
        public async Task<IEnumerable<DetailSaleEvent>> GetBySaleEventIdAsync(uint saleEventId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetailSaleEvent>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<DetailSaleEvent?> GetBySaleEventIdAndProductBarcodeAsync(uint saleEventId, string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
