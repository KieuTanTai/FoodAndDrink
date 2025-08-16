namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IUpdateAsync<T> where T : class
    {
        Task<int> UpdateAsync(T entity);
        Task<int> UpdateManyAsync(IEnumerable<T> entities);
    }
}
