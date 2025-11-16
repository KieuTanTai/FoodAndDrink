using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class InventoryMovementRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule) : Repository<InventoryMovement>(context, maxReturnRecordsRule), IInventoryMovementRepository
    {
        public async Task<IEnumerable<InventoryMovement>> GetByInventoryIdAsync(uint inventoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InventoryMovement>> GetByMovementTypeAsync(string movementType, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InventoryMovement>> GetByMovementDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<InventoryMovement?> GetByIdWithDetailsAsync(uint movementId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
