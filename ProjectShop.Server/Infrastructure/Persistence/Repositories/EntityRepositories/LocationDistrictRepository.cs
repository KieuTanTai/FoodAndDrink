using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class LocationDistrictRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule) : Repository<LocationDistrict>(context, maxReturnRecordsRule), ILocationDistrictRepository
    {
        public async Task<IEnumerable<LocationDistrict>> GetByLocationCityIdAsync(uint locationCityId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<LocationDistrict?> GetByNameAsync(string districtName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LocationDistrict>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<LocationDistrict?> GetByIdWithWardsAsync(uint districtId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
