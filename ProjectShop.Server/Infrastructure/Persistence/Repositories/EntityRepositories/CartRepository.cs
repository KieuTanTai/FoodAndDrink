using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class CartRepository(IDBContext context) : Repository<Cart>(context), ICartRepository
    {
        public async Task<IEnumerable<Cart>> GetByCustomerIdAsync(uint customerId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart?> GetActiveCartByCustomerIdAsync(uint customerId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Cart>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart?> GetByIdWithDetailsAsync(uint cartId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
