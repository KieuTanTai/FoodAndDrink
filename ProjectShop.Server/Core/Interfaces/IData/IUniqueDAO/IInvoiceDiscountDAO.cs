using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IInvoiceDiscountDAO<TEntity, TKey> : IGetByKeysAsync<TEntity, TKey> where TEntity : class where TKey : struct
    {
        Task<IEnumerable<TEntity>> GetByInvoiceId(uint invoiceId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetBySaleEventId(uint saleEventId, int? maxGetCount = null);
    }
}
