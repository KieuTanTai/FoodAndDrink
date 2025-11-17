using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.Results.ServiceResult;

namespace ProjectShop.Server.Core.Interfaces.IServices.IRole
{
    public interface IAddAccountRoleServices<TEntity, TKey> where TEntity : class where TKey : struct
    {
        Task<ServiceResult<TEntity>> AddAccountRoleAsync(TKey keys, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> AddAccountRolesAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    }
}
