using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class LocationTypeRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<LocationType>(context, maxGetRecord), ILocationTypeRepository
    {
        public async Task<LocationType?> GetByTypeNameAsync(string typeName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LocationType>> SearchByTypeNameAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
