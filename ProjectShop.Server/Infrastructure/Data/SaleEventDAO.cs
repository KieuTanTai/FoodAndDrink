using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class SaleEventDAO : BaseDAO<SaleEventModel>, ISaleEventDAO<SaleEventModel>
    {
        public SaleEventDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "sale_event", "sale_event_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (sale_event_start_date, sale_event_end_date, sale_event_name, sale_event_status, sale_event_description, sale_event_discount_code) 
                      VALUES (@SaleEventStartDate, @SaleEventEndDate, @SaleEventName, @SaleEventStatus, @SaleEventDescription, @SaleEventDiscountCode); SELECT LAST_INSERT_ID();";
        }
        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET sale_event_status = @SaleEventStatus
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        public async Task<SaleEventModel?> GetByDiscountCodeAsync(string discountCode)
            => await GetSingleDataAsync(discountCode, "sale_event_discount_code");

        public async Task<IEnumerable<SaleEventModel>> GetByEndDateMonthAndYearAsync(int month, int year)
            => await GetByDateTimeAsync("sale_event_end_date", EQueryTimeType.MONTH_AND_YEAR, (year, month));

        public async Task<IEnumerable<SaleEventModel>> GetByEndDateRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("sale_event_end_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate));

        public async Task<IEnumerable<SaleEventModel>> GetByEndDateAsync<TCompareType>(DateTime endDate, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("sale_event_end_date", EQueryTimeType.DATE_TIME, ct, endDate);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<SaleEventModel>> GetByEndDateYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("sale_event_end_date", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<SaleEventModel>> GetByLikeStringAsync(string input)
            => await GetByLikeStringAsync(input, "sale_event_name");

        public async Task<SaleEventModel?> GetByNameAsync(string name)
            => await GetSingleDataAsync(name, "sale_event_name");

        public async Task<IEnumerable<SaleEventModel>> GetByRelativeDiscountCodeAsync(string discountCode)
            => await GetByLikeStringAsync(discountCode, "sale_event_discount_code");

        public async Task<IEnumerable<SaleEventModel>> GetByStartAndEndDateRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByStartAndEndDateRangeCustomAsync(startDate, endDate); // Method riêng cho logic này

        public async Task<IEnumerable<SaleEventModel>> GetByStartDateAsync<TCompareType>(DateTime startDate, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("sale_event_start_date", EQueryTimeType.DATE_TIME, ct, startDate);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<SaleEventModel>> GetByStartDateMonthAndYearAsync(int month, int year)
            => await GetByDateTimeAsync("sale_event_start_date", EQueryTimeType.MONTH_AND_YEAR, (year, month));

        public async Task<IEnumerable<SaleEventModel>> GetByStartDateRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("sale_event_start_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate));

        public async Task<IEnumerable<SaleEventModel>> GetByStartDateYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("sale_event_start_date", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<SaleEventModel>> GetByStatusAsync(bool status)
            => await GetByInputAsync(GetTinyIntString(status), "sale_event_status");

        public async Task<IEnumerable<SaleEventModel>> GetByTextAsync(string text)
            => await GetByLikeStringAsync(text, "sale_event_name");

        // Method riêng cho logic lấy theo cả start và end date range
        private async Task<IEnumerable<SaleEventModel>> GetByStartAndEndDateRangeCustomAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                string query = $@" SELECT * FROM {TableName}
                    WHERE sale_event_start_date >= @StartDate AND sale_event_end_date <= DATE_ADD(@EndDate, INTERVAL 1 DAY)";
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<SaleEventModel> result = await connection.QueryAsync<SaleEventModel>(query, new { StartDate = startDate, EndDate = endDate });
                if (result == null || !result.Any())
                    throw new Exception("No SaleEvents found for the given date range.");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting SaleEvents by start and end date range: {ex.Message}", ex);
            }
        }
    }
}
