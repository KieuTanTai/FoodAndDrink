namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IDeleteByKeysAsync<TKey> where TKey : struct
    {
        //public string GetDeleteQuery();
        Task<int> DeleteByKeysAsync(TKey keys);
        Task<int> DeleteBySingleKeyAsync(string key, string colName);
    }
}
