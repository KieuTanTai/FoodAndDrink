namespace ProjectShop.Server.Core.Interfaces.IServices.Transaction.Invoice
{
    public interface IInvoiceManagementService<T> : IBaseEnumTimeService<T>, IBaseService<T> where T : class
    {
        Task<List<T>> GetAllByEnumAsync<TEnum>(TEnum value, string colName) where TEnum : Enum;
        Task<List<T>> GetAllByIdAsync(string id, string colName);
    }
}
