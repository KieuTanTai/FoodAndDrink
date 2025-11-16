using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Permission repository interface with specific query methods
    /// </summary>
    public interface IPermissionRepository : IRepository<Permission>, IBaseExplicitLoadRepository<Permission, PermissionNavigationOptions>,
        IBaseGetByCreatedAndLastUpdatedDate<Permission>
    {
        // Query by PermissionName
        Task<Permission?> GetByNameAsync(string permissionName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Permission>> GetManyByNamesAsync(IEnumerable<string> permissionNames, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<Permission>> SearchByNameContainsAsync(string searchTerm, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // Query by Status
        Task<IEnumerable<Permission>> GetByStatusAsync(bool? status, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // More Explicit Load Methods
        Task<Permission?> GetNavigationByIdAsync(uint id, bool isGetAccountAdditionalPermissions, bool isGetRolePermissions, CancellationToken cancellationToken = default);
        Task<Permission> ExplicitLoadAsync(Permission entity, bool isGetAccountAdditionalPermissions, bool isGetRolePermissions, CancellationToken cancellationToken = default);
    }
}
