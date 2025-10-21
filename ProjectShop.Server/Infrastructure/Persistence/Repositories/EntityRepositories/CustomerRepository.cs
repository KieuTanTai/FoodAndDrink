using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class CustomerRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<Customer>(context, maxGetRecord), ICustomerRepository
    {
        public async Task<Customer?> GetByPersonIdAsync(uint personId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetByPersonIdsAsync(IEnumerable<uint> personIds, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetByLoyaltyPointsRangeAsync(decimal minPoints, decimal maxPoints, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetTopByLoyaltyPointsAsync(int topCount, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetByRegistrationDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetByRegistrationYearAsync(int year, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetByRegistrationMonthAndYearAsync(int year, int month, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer?> GetByIdWithNavigationAsync(uint customerId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllWithNavigationAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
