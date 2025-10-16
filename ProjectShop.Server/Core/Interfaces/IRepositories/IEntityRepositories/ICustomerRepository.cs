using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Customer repository interface with specific query methods
    /// </summary>
    public interface ICustomerRepository : IRepository<Customer>
    {
        // Query by PersonId
        Task<Customer?> GetByPersonIdAsync(uint personId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> GetByPersonIdsAsync(IEnumerable<uint> personIds, CancellationToken cancellationToken = default);

        // Query by LoyaltyPoints
        Task<IEnumerable<Customer>> GetByLoyaltyPointsRangeAsync(decimal minPoints, decimal maxPoints, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> GetTopByLoyaltyPointsAsync(int topCount, CancellationToken cancellationToken = default);

        // Query by RegistrationDate
        Task<IEnumerable<Customer>> GetByRegistrationDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> GetByRegistrationYearAsync(int year, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> GetByRegistrationMonthAndYearAsync(int year, int month, CancellationToken cancellationToken = default);

        // Query with navigation properties
        Task<Customer?> GetByIdWithNavigationAsync(uint customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> GetAllWithNavigationAsync(CancellationToken cancellationToken = default);
    }
}
