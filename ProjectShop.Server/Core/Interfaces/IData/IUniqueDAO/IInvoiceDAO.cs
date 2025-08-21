namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IInvoiceDAO<TEntity> : IGetDataByDateTimeAsync<TEntity>, IGetByStatusAsync<TEntity>, 
        IGetByRangePriceAsync<TEntity>, IGetByEnumAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByCustomerIdAsync(uint customerId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByCustomerIdsAsync(IEnumerable<uint> customerIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByEmployeeIdAsync(uint employeeId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByEmployeeIdsAsync(IEnumerable<uint> employeeIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByPaymentMethodIdAsync(uint paymentMethodId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByPaymentMethodIdsAsync(IEnumerable<uint> paymentMethodIds, int? maxGetCount = null);
    }
}
