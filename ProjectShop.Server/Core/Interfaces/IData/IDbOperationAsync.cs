namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IDbOperationAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetSingleDataAsync(string input); // remove colName for simplicity
        Task<IEnumerable<T>> GetByInputsAsync(IEnumerable<string> inputs); // remove colName for simplicity
        Task<int> InsertAsync(T entity);
        Task<int> InsertManyAsync(IEnumerable<T> entities);
        //Task<bool> IsExistObjectAsync(T entity);
    }
}
