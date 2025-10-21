using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class AccountRepository(IDBContext context, IMaxGetRecord maxGetRecord) : Repository<Account>(context, maxGetRecord), IAccountRepository
    {
        #region Query by UserName

        public async Task<Account?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(a => a.UserName == userName, cancellationToken);
        }

        public async Task<Account?> GetByUserNameAndPasswordAsync(string userName, string password, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(a => a.UserName == userName && a.Password == password, cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetByUserNamesAsync(IEnumerable<string> userNames, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(a => userNames.Contains(a.UserName))
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by Status

        public async Task<IEnumerable<Account>> GetByStatusAsync(bool status, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(a => a.AccountStatus == status)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by AccountCreatedDate

        public async Task<IEnumerable<Account>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(a => a.AccountCreatedDate >= startDate && a.AccountCreatedDate <= endDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetByCreatedYearAsync(int year, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(a => a.AccountCreatedDate.Year == year)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetByCreatedMonthAndYearAsync(int year, int month, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(a => a.AccountCreatedDate.Year == year && a.AccountCreatedDate.Month == month)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by AccountLastUpdatedDate

        public async Task<IEnumerable<Account>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(a => a.AccountLastUpdatedDate >= startDate && a.AccountLastUpdatedDate <= endDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetByLastUpdatedYearAsync(int year, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(a => a.AccountLastUpdatedDate.Year == year)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetByLastUpdatedMonthAndYearAsync(int year, int month, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(a => a.AccountLastUpdatedDate.Year == year && a.AccountLastUpdatedDate.Month == month)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query with Navigation Properties

        public async Task<Account?> GetByIdWithNavigationAsync(uint accountId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(a => a.AccountAdditionalPermissions)
                .Include(a => a.AccountRoles)
                    .ThenInclude(ar => ar.Role)
                .Include(a => a.Person)
                .FirstOrDefaultAsync(a => a.AccountId == accountId, cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetAllWithNavigationAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(a => a.AccountAdditionalPermissions)
                .Include(a => a.AccountRoles)
                    .ThenInclude(ar => ar.Role)
                .Include(a => a.Person)
                .ToListAsync(cancellationToken);
        }

        #endregion
    }
}
