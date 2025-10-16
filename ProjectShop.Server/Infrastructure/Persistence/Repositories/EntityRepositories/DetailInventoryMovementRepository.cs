using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class DetailInventoryMovementRepository : Repository<DetailInventoryMovement>, IDetailInventoryMovementRepository
    {
        public DetailInventoryMovementRepository(IDBContext context) : base(context)
        {
        }

        // TODO: Implement all methods from IDetailInventoryMovementRepository
    }
}
