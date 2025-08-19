namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IUpdateAsync<TEntity> where TEntity : class
    {
        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateManyAsync(IEnumerable<TEntity> entities);
    }
}
