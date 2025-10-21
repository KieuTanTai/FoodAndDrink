using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class LocationCityRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<LocationCity>(context, maxGetRecord), ILocationCityRepository
    {
        public async Task<LocationCity?> GetByNameAsync(string cityName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LocationCity>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<LocationCity?> GetByIdWithDistrictsAsync(uint cityId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
