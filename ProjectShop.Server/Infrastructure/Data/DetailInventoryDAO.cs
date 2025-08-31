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
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "detail_inventory", "detail_inventory_id", string.Empty)
        {
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

        public async Task<IEnumerable<DetailInventoryModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("detail_inventory_added_date", EQueryTimeType.DATE_TIME, type, dateTime, maxGetCount);
        }

        public async Task<IEnumerable<DetailInventoryModel>> GetByLastUpdatedDateAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("detail_inventory_last_updated_date", EQueryTimeType.DATE_TIME, type, dateTime, maxGetCount);
        }

        public async Task<IEnumerable<DetailInventoryModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount)
            => await GetByDateTimeAsync("detail_inventory_added_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate), maxGetCount);

        public async Task<IEnumerable<DetailInventoryModel>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount)
            => await GetByDateTimeAsync("detail_inventory_last_updated_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate), maxGetCount);

        public async Task<IEnumerable<DetailInventoryModel>> GetByInventoryIdAsync(uint inventoryId, int? maxGetCount)
            => await GetByInputAsync(inventoryId.ToString(), "inventory_id", maxGetCount);

        public async Task<IEnumerable<DetailInventoryModel>> GetByInventoryIdsAsync(IEnumerable<uint> inventoryIds, int? maxGetCount)
            => await GetByInputsAsync(inventoryIds.Select(id => id.ToString()), "inventory_id", maxGetCount);

        public async Task<IEnumerable<DetailInventoryModel>> GetByMonthAndYearAsync(int year, int month, int? maxGetCount)
            => await GetByDateTimeAsync("detail_inventory_added_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month), maxGetCount);

        public async Task<IEnumerable<DetailInventoryModel>> GetByMonthAndYearLastUpdatedDateAsync(int year, int month, int? maxGetCount)
            => await GetByDateTimeAsync("detail_inventory_last_updated_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month), maxGetCount);

        public async Task<IEnumerable<DetailInventoryModel>> GetByProductBarcodeAsync(string barcode, int? maxGetCount)
            => await GetByInputAsync(barcode, "product_barcode", maxGetCount);

        public async Task<IEnumerable<DetailInventoryModel>> GetByProductBarcodesAsync(IEnumerable<string> barcodes, int? maxGetCount)
            => await GetByInputsAsync(barcodes, "product_barcode", maxGetCount);

        public async Task<IEnumerable<DetailInventoryModel>> GetByYearAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("detail_inventory_added_date", EQueryTimeType.YEAR, type, year, maxGetCount);
        }

        public Task<IEnumerable<DetailInventoryModel>> GetByYearLastUpdatedDateAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return GetByDateTimeAsync("detail_inventory_last_updated_date", EQueryTimeType.YEAR, type, year, maxGetCount);
        }

    }
}
