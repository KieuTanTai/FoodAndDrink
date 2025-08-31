using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface IAddAccountRoleService<TEntity, TKey> where TEntity : class where TKey : struct
    {
        Task<ServiceResult<TEntity>> AddAccountRoleAsync(TKey keys);
        Task<ServiceResults<TEntity>> AddAccountRolesAsync(IEnumerable<TEntity> entities);
    }
}
