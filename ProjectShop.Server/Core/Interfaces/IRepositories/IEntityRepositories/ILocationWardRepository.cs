using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface ILocationWardRepository : IRepository<LocationWard>
    {
        Task<IEnumerable<LocationWard>> GetByLocationDistrictIdAsync(uint locationDistrictId, CancellationToken cancellationToken = default);
        Task<LocationWard?> GetByNameAsync(string wardName, CancellationToken cancellationToken = default);
        Task<IEnumerable<LocationWard>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);
    }
}
