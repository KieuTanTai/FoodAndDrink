using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class DetailSaleEventRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule, IDefaultPageSizeRule defaultPageSizeRule) : Repository<DetailSaleEvent>(context, maxReturnRecordsRule, defaultPageSizeRule), IDetailSaleEventRepository
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
