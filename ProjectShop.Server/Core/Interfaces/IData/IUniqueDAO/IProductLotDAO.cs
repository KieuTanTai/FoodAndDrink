namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductLotDAO<TEntity> : IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string barcode, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodesAsync(IEnumerable<string> barcodes, int? maxGetCount = null)
    }
}
