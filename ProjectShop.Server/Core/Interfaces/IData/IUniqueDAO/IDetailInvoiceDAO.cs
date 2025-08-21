namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailInvoiceDAO<TEntity> : IGetByStatusAsync<TEntity>, IGetByRangePriceAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByInvoiceIdAsync(uint invoiceId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string barcode, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByQuantityAsync<TEnum>(int quantity, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
    }
}
