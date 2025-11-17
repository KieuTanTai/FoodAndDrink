using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class RoleRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule, IDefaultPageSizeRule defaultPageSizeRule)
        : Repository<Role>(context, maxReturnRecordsRule, defaultPageSizeRule), IRoleRepository
    {
        #region Query by RoleName

        public async Task<Role?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(role => role.RoleName == roleName, cancellationToken);

        public async Task<IEnumerable<Role>> GetManyByNamesAsync(IEnumerable<string> roleNames, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;
            var namesList = roleNames.ToList();

            return await _dbSet
                .Where(role => namesList.Contains(role.RoleName) && role.RoleId > cursor)
                .OrderBy(role => role.RoleId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Role>> SearchByNameContainsAsync(string searchTerm, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(role => role.RoleName.Contains(searchTerm) && role.RoleId > cursor)
                .OrderBy(role => role.RoleId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by Status

        public async Task<IEnumerable<Role>> GetByStatusAsync(bool? status, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(role => role.RoleStatus == status && role.RoleId > cursor)
                .OrderBy(role => role.RoleId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by RoleCreatedDate

        public Task<IEnumerable<Role>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
            => GetByDateTimeRangeAsync(startDate, endDate, role => role.RoleCreatedDate, true, 0, null, cancellationToken);

        #endregion

        #region Query with Navigation Properties

        public async Task<Role?> GetNavigationByIdAsync(uint id, RoleNavigationOptions options, CancellationToken cancellationToken)
        {
            var query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query.FirstOrDefaultAsync(role => role.RoleId == id, cancellationToken);
        }

        public async Task<IEnumerable<Role>> GetNavigationByIdsAsync(IEnumerable<uint> ids, RoleNavigationOptions options, uint? fromRecord,
            uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            var query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query
                .Where(role => ids.Contains(role.RoleId) && role.RoleId > cursor)
                .OrderBy(role => role.RoleId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Role> ExplicitLoadAsync(Role entity, RoleNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetAccountRoles)
                await _context.Entry(entity).Collection(role => role.AccountRoles).LoadAsync(cancellationToken);
            if (options.IsGetRolePermissions)
                await _context.Entry(entity).Collection(role => role.RolePermissions).LoadAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<Role>> ExplicitLoadAsync(IEnumerable<Role> entities, RoleNavigationOptions options, CancellationToken cancellationToken)
        {
            var roleIds = entities.Select(role => role.RoleId).Distinct().ToList();
            ILookup<uint, AccountRole>? accountRolesLookup = null;
            ILookup<uint, RolePermission>? rolePermissionsLookup = null;

            if (options.IsGetAccountRoles)
            {
                var accountRoles = await _context.AccountRoles
                    .Where(ar => roleIds.Contains(ar.RoleId))
                    .ToListAsync(cancellationToken);
                accountRolesLookup = accountRoles.ToLookup(ar => ar.RoleId);
            }

            if (options.IsGetRolePermissions)
            {
                var rolePermissions = await _context.RolePermissions
                    .Where(rp => roleIds.Contains(rp.RoleId))
                    .ToListAsync(cancellationToken);
                rolePermissionsLookup = rolePermissions.ToLookup(rp => rp.RoleId);
            }

            return MappingToRoles(entities, accountRolesLookup, rolePermissionsLookup);
        }

        #endregion

        #region Helper Methods for Mapping and Apply Navigation Options

        private static IQueryable<Role> ApplyNavigationOptions(IQueryable<Role> query, RoleNavigationOptions options)
        {
            if (options.IsGetAccountRoles)
                query = query.Include(role => role.AccountRoles);
            if (options.IsGetRolePermissions)
                query = query.Include(role => role.RolePermissions);

            return query;
        }

        private static IEnumerable<Role> MappingToRoles(IEnumerable<Role> roles,
            ILookup<uint, AccountRole>? accountRolesLookup, ILookup<uint, RolePermission>? rolePermissionsLookup)
        {
            if (roles == null || !roles.Any())
                return [];

            foreach (var role in roles)
            {
                if (accountRolesLookup != null)
                    role.AccountRoles = accountRolesLookup[role.RoleId].ToList();
                if (rolePermissionsLookup != null)
                    role.RolePermissions = rolePermissionsLookup[role.RoleId].ToList();
            }

            return roles;
        }

        #endregion
    }
}
