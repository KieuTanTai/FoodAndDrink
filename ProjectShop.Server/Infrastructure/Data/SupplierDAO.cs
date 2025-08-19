using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class SupplierDAO : BaseDAO<SupplierModel>, ISupplierDAO<SupplierModel>
    {
        public SupplierDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "supplier", "supplier_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            // add field `supplier_cooperation_date` if needed
            return $@"
                INSERT INTO {TableName} (
                    `supplier_name`,
                    `supplier_phone`,
                    `supplier_email`,
                    `company_location_id`,
                    `store_location_id`,
                    `supplier_status`
                ) VALUES (
                    @SupplierName,
                    @SupplierPhone,
                    @SupplierEmail,
                    @CompanyLocationId,
                    @StoreLocationId,
                    @SupplierStatus
                );
                SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            return $@"
                UPDATE {TableName} SET
                    `supplier_name` = @SupplierName,
                    `supplier_phone` = @SupplierPhone,
                    `supplier_email` = @SupplierEmail,
                    `company_location_id` = @CompanyLocationId,
                    `store_location_id` = @StoreLocationId,
                    `supplier_status` = @SupplierStatus
                WHERE {ColumnIdName} = @{ColumnIdName};";
        }

        public async Task<IEnumerable<SupplierModel>> GetAllByCompanyLocationIdAsync(uint locationId)
    => await GetByInputAsync(locationId.ToString(), "company_location_id");

        public async Task<IEnumerable<SupplierModel>> GetAllByStoreLocationIdAsync(uint locationId)
            => await GetByInputAsync(locationId.ToString(), "store_location_id");

        public async Task<SupplierModel?> GetByCompanyLocationIdAsync(uint locationId)
            => await GetSingleDataAsync(locationId.ToString(), "company_location_id");

        public async Task<IEnumerable<SupplierModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("supplier_cooperation_date", EQueryTimeType.DATE_TIME, ct, dateTime);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<SupplierModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("supplier_cooperation_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate));

        public async Task<SupplierModel?> GetByEmailAsync(string email)
            => await GetSingleDataAsync(email, "supplier_email");

        public async Task<IEnumerable<SupplierModel>> GetByEmailsAsync(IEnumerable<string> emails)
            => await GetByInputsAsync(emails, "supplier_email");

        public async Task<IEnumerable<SupplierModel>> GetByLikeStringAsync(string input)
            => await GetByLikeStringAsync(input, "supplier_name");

        public async Task<IEnumerable<SupplierModel>> GetByLikeStringAsync(string input, int maxGetCount)
            => await GetByLikeStringAsync(input, "supplier_name", maxGetCount);

        public async Task<IEnumerable<SupplierModel>> GetByMonthAndYearAsync(int year, int month)
            => await GetByDateTimeAsync("supplier_cooperation_date", EQueryTimeType.MONTH_AND_YEAR, (year, month));

        public async Task<SupplierModel?> GetByNameAsync(string name)
            => await GetSingleDataAsync(name, "supplier_name");

        public async Task<SupplierModel?> GetByPhoneAsync(string phone)
            => await GetSingleDataAsync(phone, "supplier_phone");

        public async Task<IEnumerable<SupplierModel>> GetByPhonesAsync(IEnumerable<string> phones)
            => await GetByInputsAsync(phones, "supplier_phone");

        public async Task<IEnumerable<SupplierModel>> GetByRelativeEmailAsync(string email)
            => await GetByLikeStringAsync(email, "supplier_email");

        public async Task<IEnumerable<SupplierModel>> GetByRelativePhoneAsync(string phone)
            => await GetByLikeStringAsync(phone, "supplier_phone");

        public async Task<IEnumerable<SupplierModel>> GetByStatusAsync(bool status)
            => await GetByInputAsync(GetTinyIntString(status), "supplier_status");

        public async Task<IEnumerable<SupplierModel>> GetByStatusAsync(bool status, int maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "supplier_status", maxGetCount);

        public async Task<SupplierModel?> GetByStoreLocationIdAsync(uint locationId)
            => await GetSingleDataAsync(locationId.ToString(), "store_location_id");

        public async Task<IEnumerable<SupplierModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("supplier_cooperation_date", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }
    }
}
