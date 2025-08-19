namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IDbOperationAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(int maxGetCount);
        Task<TEntity?> GetSingleDataAsync(string input); // remove colName for simplicity
        Task<IEnumerable<TEntity>> GetByInputAsync(string input, int maxGetCount); // remove colName for simplicity
        Task<IEnumerable<TEntity>> GetByInputsAsync(IEnumerable<string> inputs, int maxGetCount); // remove colName for simplicity
        Task<IEnumerable<TEntity>> GetByInputsAsync(IEnumerable<string> inputs); // remove colName for simplicity
        Task<int> InsertAsync(TEntity entity);
        Task<int> InsertManyAsync(IEnumerable<TEntity> entities);
        //Task<bool> IsExistObjectAsync(T entity);
    }
}
