using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface IAddRoleService<TEntity> where TEntity : class
    {
        Task<int> AddRoleAsync(TEntity role);
        Task<IEnumerable<BatchItemResult<TEntity>>> AddRolesAsync(IEnumerable<TEntity> roles);
    }
}
