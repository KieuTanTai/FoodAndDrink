namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailInventoryMovementDAO<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByMovementIdAsync(uint movementId);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string barcode);
    }
}
