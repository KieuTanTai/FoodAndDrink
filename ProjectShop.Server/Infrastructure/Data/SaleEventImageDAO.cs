using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class SaleEventImageDAO : BaseDAO<SaleEventImageModel>, ISaleEventImageDAO<SaleEventImageModel>
    {
        public SaleEventImageDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "sale_event_image", "sale_event_image_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (sale_event_id, sale_event_image_url, sale_event_image_priority) 
                      VALUES (@SaleEventId, @SaleEventImageUrl, @SaleEventImagePriority); 
                      SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET sale_event_image_url = @SaleEventImageUrl, 
                          sale_event_image_priority = @SaleEventImagePriority 
                      WHERE {ColumnIdName} = @{colIdName};";
        }

        public async Task<IEnumerable<SaleEventImageModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("sale_event_image_created_date", EQueryTimeType.DATE_TIME, ct, dateTime);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<SaleEventImageModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("sale_event_image_created_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate));

        public async Task<IEnumerable<SaleEventImageModel>> GetByMonthAndYearAsync(int year, int month)
            => await GetByDateTimeAsync("sale_event_image_created_date", EQueryTimeType.MONTH_AND_YEAR, (year, month));

        public async Task<IEnumerable<SaleEventImageModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("sale_event_image_created_date", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // ----------- LastUpdatedDate -----------
        public async Task<IEnumerable<SaleEventImageModel>> GetByLastUpdatedDateAsync<TCompareType>(DateTime lastUpdated, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("sale_event_image_last_updated_date", EQueryTimeType.DATE_TIME, ct, lastUpdated);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<SaleEventImageModel>> GetByLastUpdatedDateTimeRangeAsync(DateTime start, DateTime end)
            => await GetByDateTimeAsync("sale_event_image_last_updated_date", EQueryTimeType.DATE_TIME_RANGE, (start, end));

        public async Task<IEnumerable<SaleEventImageModel>> GetByLastUpdatedMonthAndYearAsync(int month, int year)
            => await GetByDateTimeAsync("sale_event_image_last_updated_date", EQueryTimeType.MONTH_AND_YEAR, (year, month));

        public async Task<IEnumerable<SaleEventImageModel>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("sale_event_image_last_updated_date", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // ----------- SaleEventId -----------
        public async Task<IEnumerable<SaleEventImageModel>> GetBySaleEventIdAsync(uint saleEventId)
            => await GetByInputAsync(saleEventId.ToString(), "sale_event_id");
    }
}
