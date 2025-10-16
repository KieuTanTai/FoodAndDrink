using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Role repository interface with specific query methods
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        // Query by RoleName
        Task<Role?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Role>> GetByNamesAsync(IEnumerable<string> roleNames, CancellationToken cancellationToken = default);
        Task<IEnumerable<Role>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);

        // Query by Status
        Task<IEnumerable<Role>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);

        // Query by dates
        Task<IEnumerable<Role>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<Role>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

        // Query with navigation properties
        Task<Role?> GetByIdWithPermissionsAsync(uint roleId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Role>> GetAllWithPermissionsAsync(CancellationToken cancellationToken = default);
    }
}
