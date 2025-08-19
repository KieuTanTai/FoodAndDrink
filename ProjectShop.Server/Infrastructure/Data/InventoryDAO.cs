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
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "inventory", "inventory_id", string.Empty)
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

        public async Task<IEnumerable<InventoryModel>> GetAllByLocationIdsAsync(IEnumerable<uint> locationIds) => await GetByInputsAsync(locationIds.Select(locationId => locationId.ToString()), "location_id");

        public async Task<IEnumerable<InventoryModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("inventory_last_updated_date", EQueryTimeType.DATE_TIME, type, dateTime);
        }

        public async Task<IEnumerable<InventoryModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate) => await GetByDateTimeAsync("inventory_last_updated_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate));

        public async Task<InventoryModel?> GetByLocationIdAsync(uint locationId) => await GetSingleDataAsync(locationId.ToString(), "location_id");

        public async Task<IEnumerable<InventoryModel>> GetByMonthAndYearAsync(int year, int month) => await GetByDateTimeAsync("inventory_last_updated_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));

        public async Task<IEnumerable<InventoryModel>> GetByStatusAsync(bool status) => await GetByInputAsync(GetTinyIntString(status), "inventory_status");

        public async Task<IEnumerable<InventoryModel>> GetByStatusAsync(bool status, int maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "inventory_status", maxGetCount);

        public async Task<IEnumerable<InventoryModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("inventory_last_updated_date", EQueryTimeType.YEAR, type, year);
        }

    }
}
