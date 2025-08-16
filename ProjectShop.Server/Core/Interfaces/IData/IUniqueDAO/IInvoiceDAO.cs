namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IInvoiceDAO<T> : IGetDataByDateTimeAsync<T>, IGetByStatusAsync<T>, IGetByRangePriceAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetByCustomerIdAsync(uint customerId);
        Task<IEnumerable<T>> GetByCustomerIdsAsync(IEnumerable<uint> customerIds);
        Task<IEnumerable<T>> GetByEmployeeIdAsync(uint employeeId);
        Task<IEnumerable<T>> GetByEmployeeIdsAsync(IEnumerable<uint> employeeIds);
        Task<IEnumerable<T>> GetByPaymentMethodIdAsync(uint paymentMethodId);
        Task<IEnumerable<T>> GetByPaymentMethodIdsAsync(IEnumerable<uint> paymentMethodIds);
    }
}
