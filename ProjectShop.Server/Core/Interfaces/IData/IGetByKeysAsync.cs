namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetByKeysAsync<T, TKey> where T : class where TKey : struct
    {
        //public string GetSingleDataString();
        Task<T> GetByKeysAsync(TKey keys);
        Task<List<T>> GetByListKeysAsync(IEnumerable<TKey> keys);
    }
}
