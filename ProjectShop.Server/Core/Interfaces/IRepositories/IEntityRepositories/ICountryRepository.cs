using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<Country?> GetByNameAsync(string countryName, CancellationToken cancellationToken = default);
        Task<Country?> GetByCodeAsync(string countryCode, CancellationToken cancellationToken = default);
        Task<IEnumerable<Country>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);
        Task<IEnumerable<Country>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);
    }
}
