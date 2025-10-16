using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IUserPaymentMethodRepository : IRepository<UserPaymentMethod>
    {
        Task<IEnumerable<UserPaymentMethod>> GetByAccountIdAsync(uint accountId, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserPaymentMethod>> GetByBankIdAsync(uint bankId, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserPaymentMethod>> GetByAccountIdAndBankIdAsync(uint accountId, uint bankId, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserPaymentMethod>> GetByAddedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserPaymentMethod>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserPaymentMethod>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);
    }
}
