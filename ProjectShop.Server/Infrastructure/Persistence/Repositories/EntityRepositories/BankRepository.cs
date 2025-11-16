using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class BankRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule, IDefaultPageSizeRule defaultPageSizeRule) : Repository<Bank>(context, maxReturnRecordsRule, defaultPageSizeRule), IBankRepository
    {
        public async Task<Bank?> GetByNameAsync(string bankName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bank>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bank>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
