namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ICartDAO<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByTotalPriceAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<TEntity>> GetByTotalPriceAsync<TEnum>(decimal price, TEnum compareType) where TEnum : Enum;
    }
}
