namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailInvoiceDAO<TEntity> : IGetByStatusAsync<TEntity>, IGetByRangePriceAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByInvoiceIdAsync(uint invoiceId);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string barcode);
        Task<IEnumerable<TEntity>> GetByQuantityAsync<TEnum>(int quantity, TEnum compareType) where TEnum : Enum;
    }
}
