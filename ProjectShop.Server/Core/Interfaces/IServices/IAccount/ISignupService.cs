using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface ISignupService<TEntity> where TEntity : class
    {
        Task<ServiceResult<TEntity>> AddAccountAsync(TEntity entity);
        Task<ServiceResults<TEntity>> AddAccountsAsync(IEnumerable<TEntity> entities);
    }
}
