namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ICartDAO<T> where T : class
    {
        Task<IEnumerable<T>> GetByTotalPriceAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<T>> GetByTotalPriceAsync<TEnum>(decimal price, TEnum compareType) where TEnum : Enum;
    }
}
