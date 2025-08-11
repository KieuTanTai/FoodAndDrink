namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IUpdateAsync<T> where T : class
    {
        Task<int> UpdateAsync(T entity, string? oldId);
        Task<int> UpdateManyAsync(IEnumerable<T> entities, IEnumerable<string>? oldIds);
    }
}
