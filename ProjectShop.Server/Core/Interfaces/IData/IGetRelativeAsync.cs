namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetRelativeAsync<TEntity> where TEntity : class
    {
        //public string GetQueryDataString(string colName);
        Task<IEnumerable<TEntity>> GetByLikeStringAsync(string input, int? maxGetCount);
    }
}
