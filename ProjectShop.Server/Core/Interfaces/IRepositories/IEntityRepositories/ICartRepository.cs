using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<IEnumerable<Cart>> GetByCustomerIdAsync(uint customerId, CancellationToken cancellationToken = default);
        Task<Cart?> GetActiveCartByCustomerIdAsync(uint customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Cart>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<Cart?> GetByIdWithDetailsAsync(uint cartId, CancellationToken cancellationToken = default);
    }
}
