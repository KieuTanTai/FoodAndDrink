using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IBankRepository : IRepository<Bank>
    {
        Task<Bank?> GetByNameAsync(string bankName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Bank>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);
        Task<IEnumerable<Bank>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);
    }
}
