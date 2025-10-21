using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class PersonRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<Person>(context, maxGetRecord), IPersonRepository
    {
        public async Task<Person?> GetByAccountIdAsync(uint accountId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Person>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Person?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Person>> GetByEmailsAsync(IEnumerable<string> emails, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Person?> GetByPhoneAsync(string phone, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Person>> GetByPhonesAsync(IEnumerable<string> phones, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Person>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Person>> GetByFullNameAsync(string firstName, string lastName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Person>> GetByGenderAsync(bool isMale, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Person>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Person>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Person>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Person?> GetByIdWithNavigationAsync(uint personId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
