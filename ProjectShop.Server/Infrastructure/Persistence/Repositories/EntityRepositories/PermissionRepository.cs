using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class PermissionRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule, IDefaultPageSizeRule defaultPageSizeRule)
        : Repository<Permission>(context, maxReturnRecordsRule, defaultPageSizeRule), IPermissionRepository
    {
        #region Query by PermissionName

        public async Task<Permission?> GetByNameAsync(string permissionName, CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(permission => permission.PermissionName == permissionName, cancellationToken);

        public async Task<IEnumerable<Permission>> GetManyByNamesAsync(IEnumerable<string> permissionNames, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;
            var namesList = permissionNames.ToList();

            return await _dbSet
                .Where(permission => namesList.Contains(permission.PermissionName) && permission.PermissionId > cursor)
                .OrderBy(permission => permission.PermissionId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Permission>> SearchByNameContainsAsync(string searchTerm, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(permission => permission.PermissionName.Contains(searchTerm) && permission.PermissionId > cursor)
                .OrderBy(permission => permission.PermissionId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by Status

        public async Task<IEnumerable<Permission>> GetByStatusAsync(bool? status, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(permission => permission.PermissionStatus == status && permission.PermissionId > cursor)
                .OrderBy(permission => permission.PermissionId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by PermissionCreatedDate and PermissionLastUpdatedDate

        public Task<IEnumerable<Permission>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
            => GetByDateTimeRangeAsync(startDate, endDate, permission => permission.PermissionCreatedDate, true, fromRecord, pageSize, cancellationToken);

        public Task<IEnumerable<Permission>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
            => GetByDateTimeRangeAsync(startDate, endDate, permission => permission.PermissionLastUpdatedDate, true, fromRecord, pageSize, cancellationToken);

        #endregion

        #region Query with Navigation Properties

        public async Task<Permission?> GetNavigationByIdAsync(uint id, PermissionNavigationOptions options, CancellationToken cancellationToken)
        {
            IQueryable<Permission> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query.FirstOrDefaultAsync(permission => permission.PermissionId == id, cancellationToken);
        }

        public async Task<Permission?> GetNavigationByIdAsync(uint id, bool isGetAccountAdditionalPermissions, bool isGetRolePermissions, CancellationToken cancellationToken)
        {
            var options = new PermissionNavigationOptions
            {
                IsGetAccountAdditionalPermissions = isGetAccountAdditionalPermissions,
                IsGetRolePermissions = isGetRolePermissions
            };
            return await GetNavigationByIdAsync(id, options, cancellationToken);
        }

        public async Task<IEnumerable<Permission>> GetNavigationByIdsAsync(IEnumerable<uint> ids, PermissionNavigationOptions options, uint? fromRecord,
            uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            IQueryable<Permission> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query
                .Where(permission => ids.Contains(permission.PermissionId) && permission.PermissionId > cursor)
                .OrderBy(permission => permission.PermissionId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Permission> ExplicitLoadAsync(Permission entity, PermissionNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetAccountAdditionalPermissions)
                await _context.Entry(entity).Collection(permission => permission.AccountAdditionalPermissions).LoadAsync(cancellationToken);
            if (options.IsGetRolePermissions)
                await _context.Entry(entity).Collection(permission => permission.RolePermissions).LoadAsync(cancellationToken);
            return entity;
        }

        public async Task<Permission> ExplicitLoadAsync(Permission entity, bool isGetAccountAdditionalPermissions, bool isGetRolePermissions, CancellationToken cancellationToken)
        {
            var options = new PermissionNavigationOptions
            {
                IsGetAccountAdditionalPermissions = isGetAccountAdditionalPermissions,
                IsGetRolePermissions = isGetRolePermissions
            };
            return await ExplicitLoadAsync(entity, options, cancellationToken);
        }

        public async Task<IEnumerable<Permission>> ExplicitLoadAsync(IEnumerable<Permission> entities, PermissionNavigationOptions options, CancellationToken cancellationToken)
        {
            var permissionIds = entities.Select(permission => permission.PermissionId).Distinct().ToList();
            ILookup<uint, AccountAdditionalPermission>? accountAdditionalPermissionsLookup = null;
            ILookup<uint, RolePermission>? rolePermissionsLookup = null;

            if (options.IsGetAccountAdditionalPermissions)
            {
                var accountAdditionalPermissions = await _context.AccountAdditionalPermissions
                    .Where(aap => permissionIds.Contains(aap.PermissionId))
                    .ToListAsync(cancellationToken);
                accountAdditionalPermissionsLookup = accountAdditionalPermissions.ToLookup(aap => aap.PermissionId);
            }

            if (options.IsGetRolePermissions)
            {
                var rolePermissions = await _context.RolePermissions
                    .Where(rp => permissionIds.Contains(rp.PermissionId))
                    .ToListAsync(cancellationToken);
                rolePermissionsLookup = rolePermissions.ToLookup(rp => rp.PermissionId);
            }

            return MappingToPermissions(entities, accountAdditionalPermissionsLookup, rolePermissionsLookup);
        }

        #endregion

        #region Helper Methods for Mapping and Apply Navigation Options

        private static IQueryable<Permission> ApplyNavigationOptions(IQueryable<Permission> query, PermissionNavigationOptions options)
        {
            if (options.IsGetAccountAdditionalPermissions)
                query = query.Include(permission => permission.AccountAdditionalPermissions);
            if (options.IsGetRolePermissions)
                query = query.Include(permission => permission.RolePermissions);

            return query;
        }

        private static IEnumerable<Permission> MappingToPermissions(IEnumerable<Permission> permissions,
            ILookup<uint, AccountAdditionalPermission>? accountAdditionalPermissionsLookup, ILookup<uint, RolePermission>? rolePermissionsLookup)
        {
            if (permissions == null || !permissions.Any())
                return [];

            foreach (Permission permission in permissions)
            {
                if (accountAdditionalPermissionsLookup != null)
                    permission.AccountAdditionalPermissions = accountAdditionalPermissionsLookup[permission.PermissionId].ToList();
                if (rolePermissionsLookup != null)
                    permission.RolePermissions = rolePermissionsLookup[permission.PermissionId].ToList();
            }

            return permissions;
        }

        #endregion
    }
}
