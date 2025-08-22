namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailInventoryMovementDAO<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByProductLotIdAsync(uint productLotId, int? maxGetCount);
        Task<IEnumerable<TEntity>> GetByProductLotIdsAsync(IEnumerable<uint> productLotIds, int? maxGetCount);
        Task<IEnumerable<TEntity>> GetByMovementIdAsync(uint movementId, int? maxGetCount);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string barcode, int? maxGetCount);
    }
}
