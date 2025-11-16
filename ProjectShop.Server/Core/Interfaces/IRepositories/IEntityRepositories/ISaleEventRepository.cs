using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface ISaleEventRepository : IRepository<SaleEvent>
    {
        Task<SaleEvent?> GetByNameAsync(string saleEventName, CancellationToken cancellationToken = default);
        Task<IEnumerable<SaleEvent>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);
        Task<IEnumerable<SaleEvent>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);
        Task<IEnumerable<SaleEvent>> GetByStartDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<SaleEvent>> GetByEndDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<SaleEvent>> GetActiveSaleEventsAsync(CancellationToken cancellationToken = default);
        Task<SaleEvent?> GetByIdWithDetailsAsync(uint saleEventId, CancellationToken cancellationToken = default);
    }
}
