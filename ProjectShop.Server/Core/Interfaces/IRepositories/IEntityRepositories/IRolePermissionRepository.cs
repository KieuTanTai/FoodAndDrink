using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IRolePermissionRepository : IRepository<RolePermission>, IBaseExplicitLoadRepository<RolePermission, uint, RolePermissionNavigationOptions>
    {
        // Query by RoleId
        Task<IEnumerable<RolePermission>> GetByRoleIdAsync(uint roleId, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // Query by PermissionId
        Task<IEnumerable<RolePermission>> GetByPermissionIdAsync(uint permissionId, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // Query by RoleId and PermissionId
        Task<RolePermission?> GetByRoleIdAndPermissionIdAsync(uint roleId, uint permissionId, CancellationToken cancellationToken = default);
        Task<IEnumerable<RolePermission>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
