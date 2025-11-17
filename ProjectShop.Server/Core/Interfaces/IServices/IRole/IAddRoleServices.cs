using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.Results.ServiceResult;

namespace ProjectShop.Server.Core.Interfaces.IServices.IRole
{
    public interface IAddRoleServices<TEntity> where TEntity : class
    {
        Task<ServiceResult<TEntity>> AddRoleAsync(TEntity role, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> AddRolesAsync(IEnumerable<TEntity> roles, CancellationToken cancellationToken = default);
    }
}
