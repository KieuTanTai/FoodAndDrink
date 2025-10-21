using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class DetailProductLotRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<DetailProductLot>(context, maxGetRecord), IDetailProductLotRepository
    {
        public async Task<IEnumerable<DetailProductLot>> GetByProductLotIdAsync(uint productLotId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetailProductLot>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<DetailProductLot?> GetByProductLotIdAndProductBarcodeAsync(uint productLotId, string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
