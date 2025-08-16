using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailCartDAO<T> : IGetDataByDateTimeAsync<T>, IGetByRangePriceAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetByCartId(uint cartId);
        Task<IEnumerable<T>> GetByProductBarcode(string barcode);
    }
}
