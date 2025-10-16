using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class DetailInventoryRepository : Repository<DetailInventory>, IDetailInventoryRepository
    {
        public DetailInventoryRepository(IDBContext context) : base(context)
        {
        }

        // TODO: Implement all methods from IDetailInventoryRepository
    }
}
