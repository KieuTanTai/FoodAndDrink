using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class InvoiceRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<Invoice>(context, maxGetRecord), IInvoiceRepository
    {
        public async Task<IEnumerable<Invoice>> GetByCustomerIdAsync(uint customerId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetByCustomerIdsAsync(IEnumerable<uint> customerIds, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetByEmployeeIdAsync(uint employeeId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetByEmployeeIdsAsync(IEnumerable<uint> employeeIds, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetByInvoiceDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetByInvoiceYearAsync(int year, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetByInvoiceMonthAndYearAsync(int year, int month, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetByTotalAmountRangeAsync(decimal minAmount, decimal maxAmount, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Invoice?> GetByIdWithNavigationAsync(uint invoiceId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetAllWithNavigationAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetTotalRevenueByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetTopInvoicesByAmountAsync(int topCount, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
