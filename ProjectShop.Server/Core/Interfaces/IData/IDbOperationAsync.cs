namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IDbOperationAsync<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetSingleDataAsync(string input, string colName);
        Task<List<T>> GetByInputsAsync(IEnumerable<string> inputs, string colName);
        Task<int> InsertAsync(T entity);
        Task<int> InsertManyAsync(IEnumerable<T> entities);
        //Task<bool> IsExistObjectAsync(T entity);
    }
}
