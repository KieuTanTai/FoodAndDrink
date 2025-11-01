using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IAccountRoleRepository : IRepository<AccountRole>,
        IBaseGetByDateTime<AccountRole>,
        IBaseExplicitLoadRepository<AccountRole, AccountRoleNavigationOptions>
    {
        Task<IEnumerable<AccountRole>> GetByAccountIdAsync(uint accountId, uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountRole>> GetByRoleIdAsync(uint roleId, uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default);
        Task<AccountRole?> GetByAccountIdAndRoleIdAsync(uint accountId, uint roleId, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountRole>> GetByStatusAsync(bool? status, uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default);
    }
}
