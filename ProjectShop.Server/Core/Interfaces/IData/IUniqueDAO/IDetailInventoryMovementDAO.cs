namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailInventoryMovementDAO<T> where T : class
    {
        Task<IEnumerable<T>> GetByMovementIdAsync(uint movementId);
        Task<IEnumerable<T>> GetByProductBarcodeAsync(string barcode);
    }
}
