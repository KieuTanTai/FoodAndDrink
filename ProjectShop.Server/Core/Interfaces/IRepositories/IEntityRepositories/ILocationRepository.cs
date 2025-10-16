using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<IEnumerable<Location>> GetByLocationTypeIdAsync(uint locationTypeId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Location>> GetByLocationCityIdAsync(uint locationCityId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Location>> GetByLocationDistrictIdAsync(uint locationDistrictId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Location>> GetByLocationWardIdAsync(uint locationWardId, CancellationToken cancellationToken = default);
        Task<Location?> GetByIdWithFullAddressAsync(uint locationId, CancellationToken cancellationToken = default);
    }
}
