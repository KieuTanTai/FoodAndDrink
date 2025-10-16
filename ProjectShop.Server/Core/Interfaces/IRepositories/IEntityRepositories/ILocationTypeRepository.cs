using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface ILocationTypeRepository : IRepository<LocationType>
    {
        Task<LocationType?> GetByTypeNameAsync(string typeName, CancellationToken cancellationToken = default);
        Task<IEnumerable<LocationType>> SearchByTypeNameAsync(string searchTerm, CancellationToken cancellationToken = default);
    }
}
