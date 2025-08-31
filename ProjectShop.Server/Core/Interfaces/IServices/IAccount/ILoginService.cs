using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface ILoginService<TEntity, TBoolean> where TEntity : class where TBoolean : class
    {
        Task<ServiceResult<TEntity>> HandleLoginAsync(string username, string password, TBoolean? options = null);
    }
}
