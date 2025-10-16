using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IAccountAdditionalPermissionRepository : IRepository<AccountAdditionalPermission>
    {
        Task<IEnumerable<AccountAdditionalPermission>> GetByAccountIdAsync(uint accountId, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountAdditionalPermission>> GetByPermissionIdAsync(uint permissionId, CancellationToken cancellationToken = default);
        Task<AccountAdditionalPermission?> GetByAccountIdAndPermissionIdAsync(uint accountId, uint permissionId, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountAdditionalPermission>> GetByIsGrantedAsync(bool? isGranted, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountAdditionalPermission>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);
    }
}
