using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface ILocationDistrictRepository : IRepository<LocationDistrict>
    {
        Task<IEnumerable<LocationDistrict>> GetByLocationCityIdAsync(uint locationCityId, CancellationToken cancellationToken = default);
        Task<LocationDistrict?> GetByNameAsync(string districtName, CancellationToken cancellationToken = default);
        Task<IEnumerable<LocationDistrict>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);
        Task<LocationDistrict?> GetByIdWithWardsAsync(uint districtId, CancellationToken cancellationToken = default);
    }
}
