using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface ISignupService<TEntity> where TEntity : class
    {
        Task<int> AddAccountAsync(TEntity entity);
        Task<IEnumerable<BatchItemResult<TEntity>>> AddAccountsAsync(IEnumerable<TEntity> entities);
    }
}
