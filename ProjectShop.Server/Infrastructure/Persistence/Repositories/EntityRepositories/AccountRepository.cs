using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class AccountRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<Account>(context, maxGetRecord), IAccountRepository
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
            => await GetByTimeAsync(a => a.AccountCreatedDate >= startDate && a.AccountCreatedDate <= endDate, cancellationToken);

        public async Task<IEnumerable<Account>> GetByCreatedYearAsync(int year, CancellationToken cancellationToken = default)
            => await GetByTimeAsync(a => a.AccountCreatedDate.Year == year, cancellationToken);

        public async Task<IEnumerable<Account>> GetByCreatedMonthAndYearAsync(int year, int month, CancellationToken cancellationToken = default)
            => await GetByTimeAsync(a => a.AccountCreatedDate.Year == year && a.AccountCreatedDate.Month == month, cancellationToken);

        #endregion

        #region Query by AccountLastUpdatedDate

        public async Task<IEnumerable<Account>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
            => await GetByTimeAsync(a => a.AccountLastUpdatedDate >= startDate && a.AccountLastUpdatedDate <= endDate, cancellationToken);

        public async Task<IEnumerable<Account>> GetByLastUpdatedYearAsync(int year, CancellationToken cancellationToken = default)
            => await GetByTimeAsync(a => a.AccountLastUpdatedDate.Year == year, cancellationToken);

        public async Task<IEnumerable<Account>> GetByLastUpdatedMonthAndYearAsync(int year, int month, CancellationToken cancellationToken = default)
            => await GetByTimeAsync(a => a.AccountLastUpdatedDate.Year == year && a.AccountLastUpdatedDate.Month == month, cancellationToken);

        #endregion

        #region Query with Navigation Properties

        public async Task<Account?> GetNavigationByIdAsync(uint accountId, AccountNavigationOptions options, CancellationToken cancellationToken = default)
        {
            IQueryable<Account> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);

            return await query.FirstOrDefaultAsync(a => a.AccountId == accountId, cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetNavigationByIdsAsync(IEnumerable<uint> accountIds, AccountNavigationOptions options, CancellationToken cancellationToken = default)
        {
            IQueryable<Account> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query
                .Where(a => accountIds.Contains(a.AccountId))
                .ToListAsync(cancellationToken);
        }

        public async Task ExplicitLoadAsync(Account account, AccountNavigationOptions options, CancellationToken cancellationToken = default)
        {
            if (options.IsGetPerson)
                await _context.Entry(account).Reference(acc => acc.Person).LoadAsync(cancellationToken);
            if (options.IsGetAccountAdditionalPermissions)
                await _context.Entry(account).Collection(acc => acc.AccountAdditionalPermissions).LoadAsync(cancellationToken);
            if (options.IsGetAccountRoles)
                await _context.Entry(account).Collection(acc => acc.AccountRoles).LoadAsync(cancellationToken);
        }

        public async Task ExplicitLoadAsync(IEnumerable<Account> accounts, AccountNavigationOptions options, CancellationToken cancellationToken = default)
        {
            List<Person> persons = [];
            List<AccountAdditionalPermission> additionalPermissions = [];
            List<AccountRole> roles = [];
            var accountIds = accounts.Select(account => account.AccountId).ToList();
            if (options.IsGetPerson)
                persons = await _context.People.Where(person => accountIds.Contains(person.AccountId)).ToListAsync(cancellationToken);
            if (options.IsGetAccountAdditionalPermissions)
                additionalPermissions = await _context.AccountAdditionalPermissions.Where(permission => accountIds.Contains(permission.AccountId)).ToListAsync(cancellationToken);
            if (options.IsGetAccountRoles)
                roles = await _context.AccountRoles.Where(role => accountIds.Contains(role.AccountId)).ToListAsync(cancellationToken);

            MappingToAccounts(accounts, persons, additionalPermissions, roles);
        }

        #endregion

        #region Helper Methods for Mapping and Apply Navigation Options

        private static IQueryable<Account> ApplyNavigationOptions(IQueryable<Account> query, AccountNavigationOptions options)
        {
            if (options.IsGetPerson)
                query = query.Include(a => a.Person);
            if (options.IsGetAccountAdditionalPermissions)
                query = query.Include(a => a.AccountAdditionalPermissions);
            if (options.IsGetAccountRoles)
                query = query.Include(a => a.AccountRoles);

            return query;
        }

        private static void MappingToAccounts(IEnumerable<Account> accounts, List<Person> persons,
            List<AccountAdditionalPermission> accountAdditionalPermissions, List<AccountRole> roles)
        {
            if (accounts == null || !accounts.Any())
                return;
            Dictionary<uint, Person> personsDict = [];
            ILookup<uint, AccountAdditionalPermission> permissionsLookup = Enumerable.Empty<AccountAdditionalPermission>().ToLookup(key => default(uint));
            ILookup<uint, AccountRole> rolesLookup = Enumerable.Empty<AccountRole>().ToLookup(key => default(uint));

            if (persons != null && persons.Count > 0)
                personsDict = persons.ToDictionary(person => person.AccountId);
            if (accountAdditionalPermissions != null && accountAdditionalPermissions.Count > 0)
                permissionsLookup = accountAdditionalPermissions.ToLookup(permission => permission.AccountId);
            if (roles != null && roles.Count > 0)
                rolesLookup = roles.ToLookup(role => role.AccountId);

            // mapping
            foreach (Account account in accounts) 
            {
                if (personsDict.TryGetValue(account.AccountId, out var person))
                    account.Person = person ?? new();
                account.AccountAdditionalPermissions = [.. permissionsLookup[account.AccountId]];
                account.AccountRoles = [.. rolesLookup[account.AccountId]];
            }
        }

        #endregion
    }
}
