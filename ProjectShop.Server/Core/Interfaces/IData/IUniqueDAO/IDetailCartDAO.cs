namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailCartDAO<TEntity> : IGetDataByDateTimeAsync<TEntity>, IGetByRangePriceAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByCartIdAsync(uint cartId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string barcode, int? maxGetCount = null);
    }
}
