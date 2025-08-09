using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetByRangePriceAsync<T> where T : class
    {
        Task<List<T>> GetByRangePrice(decimal minPrice, decimal maxPrice);
    }
}
