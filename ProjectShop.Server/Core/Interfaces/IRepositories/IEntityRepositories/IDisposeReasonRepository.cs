using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IDisposeReasonRepository : IRepository<DisposeReason>
    {
        Task<DisposeReason?> GetByNameAsync(string reasonName, CancellationToken cancellationToken = default);
        Task<IEnumerable<DisposeReason>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);
    }
}
