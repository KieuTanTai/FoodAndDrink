namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailInventoryMovementDAO<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByProductLotIdAsync(uint productLotId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductLotIdsAsync(IEnumerable<uint> productLotIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByMovementIdAsync(uint movementId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByMovementIdsAsync(IEnumerable<uint> movementIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string barcode, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodesAsync(IEnumerable<string> barcodes, int? maxGetCount = null);
    }
}
