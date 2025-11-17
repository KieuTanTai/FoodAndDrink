using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class AccountRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule, IDefaultPageSizeRule defaultPageSizeRule) : Repository<Account>(context, maxReturnRecordsRule, defaultPageSizeRule), IAccountRepository
    {

        #region Legacy Navigation Methods (for interface compatibility)

        public async Task<Account?> GetNavigationByIdAsync(uint id, bool isGetAuth, bool isGetPermission, CancellationToken cancellationToken)
        {
            var query = _dbSet.AsQueryable();
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
                query = query.Include(account => account.AccountRoles);
            return query;
        }
        #endregion

        #region Query Methods

        #region Query by UserName

        public async Task<Account?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(account => account.UserName == userName, cancellationToken);

        public async Task<Account?> GetByUserNameAndPasswordAsync(string userName, string password, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(account => account.UserName == userName && account.Password == password, cancellationToken);

        public async Task<IEnumerable<Account>> GetManyByUserNamesAsync(IEnumerable<string> userNames, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;
            return await _dbSet
                .Where(account => userNames.Contains(account.UserName) && account.AccountId > cursor)
                .OrderBy(account => account.AccountId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Account>> SearchByUserNameContainsAsync(string searchTerm, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;
            return await _dbSet
                .Where(account => account.UserName.Contains(searchTerm) && account.AccountId > cursor)
                .OrderBy(account => account.AccountId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }
        
        #endregion

        #region Query by Status

        public async Task<IEnumerable<Account>> GetByStatusAsync(bool status, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;
            return await _dbSet
                .Where(account => account.AccountStatus == status && account.AccountId > cursor)
                .OrderBy(account => account.AccountId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }
        #endregion

        #region Query by AccountCreatedDate

        public async Task<IEnumerable<Account>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
            => await GetByDateTimeRangeAsync(startDate, endDate, account => account.AccountCreatedDate, true, fromRecord, pageSize, cancellationToken);

        #endregion

        #region Query by AccountLastUpdatedDate

        public async Task<IEnumerable<Account>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
            => await GetByDateTimeRangeAsync(startDate, endDate, account => account.AccountLastUpdatedDate, true, fromRecord, pageSize, cancellationToken);

        #endregion

        #region Query with Navigation Properties

        public async Task<Account?> GetNavigationByIdAsync(uint accountId, AccountNavigationOptions options, CancellationToken cancellationToken)
        {
            var query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query.FirstOrDefaultAsync(account => account.AccountId == accountId, cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetNavigationByIdsAsync(IEnumerable<uint> accountIds, AccountNavigationOptions options, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;
            var query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query
                .Where(account => accountIds.Contains(account.AccountId) && account.AccountId > cursor)
                .OrderBy(account => account.AccountId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }
        #endregion

        #endregion

        #region Explicit Load Methods

        #region Single Entity Explicit Load

        public async Task<Account> ExplicitLoadAsync(Account entity, AccountNavigationOptions options, CancellationToken cancellationToken)
        {
            await ExplicitLoadOneToOneAsync(entity, options, cancellationToken);
            await ExplicitLoadOneToManyAsync(entity, options, cancellationToken);
            return entity;
        }

        private async Task ExplicitLoadOneToOneAsync(Account entity, AccountNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetPerson)
                await _context.Entry(entity).Reference(account => account.Person).LoadAsync(cancellationToken);
        }

        private async Task ExplicitLoadOneToManyAsync(Account entity, AccountNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetAccountAdditionalPermissions)
                await _context.Entry(entity).Collection(account => account.AccountAdditionalPermissions).LoadAsync(cancellationToken);
            if (options.IsGetAccountRoles)
                await _context.Entry(entity).Collection(account => account.AccountRoles).LoadAsync(cancellationToken);
        }
        #endregion

        #region Bulk Entities Explicit Load

        public async Task<IEnumerable<Account>> ExplicitLoadAsync(IEnumerable<Account> entities, AccountNavigationOptions options, CancellationToken cancellationToken)
        {
            var accountIds = entities.Select(account => account.AccountId).Distinct().ToList();
            List<Person> persons = [];
            List<AccountAdditionalPermission> additionalPermissions = [];
            List<AccountRole> roles = [];
            if (options.IsGetPerson)
                persons = await _context.People.Where(person => accountIds.Contains(person.AccountId)).ToListAsync(cancellationToken);
            if (options.IsGetAccountAdditionalPermissions)
                additionalPermissions = await _context.AccountAdditionalPermissions.Where(permission => accountIds.Contains(permission.AccountId)).ToListAsync(cancellationToken);
            if (options.IsGetAccountRoles)
                roles = await _context.AccountRoles.Where(role => accountIds.Contains(role.AccountId)).ToListAsync(cancellationToken);
            return MappingToAccounts(entities, persons, additionalPermissions, roles);
        }
        #endregion

        #endregion

        #region Apply Navigation Options Methods

        private static IQueryable<Account> ApplyNavigationOptions(IQueryable<Account> query, AccountNavigationOptions options)
        {
            query = ApplyOneToOneNavigationOptions(query, options);
            query = ApplyOneToManyNavigationOptions(query, options);
            return query;
        }

        private static IQueryable<Account> ApplyOneToOneNavigationOptions(IQueryable<Account> query, AccountNavigationOptions options)
        {
            if (options.IsGetPerson)
                query = query.Include(account => account.Person);
            return query;
        }

        private static IQueryable<Account> ApplyOneToManyNavigationOptions(IQueryable<Account> query, AccountNavigationOptions options)
        {
            if (options.IsGetAccountAdditionalPermissions)
                query = query.Include(account => account.AccountAdditionalPermissions);
            if (options.IsGetAccountRoles)
                query = query.Include(account => account.AccountRoles);
            return query;
        }
        #endregion

        #region Mapping Methods

        private static IEnumerable<Account> MappingToAccounts(IEnumerable<Account> accounts, List<Person> persons, List<AccountAdditionalPermission> accountAdditionalPermissions, List<AccountRole> roles)
        {
            if (accounts == null || !accounts.Any())
                return [];
            Dictionary<uint, Person> personsDict = [];
            var permissionsLookup = Enumerable.Empty<AccountAdditionalPermission>().ToLookup(key => 0U);
            var rolesLookup = Enumerable.Empty<AccountRole>().ToLookup(key => 0U);
            if (persons is { Count: > 0 })
                personsDict = persons.ToDictionary(person => person.AccountId);
            if (accountAdditionalPermissions is { Count: > 0 })
                permissionsLookup = accountAdditionalPermissions.ToLookup(permission => permission.AccountId);
            if (roles is { Count: > 0 })
                rolesLookup = roles.ToLookup(role => role.AccountId);
            foreach (var account in accounts)
            {
                if (personsDict.TryGetValue(account.AccountId, out var person))
                    account.Person = person ?? new Person();
                account.AccountAdditionalPermissions = [.. permissionsLookup[account.AccountId]];
                account.AccountRoles = [.. rolesLookup[account.AccountId]];
            }
            return accounts;
        }
        #endregion
    }
}
