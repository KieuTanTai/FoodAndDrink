using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Account repository interface with specific query methods
    /// Extends base IRepository with Account-specific operations
    /// </summary>
    public interface IAccountRepository : IRepository<Account>
    {
        // Query by UserName
        Task<Account?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
        Task<Account?> GetByUserNameAndPasswordAsync(string userName, string password, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetByUserNamesAsync(IEnumerable<string> userNames, CancellationToken cancellationToken = default);

        // Query by AccountStatus
        Task<IEnumerable<Account>> GetByStatusAsync(bool status, CancellationToken cancellationToken = default);

        // Query by AccountCreatedDate
        Task<IEnumerable<Account>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetByCreatedYearAsync(int year, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetByCreatedMonthAndYearAsync(int year, int month, CancellationToken cancellationToken = default);

        // Query by AccountLastUpdatedDate
        Task<IEnumerable<Account>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetByLastUpdatedYearAsync(int year, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetByLastUpdatedMonthAndYearAsync(int year, int month, CancellationToken cancellationToken = default);

        // Query with navigation properties
        Task<Account?> GetByIdWithNavigationAsync(uint accountId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetAllWithNavigationAsync(CancellationToken cancellationToken = default);
    }
}
