namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetByRangePriceAsync<T> where T : class
    {
        //Task<List<T>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice);
        Task<List<T>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice, string colName);
    }
}
