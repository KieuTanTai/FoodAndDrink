using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Enums;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class AccountRoleRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule, IDefaultPageSizeRule defaultPageSizeRule) :
        Repository<AccountRole>(context, maxReturnRecordsRule, defaultPageSizeRule), IAccountRoleRepository
    {
        #region Query by AccountId and RoleId

        public async Task<AccountRole?> GetByAccountIdAndRoleIdAsync(uint accountId, uint roleId, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(accountRole => accountRole.AccountId == accountId && accountRole.RoleId == roleId, cancellationToken);

        public async Task<IEnumerable<AccountRole>> GetByAccountIdAsync(uint accountId, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(accountRole => accountRole.AccountId == accountId && accountRole.AccountRoleId > cursor)
                .OrderBy(accountRole => accountRole.AccountRoleId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<AccountRole>> GetByRoleIdAsync(uint roleId, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(accountRole => accountRole.RoleId == roleId && accountRole.AccountRoleId > cursor)
                .OrderBy(accountRole => accountRole.AccountRoleId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by Status

        public async Task<IEnumerable<AccountRole>> GetByStatusAsync(bool? status, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(accountRole => accountRole.AccountRoleStatus == status && accountRole.AccountRoleId > cursor)
                .OrderBy(accountRole => accountRole.AccountRoleId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by AccountRoleAssignedDate
        public Task<IEnumerable<AccountRole>> GetByAssignDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
            => GetByDateTimeRangeAsync(startDate, endDate, accountRole => accountRole.AccountRoleAssignedDate, true, fromRecord, pageSize, cancellationToken);

        #endregion

        #region Query with Navigation Properties

        public async Task<AccountRole?> GetNavigationByIdAsync(uint id, AccountRoleNavigationOptions options, CancellationToken cancellationToken)
        {
            var query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query.FirstOrDefaultAsync(accountRole => accountRole.AccountRoleId == id, cancellationToken);
        }

        public async Task<IEnumerable<AccountRole>> GetNavigationByIdsAsync(IEnumerable<uint> ids, AccountRoleNavigationOptions options, uint? fromRecord,
            uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            var query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query
                .Where(accountRole => ids.Contains(accountRole.AccountRoleId) && accountRole.AccountRoleId > cursor)
                .OrderBy(accountRole => accountRole.AccountRoleId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<AccountRole> ExplicitLoadAsync(AccountRole entity, AccountRoleNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetAccount)
                await _context.Entry(entity).Reference(accountRole => accountRole.Account).LoadAsync(cancellationToken);
            if (options.IsGetRole)
                await _context.Entry(entity).Reference(accountRole => accountRole.Role).LoadAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<AccountRole>> ExplicitLoadAsync(IEnumerable<AccountRole> entities, AccountRoleNavigationOptions options, CancellationToken cancellationToken)
        {
            List<Account> accounts = [];
            List<Role> roles = [];
            var accountIds = entities.Select(accountRole => accountRole.AccountId).Distinct().ToList();
            var roleIds = entities.Select(accountRole => accountRole.RoleId).Distinct().ToList();

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
                query = query.Include(accountRole => accountRole.Account);
            if (options.IsGetRole)
                query = query.Include(accountRole => accountRole.Role);

            return query;
        }

        private static IEnumerable<AccountRole> MappingToAccountRoles(IEnumerable<AccountRole> accountRoles,
            List<Account> accounts, List<Role> roles)
        {
            if (accountRoles == null || !accountRoles.Any())
                return [];

            Dictionary<uint, Account> accountsDict = [];
            Dictionary<uint, Role> rolesDict = [];

            if (accounts is { Count: > 0 })
                accountsDict = accounts.ToDictionary(account => account.AccountId);
            if (roles is { Count: > 0 })
                rolesDict = roles.ToDictionary(role => role.RoleId);

            // mapping
            foreach (var accountRole in accountRoles)
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
