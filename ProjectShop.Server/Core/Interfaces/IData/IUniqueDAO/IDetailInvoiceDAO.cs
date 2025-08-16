using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailInvoiceDAO<T> : IGetByStatusAsync<T>, IGetByRangePriceAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetByInvoiceIdAsync(uint invoiceId);
        Task<IEnumerable<T>> GetByProductBarcodeAsync(string barcode);
        Task<IEnumerable<T>> GetByQuantityAsync<TEnum>(int quantity, TEnum compareType) where TEnum : Enum;
    }
}
