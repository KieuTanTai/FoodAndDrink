using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class RolePermissionRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule, IDefaultPageSizeRule defaultPageSizeRule)
        : Repository<RolePermission>(context, maxReturnRecordsRule, defaultPageSizeRule), IRolePermissionRepository
    {
        #region Query by RoleId and PermissionId

        public async Task<RolePermission?> GetByRoleIdAndPermissionIdAsync(uint roleId, uint permissionId, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId, cancellationToken);

        public async Task<IEnumerable<RolePermission>> GetByRoleIdAsync(uint roleId, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(rp => rp.RoleId == roleId && rp.RolePermissionId > cursor)
                .OrderBy(rp => rp.RolePermissionId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<RolePermission>> GetByPermissionIdAsync(uint permissionId, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(rp => rp.PermissionId == permissionId && rp.RolePermissionId > cursor)
                .OrderBy(rp => rp.RolePermissionId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by RolePermissionCreatedDate

        public Task<IEnumerable<RolePermission>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
            => GetByDateTimeRangeAsync(startDate, endDate, rp => rp.RolePermissionCreatedDate, true, 0, null, cancellationToken);

        #endregion

        #region Query with Navigation Properties

        public async Task<RolePermission?> GetNavigationByIdAsync(uint id, RolePermissionNavigationOptions options, CancellationToken cancellationToken)
        {
            IQueryable<RolePermission> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query.FirstOrDefaultAsync(rp => rp.RolePermissionId == id, cancellationToken);
        }

        public async Task<IEnumerable<RolePermission>> GetNavigationByIdsAsync(IEnumerable<uint> ids, RolePermissionNavigationOptions options, uint? fromRecord,
            uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            IQueryable<RolePermission> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query
                .Where(rp => ids.Contains(rp.RolePermissionId) && rp.RolePermissionId > cursor)
                .OrderBy(rp => rp.RolePermissionId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<RolePermission> ExplicitLoadAsync(RolePermission entity, RolePermissionNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetRole)
                await _context.Entry(entity).Reference(rp => rp.Role).LoadAsync(cancellationToken);
            if (options.IsGetPermission)
                await _context.Entry(entity).Reference(rp => rp.Permission).LoadAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<RolePermission>> ExplicitLoadAsync(IEnumerable<RolePermission> entities, RolePermissionNavigationOptions options, CancellationToken cancellationToken)
        {
            List<Role> roles = [];
            List<Permission> permissions = [];
            var roleIds = entities.Select(rp => rp.RoleId).Distinct().ToList();
            var permissionIds = entities.Select(rp => rp.PermissionId).Distinct().ToList();

            if (options.IsGetRole)
                roles = await _context.Roles.Where(role => roleIds.Contains(role.RoleId)).ToListAsync(cancellationToken);
            if (options.IsGetPermission)
                permissions = await _context.Permissions.Where(permission => permissionIds.Contains(permission.PermissionId)).ToListAsync(cancellationToken);

            return MappingToRolePermissions(entities, roles, permissions);
        }

        #endregion

        #region Helper Methods for Mapping and Apply Navigation Options

        private static IQueryable<RolePermission> ApplyNavigationOptions(IQueryable<RolePermission> query, RolePermissionNavigationOptions options)
        {
            if (options.IsGetRole)
                query = query.Include(rp => rp.Role);
            if (options.IsGetPermission)
                query = query.Include(rp => rp.Permission);

            return query;
        }

        private static IEnumerable<RolePermission> MappingToRolePermissions(IEnumerable<RolePermission> rolePermissions,
            List<Role> roles, List<Permission> permissions)
        {
            if (rolePermissions == null || !rolePermissions.Any())
                return [];

            Dictionary<uint, Role> rolesDict = [];
            Dictionary<uint, Permission> permissionsDict = [];

            if (roles != null && roles.Count > 0)
                rolesDict = roles.ToDictionary(role => role.RoleId);
            if (permissions != null && permissions.Count > 0)
                permissionsDict = permissions.ToDictionary(permission => permission.PermissionId);

            foreach (RolePermission rp in rolePermissions)
            {
                if (rolesDict.TryGetValue(rp.RoleId, out var role))
                    rp.Role = role ?? new();
                if (permissionsDict.TryGetValue(rp.PermissionId, out var permission))
                    rp.Permission = permission ?? new();
            }

            return rolePermissions;
        }

        #endregion
    }
}
