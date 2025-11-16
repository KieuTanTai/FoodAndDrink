using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface ILocationCityRepository : IRepository<LocationCity>
    {
        Task<LocationCity?> GetByNameAsync(string cityName, CancellationToken cancellationToken = default);
        Task<IEnumerable<LocationCity>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);
        Task<LocationCity?> GetByIdWithDistrictsAsync(uint cityId, CancellationToken cancellationToken = default);
    }
}
