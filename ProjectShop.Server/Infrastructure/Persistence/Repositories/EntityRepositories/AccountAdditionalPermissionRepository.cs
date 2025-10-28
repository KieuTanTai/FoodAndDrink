using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;
using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class AccountAdditionalPermissionRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : 
        Repository<AccountAdditionalPermission>(context, maxGetRecord), IAccountAdditionalPermissionRepository
    {
        #region  Query by foreign id

        public async Task<AccountAdditionalPermission?> GetByAccountIdAndPermissionIdAsync
            (uint accountId, uint permissionId, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(permission => permission.AccountId == accountId && permission.PermissionId == permissionId, cancellationToken);

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByAccountIdAsync(uint accountId, CancellationToken cancellationToken)
            => await _dbSet.Where(permission => permission.AccountId == accountId).ToListAsync(cancellationToken);

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByIsGrantedAsync(bool isGranted, CancellationToken cancellationToken)
            => await _dbSet.Where(permission => permission.IsGranted == isGranted).ToListAsync(cancellationToken);

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByPermissionIdAsync(uint permissionId, CancellationToken cancellationToken)
            => await _dbSet.Where(permission => permission.PermissionId == permissionId).ToListAsync(cancellationToken);

        #endregion

        #region Query by status and time
        public async Task<IEnumerable<AccountAdditionalPermission>> GetByStatusAsync(bool status, CancellationToken cancellationToken)
            => await _dbSet.Where(permission => permission.AdditionalPermissionStatus == status).ToListAsync(cancellationToken);

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
            => await GetByDateTimeRangeAsync(startDate, endDate, permission => permission.AdditionalPermissionAssignedDate, cancellationToken);

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByMonthAndYearAsync(int month, int year, ECompareType eCompareType, CancellationToken cancellationToken)
        {
            Func<AccountAdditionalPermission, bool> predicate = await GetCompareConditions(month, year, eCompareType,
                permission => permission.AdditionalPermissionAssignedDate);
            return await GetByTimeAsync(predicate, cancellationToken);
        }

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByYearAsync(int year, ECompareType eCompareType, CancellationToken cancellationToken)
        {
            Func<AccountAdditionalPermission, bool> predicate = await GetCompareConditions(year, eCompareType,
                permission => permission.AdditionalPermissionAssignedDate);
            return await GetByTimeAsync(predicate, cancellationToken);
        }

        #endregion

        #region Query with Navigation Properties

        public async Task<AccountAdditionalPermission?> GetNavigationByIdAsync(uint id, AccountAdditionalPermissionNavigationOptions options,
            CancellationToken cancellationToken)
        {
            IQueryable<AccountAdditionalPermission> queryable = _dbSet.AsQueryable();
            queryable = ApplyNavigationOptions(queryable, options);
            return await queryable.FirstOrDefaultAsync(additional => additional.AccountAdditionalPermissionId == id, cancellationToken);
        }

        public async Task<IEnumerable<AccountAdditionalPermission>> GetNavigationByIdsAsync(IEnumerable<uint> ids, AccountAdditionalPermissionNavigationOptions options,
            CancellationToken cancellationToken)
        {
            IQueryable<AccountAdditionalPermission> queryable = _dbSet.AsQueryable();
            queryable = ApplyNavigationOptions(queryable, options);
            return await queryable
                .Where(additional => ids.Contains(additional.AccountAdditionalPermissionId))
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
            var entityIds = entities.Select(entity => entity.AccountAdditionalPermissionId).ToList();
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

            if (permissions != null && permissions.Count > 0)
                permissionsDict = permissions.ToDictionary(permission => permission.PermissionId);
            if (accounts != null && accounts.Count > 0)
                accountsDict = accounts.ToDictionary(account => account.AccountId);

            // mapping
            foreach (AccountAdditionalPermission additional in additionalPermissions)
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
