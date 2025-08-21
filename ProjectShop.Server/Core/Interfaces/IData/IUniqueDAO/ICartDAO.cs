namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ICartDAO<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByTotalPriceAsync(decimal minPrice, decimal maxPrice, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByTotalPriceAsync<TEnum>(decimal price, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
    }
}
