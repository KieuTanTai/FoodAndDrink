using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class RolePermissionRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<RolePermission>(context, maxGetRecord), IRolePermissionRepository
    {
        public async Task<IEnumerable<RolePermission>> GetByRoleIdAsync(uint roleId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RolePermission>> GetByPermissionIdAsync(uint permissionId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<RolePermission?> GetByRoleIdAndPermissionIdAsync(uint roleId, uint permissionId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RolePermission>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
