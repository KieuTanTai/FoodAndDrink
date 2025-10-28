using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Enums;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class AccountRoleRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) :
        Repository<AccountRole>(context, maxGetRecord), IAccountRoleRepository
    {
        #region Query by AccountId and RoleId

        public async Task<AccountRole?> GetByAccountIdAndRoleIdAsync(uint accountId, uint roleId, CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(ar => ar.AccountId == accountId && ar.RoleId == roleId, cancellationToken);

        public async Task<IEnumerable<AccountRole>> GetByAccountIdAsync(uint accountId, CancellationToken cancellationToken = default)
            => await _dbSet.Where(ar => ar.AccountId == accountId).ToListAsync(cancellationToken);

        public async Task<IEnumerable<AccountRole>> GetByRoleIdAsync(uint roleId, CancellationToken cancellationToken = default)
            => await _dbSet.Where(ar => ar.RoleId == roleId).ToListAsync(cancellationToken);

        #endregion

        #region Query by Status

        public async Task<IEnumerable<AccountRole>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default)
            => await _dbSet.Where(ar => ar.AccountRoleStatus == status).ToListAsync(cancellationToken);

        #endregion

        #region Query by AccountRoleAssignedDate

        public async Task<IEnumerable<AccountRole>> GetByAssignedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
            => await GetByDateTimeRangeAsync(startDate, endDate, ar => ar.AccountRoleAssignedDate, cancellationToken);

        public Task<IEnumerable<AccountRole>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
            => GetByAssignedDateRangeAsync(startDate, endDate, cancellationToken);

        public async Task<IEnumerable<AccountRole>> GetByYearAsync(int year, ECompareType eCompareType, CancellationToken cancellationToken = default)
        {
            Func<AccountRole, bool> predicate = await GetCompareConditions(year, eCompareType, ar => ar.AccountRoleAssignedDate);
            return await GetByTimeAsync(predicate, cancellationToken);
        }

        public async Task<IEnumerable<AccountRole>> GetByMonthAndYearAsync(int month, int year, ECompareType eCompareType, CancellationToken cancellationToken = default)
        {
            Func<AccountRole, bool> predicate = await GetCompareConditions(month, year, eCompareType, ar => ar.AccountRoleAssignedDate);
            return await GetByTimeAsync(predicate, cancellationToken);
        }

        #endregion

        #region Query with Navigation Properties

        public async Task<AccountRole?> GetNavigationByIdAsync(uint id, AccountRoleNavigationOptions options, CancellationToken cancellationToken = default)
        {
            IQueryable<AccountRole> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query.FirstOrDefaultAsync(ar => ar.AccountRoleId == id, cancellationToken);
        }

        public async Task<IEnumerable<AccountRole>> GetNavigationByIdsAsync(IEnumerable<uint> ids, AccountRoleNavigationOptions options, CancellationToken cancellationToken = default)
        {
            IQueryable<AccountRole> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query
                .Where(ar => ids.Contains(ar.AccountRoleId))
                .ToListAsync(cancellationToken);
        }

        public async Task<AccountRole> ExplicitLoadAsync(AccountRole entity, AccountRoleNavigationOptions options, CancellationToken cancellationToken = default)
        {
            if (options.IsGetAccount)
                await _context.Entry(entity).Reference(ar => ar.Account).LoadAsync(cancellationToken);
            if (options.IsGetRole)
                await _context.Entry(entity).Reference(ar => ar.Role).LoadAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<AccountRole>> ExplicitLoadAsync(IEnumerable<AccountRole> entities, AccountRoleNavigationOptions options, CancellationToken cancellationToken = default)
        {
            List<Account> accounts = [];
            List<Role> roles = [];
            var accountIds = entities.Select(ar => ar.AccountId).Distinct().ToList();
            var roleIds = entities.Select(ar => ar.RoleId).Distinct().ToList();

            if (options.IsGetAccount)
                accounts = await _context.Accounts.Where(account => accountIds.Contains(account.AccountId)).ToListAsync(cancellationToken);
            if (options.IsGetRole)
                roles = await _context.Roles.Where(role => roleIds.Contains(role.RoleId)).ToListAsync(cancellationToken);

            return MappingToAccountRoles(entities, accounts, roles);
        }

        #endregion

        #region Helper Methods for Mapping and Apply Navigation Options

        private static IQueryable<AccountRole> ApplyNavigationOptions(IQueryable<AccountRole> query, AccountRoleNavigationOptions options)
        {
            if (options.IsGetAccount)
                query = query.Include(ar => ar.Account);
            if (options.IsGetRole)
                query = query.Include(ar => ar.Role);

            return query;
        }

        private static IEnumerable<AccountRole> MappingToAccountRoles(IEnumerable<AccountRole> accountRoles,
            List<Account> accounts, List<Role> roles)
        {
            if (accountRoles == null || !accountRoles.Any())
                return [];

            Dictionary<uint, Account> accountsDict = [];
            Dictionary<uint, Role> rolesDict = [];

            if (accounts != null && accounts.Count > 0)
                accountsDict = accounts.ToDictionary(account => account.AccountId);
            if (roles != null && roles.Count > 0)
                rolesDict = roles.ToDictionary(role => role.RoleId);

            // mapping
            foreach (AccountRole accountRole in accountRoles)
            {
                if (accountsDict.TryGetValue(accountRole.AccountId, out var account))
                    accountRole.Account = account ?? new();
                if (rolesDict.TryGetValue(accountRole.RoleId, out var role))
                    accountRole.Role = role ?? new();
            }
            return accountRoles;
        }

        #endregion
    }
}
