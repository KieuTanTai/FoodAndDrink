using ProjectShop.Server.Core.Entities.EntitiesRequest;

namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface IAddAccountRoleService<TEntity, TKey> where TEntity : class where TKey : struct
    {
        Task<int> AddAccountRoleAsync(TKey keys);
        Task<IEnumerable<BatchItemResult<TEntity>>> AddAccountRolesAsync(IEnumerable<TEntity> entities);
    }
}
