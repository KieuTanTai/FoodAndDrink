using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class LocationTypeRepository : Repository<LocationType>, ILocationTypeRepository
    {
        public LocationTypeRepository(IDBContext context) : base(context)
        {
        }

        // TODO: Implement all methods from ILocationTypeRepository
    }
}
