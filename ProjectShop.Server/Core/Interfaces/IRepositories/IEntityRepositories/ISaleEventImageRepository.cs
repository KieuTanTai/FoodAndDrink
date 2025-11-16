using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface ISaleEventImageRepository : IRepository<SaleEventImage>
    {
        Task<IEnumerable<SaleEventImage>> GetBySaleEventIdAsync(uint saleEventId, CancellationToken cancellationToken = default);
        Task<IEnumerable<SaleEventImage>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<SaleEventImage>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
