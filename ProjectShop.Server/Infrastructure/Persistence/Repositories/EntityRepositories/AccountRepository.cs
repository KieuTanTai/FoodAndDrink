using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class AccountRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) :
        Repository<Account>(context, maxGetRecord), IAccountRepository
    {
        #region Query by UserName

        public async Task<Account?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(account => account.UserName == userName, cancellationToken);

        public async Task<Account?> GetByUserNameAndPasswordAsync(string userName, string password, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(account => account.UserName == userName && account.Password == password, cancellationToken);

        public async Task<IEnumerable<Account>> GetByUserNamesAsync(IEnumerable<string> userNames, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            if (pageSize == null || pageSize == 0 || pageSize > _maxGetReturn)
                pageSize = _maxGetReturn;
            return await _dbSet
                .Where(a => userNames.Contains(a.UserName))
                .Skip((int)(fromRecord ?? 0))
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by Status

        public async Task<IEnumerable<Account>> GetByStatusAsync(bool status, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            if (pageSize == null || pageSize == 0 || pageSize > _maxGetReturn)
                pageSize = _maxGetReturn;
            return await _dbSet
                .Where(account => account.AccountStatus == status)
                .Skip((int)(fromRecord ?? 0))
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by AccountCreatedDate

        public async Task<IEnumerable<Account>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize,
            CancellationToken cancellationToken)
            => await GetByDateTimeRangeAsync(startDate, endDate, account => account.AccountCreatedDate, fromRecord, pageSize, cancellationToken);

        public async Task<IEnumerable<Account>> GetByCreatedYearAsync(int year, ECompareType eCompareType, uint? fromRecord, uint? pageSize,
             CancellationToken cancellationToken)
        {
            Func<Account, bool> predicate = await GetCompareConditions(year, eCompareType, account => account.AccountCreatedDate);
            return await GetByTimeAsync(predicate, fromRecord, pageSize, cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetByCreatedMonthAndYearAsync(int month, int year, ECompareType eCompareType, uint? fromRecord, uint? pageSize,
         CancellationToken cancellationToken)
        {
            Func<Account, bool> predicate = await GetCompareConditions(month, year, eCompareType, account => account.AccountCreatedDate);
            return await GetByTimeAsync(predicate, fromRecord, pageSize, cancellationToken);
        }

        #endregion

        #region Query by AccountLastUpdatedDate

        public async Task<IEnumerable<Account>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize,
            CancellationToken cancellationToken)
            => await GetByDateTimeRangeAsync(startDate, endDate, account => account.AccountLastUpdatedDate, fromRecord, pageSize, cancellationToken);

        public async Task<IEnumerable<Account>> GetByLastUpdatedYearAsync(int year, ECompareType eCompareType, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            Func<Account, bool> predicate = await GetCompareConditions(year, eCompareType, account => account.AccountLastUpdatedDate);
            return await GetByTimeAsync(predicate, fromRecord, pageSize, cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetByLastUpdatedMonthAndYearAsync(int month, int year,
            ECompareType eCompareType, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            Func<Account, bool> predicate = await GetCompareConditions(month, year, eCompareType, account => account.AccountLastUpdatedDate);
            return await GetByTimeAsync(predicate, fromRecord, pageSize, cancellationToken);
        }

        #endregion

        #region Query with Navigation Properties

        public async Task<Account?> GetNavigationByIdAsync(uint accountId, AccountNavigationOptions options, CancellationToken cancellationToken)
        {
            IQueryable<Account> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);

            return await query.FirstOrDefaultAsync(account => account.AccountId == accountId, cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetNavigationByIdsAsync(IEnumerable<uint> accountIds, AccountNavigationOptions options,
            uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            if (pageSize == null || pageSize == 0 || pageSize > _maxGetReturn)
                pageSize = _maxGetReturn;
            IQueryable<Account> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query
                .Where(a => accountIds.Contains(a.AccountId))
                .Skip((int)(fromRecord ?? 0))
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Account> ExplicitLoadAsync(Account account, AccountNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetPerson)
                await _context.Entry(account).Reference(acc => acc.Person).LoadAsync(cancellationToken);
            if (options.IsGetAccountAdditionalPermissions)
                await _context.Entry(account).Collection(acc => acc.AccountAdditionalPermissions).LoadAsync(cancellationToken);
            if (options.IsGetAccountRoles)
                await _context.Entry(account).Collection(acc => acc.AccountRoles).LoadAsync(cancellationToken);
            return account;
        }

        public async Task<IEnumerable<Account>> ExplicitLoadAsync(IEnumerable<Account> accounts, AccountNavigationOptions options,
            uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
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

            return MappingToAccounts(accounts, persons, additionalPermissions, roles);
        }

        public async Task<Account?> GetNavigationByIdAsync(uint id, bool isGetAuth, bool isGetPermission, CancellationToken cancellationToken)
        {
            IQueryable<Account> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptionsForAuth(query, isGetAuth, isGetPermission);
            return await query.FirstOrDefaultAsync(account => account.AccountId == id, cancellationToken);
        }

        public async Task<Account> ExplicitLoadAsync(Account entity, bool isGetAuth, bool isGetPermission, CancellationToken cancellationToken)
        {
            if (isGetAuth || isGetPermission)
            {
                await _context.Entry(entity)
                    .Collection(account => account.AccountAdditionalPermissions)
                    .Query()
                    .Include(permission => permission.Permission)
                    .LoadAsync(cancellationToken);

                await _context.Entry(entity)
                    .Collection(account => account.AccountRoles)
                    .Query()
                    .Include(role => role.Role)
                    .LoadAsync(cancellationToken);
            }
            else if (isGetAuth && !isGetPermission)
            {
                await _context.Entry(entity)
                    .Collection(account => account.AccountRoles)
                    .LoadAsync(cancellationToken);
            }
            return entity;
        }

        #endregion

        #region Helper Methods for Mapping and Apply Navigation Options

        private static IQueryable<Account> ApplyNavigationOptions(IQueryable<Account> query, AccountNavigationOptions options)
        {
            if (options.IsGetPerson)
                query = query.Include(account => account.Person);
            if (options.IsGetAccountAdditionalPermissions)
                query = query.Include(account => account.AccountAdditionalPermissions);
            if (options.IsGetAccountRoles)
                query = query.Include(account => account.AccountRoles);
            return query;
        }

        private static IQueryable<Account> ApplyNavigationOptionsForAuth(IQueryable<Account> query, bool isGetAuth, bool isGetPermission)
        {
            if (isGetAuth || isGetPermission)
            {
                query = query
                    .Include(account => account.AccountAdditionalPermissions)
                        .ThenInclude(permission => permission.Permission)
                    .Include(account => account.AccountRoles)
                        .ThenInclude(role => role.Role);
            }
            else if (isGetAuth && !isGetPermission)
            {
                query = query.Include(account => account.AccountRoles);
            }
            return query;
        }

        private static IEnumerable<Account> MappingToAccounts(IEnumerable<Account> accounts, List<Person> persons,
            List<AccountAdditionalPermission> accountAdditionalPermissions, List<AccountRole> roles)
        {
            if (accounts == null || !accounts.Any())
                return [];
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
            return accounts;
        }
        #endregion
    }
}
