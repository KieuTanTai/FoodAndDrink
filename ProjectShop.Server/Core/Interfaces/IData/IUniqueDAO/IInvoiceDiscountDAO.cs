using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IInvoiceDiscountDAO<T, TKey> : IGetByKeysAsync<InvoiceDiscountModel, TKey> where T : class where TKey : struct
    {
        Task<IEnumerable<T>> GetByInvoiceId(uint invoiceId);
        Task<IEnumerable<T>> GetBySaleEventId(uint saleEventId);
    }
}
