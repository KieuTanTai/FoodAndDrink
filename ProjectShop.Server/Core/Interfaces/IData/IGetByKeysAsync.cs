namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetByKeysAsync<TEntity, TKey> where TEntity : class where TKey : struct
    {
        //public string GetSingleDataString();
        Task<TEntity?> GetByKeysAsync(TKey keys);
        Task<IEnumerable<TEntity>> GetByListKeysAsync(IEnumerable<TKey> keys, int? maxGetCount = null);
    }
}
