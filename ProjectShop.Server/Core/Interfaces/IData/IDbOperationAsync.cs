namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IDbOperationAsync<TEntity> where TEntity : class
    {
        //Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(int? maxGetCount = null);
        Task<TEntity?> GetSingleDataAsync(string input); 
        Task<IEnumerable<TEntity>> GetByInputAsync(string input, int? maxGetCount = null); 
        Task<IEnumerable<TEntity>> GetByInputsAsync(IEnumerable<string> inputs, int? maxGetCount = null); 
        //Task<IEnumerable<TEntity>> GetByInputsAsync(IEnumerable<string> inputs); 
        Task<int> InsertAsync(TEntity entity);
        Task<int> InsertAsync(IEnumerable<TEntity> entities);
        //Task<bool> IsExistObjectAsync(T entity);
    }
}
