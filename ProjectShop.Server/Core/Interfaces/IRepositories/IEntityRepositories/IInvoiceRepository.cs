using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Invoice repository interface with specific query methods
    /// </summary>
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        // Query by CustomerId
        Task<IEnumerable<Invoice>> GetByCustomerIdAsync(uint customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Invoice>> GetByCustomerIdsAsync(IEnumerable<uint> customerIds, CancellationToken cancellationToken = default);

        // Query by EmployeeId
        Task<IEnumerable<Invoice>> GetByEmployeeIdAsync(uint employeeId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Invoice>> GetByEmployeeIdsAsync(IEnumerable<uint> employeeIds, CancellationToken cancellationToken = default);

        // Query by InvoiceStatus
        Task<IEnumerable<Invoice>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);

        // Query by InvoiceDate
        Task<IEnumerable<Invoice>> GetByInvoiceDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<Invoice>> GetByInvoiceYearAsync(int year, CancellationToken cancellationToken = default);
        Task<IEnumerable<Invoice>> GetByInvoiceMonthAndYearAsync(int year, int month, CancellationToken cancellationToken = default);

        // Query by TotalAmount
        Task<IEnumerable<Invoice>> GetByTotalAmountRangeAsync(decimal minAmount, decimal maxAmount, CancellationToken cancellationToken = default);

        // Query with navigation properties
        Task<Invoice?> GetByIdWithNavigationAsync(uint invoiceId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Invoice>> GetAllWithNavigationAsync(CancellationToken cancellationToken = default);

        // Business queries
        Task<decimal> GetTotalRevenueByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<Invoice>> GetTopInvoicesByAmountAsync(int topCount, CancellationToken cancellationToken = default);
    }
}
