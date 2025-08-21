namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetByRangePriceAsync<TEntity> where TEntity : class
    {
        //Task<IEnumerable<TEntity>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<TEntity>> GetByInputPriceAsync<TEnum>(decimal price, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice, int? maxGetCount = null);
    }
}
