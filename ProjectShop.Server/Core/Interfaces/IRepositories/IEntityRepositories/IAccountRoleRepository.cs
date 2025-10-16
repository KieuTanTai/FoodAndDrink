using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IAccountRoleRepository : IRepository<AccountRole>
    {
        Task<IEnumerable<AccountRole>> GetByAccountIdAsync(uint accountId, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountRole>> GetByRoleIdAsync(uint roleId, CancellationToken cancellationToken = default);
        Task<AccountRole?> GetByAccountIdAndRoleIdAsync(uint accountId, uint roleId, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountRole>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountRole>> GetByAssignedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
