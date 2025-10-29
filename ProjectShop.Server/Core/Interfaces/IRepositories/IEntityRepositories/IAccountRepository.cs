using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IAccountRepository : IRepository<Account>, IBaseExplicitLoadRepository<Account, AccountNavigationOptions>,
        IBaseGetByCreatedAndLastUpdatedDate<Account>
    {
        // Query by UserName
        Task<Account?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
        Task<Account?> GetByUserNameAndPasswordAsync(string userName, string password, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetByUserNamesAsync(IEnumerable<string> userNames, CancellationToken cancellationToken = default);

        // Query by AccountStatus
        Task<IEnumerable<Account>> GetByStatusAsync(bool status, CancellationToken cancellationToken = default);

        // More Explicit Load Methods
        Task<Account?> GetNavigationByIdAsync(uint id, bool isGetAuth, bool isGetPermission, CancellationToken cancellationToken = default);
        Task<Account> ExplicitLoadAsync(Account entity, bool isGetAuth, bool isGetPermission, CancellationToken cancellationToken = default);
    }
}
