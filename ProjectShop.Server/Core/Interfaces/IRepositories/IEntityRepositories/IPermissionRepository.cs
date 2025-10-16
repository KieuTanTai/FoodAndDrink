using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Permission repository interface with specific query methods
    /// </summary>
    public interface IPermissionRepository : IRepository<Permission>
    {
        // Query by PermissionName
        Task<Permission?> GetByNameAsync(string permissionName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Permission>> GetByNamesAsync(IEnumerable<string> permissionNames, CancellationToken cancellationToken = default);
        Task<IEnumerable<Permission>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);

        // Query by Status
        Task<IEnumerable<Permission>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);

        // Query by dates
        Task<IEnumerable<Permission>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<Permission>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

        // Query with navigation properties
        Task<Permission?> GetByIdWithRolesAsync(uint permissionId, CancellationToken cancellationToken = default);
    }
}
