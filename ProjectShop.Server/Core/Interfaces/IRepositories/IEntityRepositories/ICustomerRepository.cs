using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Customer repository interface with specific query methods
    /// </summary>
    public interface ICustomerRepository : IRepository<Customer>, IBaseExplicitLoadRepository<Customer, uint, CustomerNavigationOptions>
    {
        // Query by PersonId
        Task<Customer?> GetByPersonIdAsync(uint personId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> GetByPersonIdsAsync(IEnumerable<uint> personIds, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // Query by LoyaltyPoints
        Task<IEnumerable<Customer>> GetByLoyaltyPointsRangeAsync(decimal minPoints, decimal maxPoints, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> GetTopByLoyaltyPointsAsync(int topCount, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> GetByRegistrationDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord = 0, uint? pageSize = null,
            CancellationToken cancellationToken = default);
    }
}
