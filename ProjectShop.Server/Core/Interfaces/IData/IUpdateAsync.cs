namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IUpdateAsync<TEntity> where TEntity : class
    {
        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateAsync(IEnumerable<TEntity> entities);
    }
}
