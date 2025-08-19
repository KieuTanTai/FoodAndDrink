namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface ISignupService<TEntity> where TEntity : class
    {
        Task<int> AddAccountAsync(TEntity entity);
        Task<int> AddAccountsAsync(IEnumerable<TEntity> entities);
    }
}
