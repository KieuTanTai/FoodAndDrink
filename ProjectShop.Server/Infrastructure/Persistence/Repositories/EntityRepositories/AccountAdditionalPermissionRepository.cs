using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;
using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class AccountAdditionalPermissionRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule, IDefaultPageSizeRule defaultPageSizeRule) :
        Repository<AccountAdditionalPermission>(context, maxReturnRecordsRule, defaultPageSizeRule), IAccountAdditionalPermissionRepository
    {
        #region  Query by foreign id

        public async Task<AccountAdditionalPermission?> GetByAccountIdAndPermissionIdAsync(uint accountId, uint permissionId, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(permission => permission.AccountId == accountId && permission.PermissionId == permissionId, cancellationToken);

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByAccountIdAsync(uint accountId, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(permission => permission.AccountId == accountId && permission.AccountAdditionalPermissionId > cursor)
                .OrderBy(permission => permission.AccountAdditionalPermissionId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByIsGrantedAsync(bool isGranted, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(permission => permission.IsGranted == isGranted && permission.AccountAdditionalPermissionId > cursor)
                .OrderBy(permission => permission.AccountAdditionalPermissionId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByPermissionIdAsync(uint permissionId, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(permission => permission.PermissionId == permissionId && permission.AccountAdditionalPermissionId > cursor)
                .OrderBy(permission => permission.AccountAdditionalPermissionId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by status and time
        public async Task<IEnumerable<AccountAdditionalPermission>> GetByStatusAsync(bool status, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(permission => permission.AdditionalPermissionStatus == status && permission.AccountAdditionalPermissionId > cursor)
                .OrderBy(permission => permission.AccountAdditionalPermissionId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord,
            uint? pageSize, CancellationToken cancellationToken)
            => await GetByDateTimeRangeAsync(startDate, endDate, permission => permission.AdditionalPermissionAssignedDate, true, fromRecord, pageSize, cancellationToken);

        #endregion

        #region Query with Navigation Properties

        public async Task<AccountAdditionalPermission?> GetNavigationByIdAsync(uint id, AccountAdditionalPermissionNavigationOptions options,
            CancellationToken cancellationToken)
        {
            var queryable = _dbSet.AsQueryable();
            queryable = ApplyNavigationOptions(queryable, options);
            return await queryable.FirstOrDefaultAsync(additional => additional.AccountAdditionalPermissionId == id, cancellationToken);
        }

        public async Task<IEnumerable<AccountAdditionalPermission>> GetNavigationByIdsAsync(IEnumerable<uint> ids, AccountAdditionalPermissionNavigationOptions options,
            uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            var queryable = _dbSet.AsQueryable();
            queryable = ApplyNavigationOptions(queryable, options);
            return await queryable
                .Where(additional => ids.Contains(additional.AccountAdditionalPermissionId) && additional.AccountAdditionalPermissionId > cursor)
                .OrderBy(additional => additional.AccountAdditionalPermissionId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<AccountAdditionalPermission> ExplicitLoadAsync(AccountAdditionalPermission entity,
            AccountAdditionalPermissionNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetAccount)
                await _context.Entry(entity).Reference(entity => entity.Account).LoadAsync(cancellationToken);
            if (options.IsGetPermission)
                await _context.Entry(entity).Reference(entity => entity.Permission).LoadAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<AccountAdditionalPermission>> ExplicitLoadAsync(IEnumerable<AccountAdditionalPermission> entities,
            AccountAdditionalPermissionNavigationOptions options, CancellationToken cancellationToken)
        {
            List<Permission> permissions = [];
            List<Account> accounts = [];
            var entityIds = entities.Select(entity => entity.AccountAdditionalPermissionId).Distinct().ToList();
            if (options.IsGetAccount)
                accounts = await _context.Accounts.Where(account => entityIds.Contains(account.AccountId)).ToListAsync(cancellationToken);
            if (options.IsGetPermission)
                permissions = await _context.Permissions.Where(permission => entityIds.Contains(permission.PermissionId)).ToListAsync(cancellationToken);
            return MappingToAccountAdditionalPermissions(entities, permissions, accounts);
        }

        #endregion

        #region Helper Methods for Mapping and Apply Navigation Options

        private static IQueryable<AccountAdditionalPermission> ApplyNavigationOptions(IQueryable<AccountAdditionalPermission> query,
            AccountAdditionalPermissionNavigationOptions options)
        {
            if (options.IsGetPermission)
                query = query.Include(additional => additional.Permission);
            if (options.IsGetAccount)
                query = query.Include(additional => additional.Account);
            return query;
        }

        private static IEnumerable<AccountAdditionalPermission> MappingToAccountAdditionalPermissions(IEnumerable<AccountAdditionalPermission> additionalPermissions,
            List<Permission> permissions, List<Account> accounts)
        {
            if (additionalPermissions == null || !additionalPermissions.Any())
                return [];
            Dictionary<uint, Permission> permissionsDict = [];
            Dictionary<uint, Account> accountsDict = [];

            if (permissions is { Count: > 0 })
                permissionsDict = permissions.ToDictionary(permission => permission.PermissionId);
            if (accounts is { Count: > 0 })
                accountsDict = accounts.ToDictionary(account => account.AccountId);

            // mapping
            foreach (var additional in additionalPermissions)
            {
                if (permissionsDict.TryGetValue(additional.AccountId, out var permission))
                    additional.Permission = permission ?? new();
                if (accountsDict.TryGetValue(additional.AccountId, out var account))
                    additional.Account = account ?? new();
            }
            return additionalPermissions;
        }
        #endregion
    }
}
