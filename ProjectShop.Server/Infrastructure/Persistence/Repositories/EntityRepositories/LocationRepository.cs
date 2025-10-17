using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class LocationRepository(IDBContext context) : Repository<Location>(context), ILocationRepository
    {
        public async Task<IEnumerable<Location>> GetByLocationTypeIdAsync(uint locationTypeId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Location>> GetByLocationCityIdAsync(uint locationCityId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Location>> GetByLocationDistrictIdAsync(uint locationDistrictId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Location>> GetByLocationWardIdAsync(uint locationWardId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Location?> GetByIdWithFullAddressAsync(uint locationId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
