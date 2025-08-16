namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetByRangePriceAsync<T> where T : class
    {
        //Task<IEnumerable<T>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<T>> GetByInputPriceAsync<TEnum>(decimal price, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<T>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice);
    }
}
