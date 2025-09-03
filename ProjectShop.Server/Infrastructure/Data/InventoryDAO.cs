using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class InventoryDAO : BaseDAO<InventoryModel>, IInventoryDAO<InventoryModel>
    {
        public InventoryDAO(
            IDbConnectionFactory connectionFactory,
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "inventory", "inventory_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (location_id, inventory_status) 
                      VALUES (@LocationId, @InventoryStatus); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET inventory_status = @InventoryStatus
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        public async Task<IEnumerable<InventoryModel>> GetByLocationIdsAsync(IEnumerable<uint> locationIds, int? maxGetCount) 
            => await GetByInputsAsync(locationIds.Select(locationId => locationId.ToString()), "location_id", maxGetCount);

        public async Task<IEnumerable<InventoryModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("inventory_last_updated_date", EQueryTimeType.DATE_TIME, type, dateTime, maxGetCount);
        }

        public async Task<IEnumerable<InventoryModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount) 
            => await GetByDateTimeAsync("inventory_last_updated_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate), maxGetCount);

        public async Task<InventoryModel?> GetByLocationIdAsync(uint locationId) 
            => await GetSingleDataAsync(locationId.ToString(), "location_id");

        public async Task<IEnumerable<InventoryModel>> GetByMonthAndYearAsync(int year, int month, int? maxGetCount) 
            => await GetByDateTimeAsync("inventory_last_updated_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month), maxGetCount);

        public async Task<IEnumerable<InventoryModel>> GetByStatusAsync(bool status, int? maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "inventory_status", maxGetCount);

        public async Task<IEnumerable<InventoryModel>> GetByYearAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("inventory_last_updated_date", EQueryTimeType.YEAR, type, year, maxGetCount);
        }

    }
}
