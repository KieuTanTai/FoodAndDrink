namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IInvoiceDAO<TEntity> : IGetDataByDateTimeAsync<TEntity>, IGetByStatusAsync<TEntity>, IGetByRangePriceAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByCustomerIdAsync(uint customerId);
        Task<IEnumerable<TEntity>> GetByCustomerIdsAsync(IEnumerable<uint> customerIds);
        Task<IEnumerable<TEntity>> GetByEmployeeIdAsync(uint employeeId);
        Task<IEnumerable<TEntity>> GetByEmployeeIdsAsync(IEnumerable<uint> employeeIds);
        Task<IEnumerable<TEntity>> GetByPaymentMethodIdAsync(uint paymentMethodId);
        Task<IEnumerable<TEntity>> GetByPaymentMethodIdsAsync(IEnumerable<uint> paymentMethodIds);
    }
}
