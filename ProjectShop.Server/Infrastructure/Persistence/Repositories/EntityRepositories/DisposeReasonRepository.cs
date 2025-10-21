using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class DisposeReasonRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<DisposeReason>(context, maxGetRecord), IDisposeReasonRepository
    {
        public async Task<DisposeReason?> GetByNameAsync(string reasonName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DisposeReason>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
