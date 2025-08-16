namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetRelativeAsync<T> where T : class
    {
        //public string GetQueryDataString(string colName);
        Task<IEnumerable<T>> GetByLikeStringAsync(string input);
    }
}
