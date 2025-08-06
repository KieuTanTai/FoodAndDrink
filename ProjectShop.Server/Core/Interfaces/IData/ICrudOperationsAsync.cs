namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface ICrudOperationsAsync<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task<List<T>> GetByIdsAsync(IEnumerable<string> ids);
        Task<int> InsertAsync(T entity);
        Task<int> InsertManyAsync(IEnumerable<T> entities);
        Task<int> UpdateAsync(T entity);
        Task<int> UpdateManyAsync(IEnumerable<T> entities);
        Task<int> DeleteAsync(string id);
        Task<int> DeleteManyAsync(IEnumerable<string> ids);
        Task<bool> IsExistObjectAsync(T entity);
    }
}
