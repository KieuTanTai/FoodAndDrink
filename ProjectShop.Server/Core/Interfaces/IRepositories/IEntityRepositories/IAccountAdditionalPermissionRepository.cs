using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IAccountAdditionalPermissionRepository : IRepository<AccountAdditionalPermission>,
        IBaseGetByDateTime<AccountAdditionalPermission>,
        IBaseExplicitLoadRepository<AccountAdditionalPermission, AccountAdditionalPermissionNavigationOptions>
    {
        Task<IEnumerable<AccountAdditionalPermission>> GetByAccountIdAsync(uint accountId, uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountAdditionalPermission>> GetByPermissionIdAsync(uint permissionId, uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default);
        Task<AccountAdditionalPermission?> GetByAccountIdAndPermissionIdAsync(uint accountId, uint permissionId, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountAdditionalPermission>> GetByIsGrantedAsync(bool isGranted = true, uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountAdditionalPermission>> GetByStatusAsync(bool status = true, uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default);
    }
}
