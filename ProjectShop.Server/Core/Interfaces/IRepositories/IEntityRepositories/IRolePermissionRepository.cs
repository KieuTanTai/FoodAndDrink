using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IRolePermissionRepository : IRepository<RolePermission>
    {
        Task<IEnumerable<RolePermission>> GetByRoleIdAsync(uint roleId, CancellationToken cancellationToken = default);
        Task<IEnumerable<RolePermission>> GetByPermissionIdAsync(uint permissionId, CancellationToken cancellationToken = default);
        Task<RolePermission?> GetByRoleIdAndPermissionIdAsync(uint roleId, uint permissionId, CancellationToken cancellationToken = default);
        Task<IEnumerable<RolePermission>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
