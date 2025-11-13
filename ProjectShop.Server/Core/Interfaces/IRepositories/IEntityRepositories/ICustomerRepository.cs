using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Customer repository interface with specific query methods
    /// </summary>
    public interface ICustomerRepository : IRepository<Customer>, IBaseExplicitLoadRepository<Customer, CustomerNavigationOptions>,
        IBaseGetByDateTime<Customer>
    {
        // Query by PersonId
        Task<Customer?> GetByPersonIdAsync(uint personId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> GetByPersonIdsAsync(IEnumerable<uint> personIds, CancellationToken cancellationToken = default);

        // Query by LoyaltyPoints
        Task<IEnumerable<Customer>> GetByLoyaltyPointsRangeAsync(decimal minPoints, decimal maxPoints, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> GetTopByLoyaltyPointsAsync(int topCount, CancellationToken cancellationToken = default);

        // Query with navigation properties
        Task<Customer?> GetByIdWithNavigationAsync(uint customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> GetAllWithNavigationAsync(CancellationToken cancellationToken = default);
    }
}
