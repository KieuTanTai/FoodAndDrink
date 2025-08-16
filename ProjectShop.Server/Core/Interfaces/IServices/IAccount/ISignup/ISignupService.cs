namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount.ISignup
{
    public interface ISignupService<T> where T : class
    {
        Task<int> AddAccountAsync(T entity);
        Task<int> AddAccountsAsync(IEnumerable<T> entities);
    }
}
