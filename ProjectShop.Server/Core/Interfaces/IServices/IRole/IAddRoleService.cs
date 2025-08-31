using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface IAddRoleService<TEntity> where TEntity : class
    {
        Task<ServiceResult<TEntity>> AddRoleAsync(TEntity role);
        Task<ServiceResults<TEntity>> AddRolesAsync(IEnumerable<TEntity> roles);
    }
}
