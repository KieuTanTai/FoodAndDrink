using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class LocationDistrictRepository : Repository<LocationDistrict>, ILocationDistrictRepository
    {
        public LocationDistrictRepository(IDBContext context) : base(context)
        {
        }

        // TODO: Implement all methods from ILocationDistrictRepository
    }
}
