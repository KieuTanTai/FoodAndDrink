using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Role repository interface with specific query methods
    /// </summary>
    public interface IRoleRepository : IRepository<Role>, IBaseExplicitLoadRepository<Role, uint, RoleNavigationOptions>
    {
        // Query by RoleName
        Task<Role?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Role>> GetManyByNamesAsync(IEnumerable<string> roleNames, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<Role>> SearchByNameContainsAsync(string searchTerm, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // Query by Status
        Task<IEnumerable<Role>> GetByStatusAsync(bool? status, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // Query by created date
        Task<IEnumerable<Role>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
