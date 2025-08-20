using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DisposeProductDAO : BaseNoneUpdateDAO<DisposeProductModel>, IDisposeProductDAO<DisposeProductModel>
    {
        public DisposeProductDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "dispose_product", "dispose_product_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (product_barcode, location_id, dispose_by_employee_id, 
                        dispose_reason_id, dispose_quantity, disposed_date) 
                      VALUES (@ProductBarcode, @LocationId, @DisposeByEmployeeId, @DisposeReasonId, 
                              @DisposeQuantity, @DisposedDate); SELECT LAST_INSERT_ID();";
        }

        public async Task<IEnumerable<DisposeProductModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type for date time comparison.");
            return await GetByDateTimeAsync("disposed_date", EQueryTimeType.DATE_TIME, type, dateTime);
        }

        public async Task<IEnumerable<DisposeProductModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("disposed_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate));

        public async Task<IEnumerable<DisposeProductModel>> GetByEmployeeIdAsync(uint employeeId) => await GetByInputAsync(employeeId.ToString(), "dispose_by_employee_id");

        public async Task<IEnumerable<DisposeProductModel>> GetByLocationIdAsync(uint locationId) => await GetByInputAsync(locationId.ToString(), "location_id");

        public async Task<IEnumerable<DisposeProductModel>> GetByMonthAndYearAsync(int year, int month) => await GetByDateTimeAsync("month_and_year", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));

        public async Task<IEnumerable<DisposeProductModel>> GetByProductBarcodeAsync(string barcode) => await GetByInputAsync(barcode, "product_barcode");

        public async Task<IEnumerable<DisposeProductModel>> GetByQuantityIdAsync<TCompareType>(int quantity, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type for quantity comparison.");
            return await GetByInputAsync(quantity.ToString(), type, "dispose_quantity");
        }

        public async Task<IEnumerable<DisposeProductModel>> GetByReasonIdAsync(uint reasonId) => await GetByInputAsync(reasonId.ToString(), "dispose_reason_id");

        public async Task<IEnumerable<DisposeProductModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type for year comparison.");
            return await GetByDateTimeAsync("year", EQueryTimeType.YEAR, type, year);
        }
    }
}
