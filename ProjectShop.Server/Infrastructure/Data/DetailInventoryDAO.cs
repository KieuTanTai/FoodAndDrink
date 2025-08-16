using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DetailInventoryDAO : BaseDAO<DetailInventoryModel>, IDetailInventoryDAO<DetailInventoryModel>
    {
        public DetailInventoryDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "detail_inventory", "detail_inventory_id", string.Empty)
        {
        }

        public Task<IEnumerable<DetailInventoryModel>> GetByYearLastUpdatedAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return GetByDateTimeAsync(
                "detail_inventory_last_updated_date",
                EQueryTimeType.YEAR,
                type,
                year
            );
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (inventory_id, product_barcode, detail_inventory_quantity, 
                detail_inventory_added_date, detail_inventory_last_updated_date) 
                      VALUES (@InventoryId, @ProductBarcode, @DetailInventoryQuantity, @DetailInventoryAddedDate, @DetailInventoryLastUpdatedDate); 
                      SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            return $@"UPDATE {TableName} 
                      SET detail_inventory_quantity = @DetailInventoryQuantity
                      WHERE detail_inventory_id = @DetailInventoryId";
        }

        public async Task<IEnumerable<DetailInventoryModel>> GetByDateTimeAddedAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("detail_inventory_added_date", EQueryTimeType.DATE_TIME, type, dateTime);
        }

        public async Task<IEnumerable<DetailInventoryModel>> GetByDateTimeLastUpdatedAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("detail_inventory_last_updated_date", EQueryTimeType.DATE_TIME, type, dateTime);
        }

        public async Task<IEnumerable<DetailInventoryModel>> GetByDateTimeRangeAddedAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("detail_inventory_added_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate));

        public async Task<IEnumerable<DetailInventoryModel>> GetByDateTimeRangeLastUpdatedAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("detail_inventory_last_updated_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate));

        public async Task<IEnumerable<DetailInventoryModel>> GetByInventoryId(uint inventoryId)
            => await GetByInputAsync(inventoryId.ToString(), "inventory_id");

        public async Task<IEnumerable<DetailInventoryModel>> GetByMonthAndYearAddedAsync(int year, int month)
            => await GetByDateTimeAsync("detail_inventory_added_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));

        public async Task<IEnumerable<DetailInventoryModel>> GetByMonthAndYearLastUpdatedAsync(int year, int month)
            => await GetByDateTimeAsync("detail_inventory_last_updated_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));

        public async Task<IEnumerable<DetailInventoryModel>> GetByProductBarcode(string barcode)
            => await GetByInputAsync(barcode, "product_barcode");

        public async Task<IEnumerable<DetailInventoryModel>> GetByYearAddedAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("detail_inventory_added_date", EQueryTimeType.YEAR, type, year);
        }
    }
}
