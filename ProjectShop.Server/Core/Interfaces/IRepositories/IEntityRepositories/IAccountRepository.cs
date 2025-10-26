using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
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
        Task<IEnumerable<Account>> GetByCreatedYearAsync(int year, ECompareType eCompareType, 
            CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetByCreatedMonthAndYearAsync(int year, int month, ECompareType eCompareType, 
            CancellationToken cancellationToken = default);

        // Query by AccountLastUpdatedDate
        Task<IEnumerable<Account>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetByLastUpdatedYearAsync(int year, ECompareType eCompareType, 
            CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetByLastUpdatedMonthAndYearAsync(int year, int month, ECompareType eCompareType, 
            CancellationToken cancellationToken = default);

        // Query with navigation properties
        Task<Account?> GetNavigationByIdAsync(uint accountId, AccountNavigationOptions options, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetNavigationByIdsAsync(IEnumerable<uint> accountIds, AccountNavigationOptions options, CancellationToken cancellationToken = default);
        Task<Account> ExplicitLoadAsync(Account account, AccountNavigationOptions options, CancellationToken cancellationToken = default);

        Task<IEnumerable<Account>> ExplicitLoadAsync(IEnumerable<Account> accounts, AccountNavigationOptions options, CancellationToken cancellationToken = default);
    }
}
