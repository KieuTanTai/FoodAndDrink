namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailCartDAO<TEntity> : IGetDataByDateTimeAsync<TEntity>, IGetByRangePriceAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByCartIdAsync(uint cartId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByCartIdsAsync(IEnumerable<uint> cartIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string barcode, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodesAsync(IEnumerable<string> barcodes, int? maxGetCount = null)
    }
}
