using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class LocationWardRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<LocationWard>(context, maxGetRecord), ILocationWardRepository
    {
        public async Task<IEnumerable<LocationWard>> GetByLocationDistrictIdAsync(uint locationDistrictId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<LocationWard?> GetByNameAsync(string wardName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LocationWard>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
