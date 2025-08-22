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
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "invoice", "invoice_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (customer_id, employee_id, payment_method_id, invoice_total_price, 
                invoice_date, invoice_status, payment_type) 
                      VALUES (@CustomerId, @EmployeeId, @PaymentMethodId, @InvoiceTotalPrice, @InvoiceDate, @InvoiceStatus, @PaymentType); SELECT LAST_INSERT_ID();";
        }

        public async Task<IEnumerable<InvoiceModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.", nameof(compareType));
            return await GetByDateTimeAsync("invoice_date", EQueryTimeType.DATE_TIME, type, dateTime, maxGetCount);
        }

        public async Task<IEnumerable<InvoiceModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount)
            => await GetByDateTimeAsync("invoice_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate), maxGetCount);

        public async Task<IEnumerable<InvoiceModel>> GetByInputPriceAsync<TEnum>(decimal price, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.", nameof(compareType));
            return await GetByDecimalAsync(price, type, "invoice_total_price", maxGetCount);
        }

        public async Task<IEnumerable<InvoiceModel>> GetByMonthAndYearAsync(int year, int month, int? maxGetCount) 
            => await GetByDateTimeAsync("invoice_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month), maxGetCount);

        public async Task<IEnumerable<InvoiceModel>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice, int? maxGetCount) 
            => await GetByRangeDecimalAsync(minPrice, maxPrice, "invoice_total_price", maxGetCount);

        public async Task<IEnumerable<InvoiceModel>> GetByStatusAsync(bool status, int? maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "invoice_status", maxGetCount);

        public async Task<IEnumerable<InvoiceModel>> GetByYearAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.", nameof(compareType));
            return await GetByDateTimeAsync("invoice_date", EQueryTimeType.YEAR, type, year, maxGetCount);
        }

        public async Task<IEnumerable<InvoiceModel>> GetByCustomerIdAsync(uint customerId, int? maxGetCount) 
            => await GetByInputAsync(customerId.ToString(), "customer_id", maxGetCount);

        public async Task<IEnumerable<InvoiceModel>> GetByCustomerIdsAsync(IEnumerable<uint> customerIds, int? maxGetCount) 
            => await GetByInputsAsync(customerIds.Select(customerId => customerId.ToString()), "customer_id", maxGetCount);

        public async Task<IEnumerable<InvoiceModel>> GetByEmployeeIdAsync(uint employeeId, int? maxGetCount) 
            => await GetByInputAsync(employeeId.ToString(), "employee_id", maxGetCount);

        public async Task<IEnumerable<InvoiceModel>> GetByEmployeeIdsAsync(IEnumerable<uint> employeeIds, int? maxGetCount) 
            => await GetByInputsAsync(employeeIds.Select(employeeId => employeeId.ToString()), "employee_id", maxGetCount);

        public async Task<IEnumerable<InvoiceModel>> GetByPaymentMethodIdAsync(uint paymentMethodId, int? maxGetCount) 
            => await GetByInputAsync(paymentMethodId.ToString(), "payment_method_id", maxGetCount);

        public async Task<IEnumerable<InvoiceModel>> GetByPaymentMethodIdsAsync(IEnumerable<uint> paymentMethodIds, int? maxGetCount) 
            => await GetByInputsAsync(paymentMethodIds.Select(paymentMethodId => paymentMethodId.ToString()), "payment_method_id", maxGetCount);

        public async Task<IEnumerable<InvoiceModel>> GetAllByEnumAsync<TEnum>(TEnum tEnum, int? maxGetCount) where TEnum : Enum
        {
            if (tEnum is not EInvoicePaymentType type)
                throw new ArgumentException("Invalid enum type provided.", nameof(tEnum));
            return await GetByInputAsync(type.ToString(), "payment_type", maxGetCount);
        }

        public async Task<InvoiceModel?> GetByEnumAsync<TEnum>(TEnum tEnum) where TEnum : Enum
        {
            if (tEnum is not EInvoicePaymentType type)
                throw new ArgumentException("Invalid enum type provided.", nameof(tEnum));
            return await GetSingleDataAsync(type.ToString(), "payment_type");
        }
    }
}
