using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class DetailInventoryMovementRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<DetailInventoryMovement>(context, maxGetRecord), IDetailInventoryMovementRepository
    {
        public async Task<IEnumerable<DetailInventoryMovement>> GetByInventoryMovementIdAsync(uint inventoryMovementId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetailInventoryMovement>> GetByProductLotIdAsync(uint productLotId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetailInventoryMovement>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
