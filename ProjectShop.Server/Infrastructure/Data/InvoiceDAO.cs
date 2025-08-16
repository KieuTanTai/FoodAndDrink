using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class InvoiceDAO : BaseNoneUpdateDAO<InvoiceModel>, IInvoiceDAO<InvoiceModel>
    {
        public InvoiceDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "invoice", "invoice_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (customer_id, employee_id, payment_method_id, invoice_total_price, 
                invoice_date, invoice_status) 
                      VALUES (@CustomerId, @EmployeeId, @PaymentMethodId, @InvoiceTotalPrice, @InvoiceDate, @InvoiceStatus); SELECT LAST_INSERT_ID();";
        }

        public async Task<IEnumerable<InvoiceModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.", nameof(compareType));
            return await GetByDateTimeAsync("invoice_date", EQueryTimeType.DATE_TIME, type, dateTime);
        }

        public async Task<IEnumerable<InvoiceModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate) 
            => await GetByDateTimeAsync("invoice_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate));

        public async Task<IEnumerable<InvoiceModel>> GetByInputPriceAsync<TEnum>(decimal price, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.", nameof(compareType));
            return await GetByDecimalAsync(price, type, "invoice_total_price");
        }

        public async Task<IEnumerable<InvoiceModel>> GetByMonthAndYearAsync(int year, int month) => await GetByDateTimeAsync("invoice_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));

        public async Task<IEnumerable<InvoiceModel>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice) => await GetByRangeDecimalAsync(minPrice, maxPrice, "invoice_total_price");

        public async Task<IEnumerable<InvoiceModel>> GetByStatusAsync(bool status) => await GetByInputAsync(GetTinyIntString(status), "invoice_status");

        public async Task<IEnumerable<InvoiceModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.", nameof(compareType));
            return await GetByDateTimeAsync("invoice_date", EQueryTimeType.YEAR, type, year);
        }

        public async Task<IEnumerable<InvoiceModel>> GetByCustomerIdAsync(uint customerId) => await GetByInputAsync(customerId.ToString(), "customer_id");

        public async Task<IEnumerable<InvoiceModel>> GetByCustomerIdsAsync(IEnumerable<uint> customerIds) => await GetByInputsAsync(customerIds.Select(customerId => customerId.ToString()), "customer_id");

        public async Task<IEnumerable<InvoiceModel>> GetByEmployeeIdAsync(uint employeeId) => await GetByInputAsync(employeeId.ToString(), "employee_id");

        public async Task<IEnumerable<InvoiceModel>> GetByEmployeeIdsAsync(IEnumerable<uint> employeeIds) => await GetByInputsAsync(employeeIds.Select(employeeId => employeeId.ToString()), "employee_id");

        public async Task<IEnumerable<InvoiceModel>> GetByPaymentMethodIdAsync(uint paymentMethodId) => await GetByInputAsync(paymentMethodId.ToString(), "payment_method_id");

        public async Task<IEnumerable<InvoiceModel>> GetByPaymentMethodIdsAsync(IEnumerable<uint> paymentMethodIds) => await GetByInputsAsync(paymentMethodIds.Select(paymentMethodId => paymentMethodId.ToString()), "payment_method_id");
    }
}
