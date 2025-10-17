using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class AccountAdditionalPermissionRepository(IDBContext context) : Repository<AccountAdditionalPermission>(context), IAccountAdditionalPermissionRepository
    {
        public async Task<AccountAdditionalPermission?> GetByAccountIdAndPermissionIdAsync(uint accountId, uint permissionId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByAccountIdAsync(uint accountId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByIsGrantedAsync(bool? isGranted, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByPermissionIdAsync(uint permissionId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AccountAdditionalPermission>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
