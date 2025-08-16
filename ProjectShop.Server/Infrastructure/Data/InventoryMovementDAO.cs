using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class InventoryMovementDAO : BaseNoneUpdateDAO<InventoryMovementModel>, IInventoryMovementDAO<InventoryMovementModel>
    {
        public InventoryMovementDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "inventory_movement", "inventory_movement_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (source_location_id, destination_location_id, inventory_movement_quantity
                    , inventory_movement_date, inventory_movement_reason) 
                      VALUES (@SourceLocationId, @DestinationLocationId, @InventoryMovementQuantity
                        , @InventoryMovementDate, InventoryMovementReason); SELECT LAST_INSERT_ID();";
        }

        public async Task<IEnumerable<InventoryMovementModel>> GetAllByEnumAsync<TEnum>(TEnum tEnum) where TEnum : Enum
        {
            if (tEnum is not EInventoryMovementReason reason)
                throw new ArgumentException("Invalid enum type for InventoryMovementModel.");
            return await GetByInputAsync(reason.ToString(), "inventory_movement_reason");
        }

        public async Task<IEnumerable<InventoryMovementModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid enum type for InventoryMovementModel date comparison.");
            return await GetByDateTimeAsync("inventory_movement_date", EQueryTimeType.DATE_TIME, type, dateTime);
        }

        public async Task<IEnumerable<InventoryMovementModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("inventory_movement_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate));

        public async Task<InventoryMovementModel?> GetByDestinationLocationId(uint locationId) 
            => await GetSingleDataAsync(locationId.ToString(), "destination_location_id");

        public async Task<IEnumerable<InventoryMovementModel>> GetByDestinationLocationIdsAsync(IEnumerable<uint> locationIds) => await GetByInputsAsync(locationIds.Select(locationId => locationId.ToString()), "destination_location_id");

        public async Task<InventoryMovementModel?> GetByEnumAsync<TEnum>(TEnum tEnum) where TEnum : Enum
        {
            if (tEnum is not EInventoryMovementReason reason)
                throw new ArgumentException("Invalid enum type for InventoryMovementModel.");
            return await GetSingleDataAsync(reason.ToString(), "inventory_movement_reason");
        }

        public async Task<InventoryMovementModel?> GetByInventoryIdAsync(uint inventoryId) => await GetSingleDataAsync(inventoryId.ToString(), "inventory_id");

        public async Task<IEnumerable<InventoryMovementModel>> GetByInventoryIdsAsync(IEnumerable<uint> inventoryIds) => await GetByInputsAsync(inventoryIds.Select(id => id.ToString()), "inventory_id");

        public async Task<IEnumerable<InventoryMovementModel>> GetByMonthAndYearAsync(int year, int month) => await GetByDateTimeAsync("inventory_movement_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));

        public async Task<IEnumerable<InventoryMovementModel>> GetByQuantityAsync<TCompareType>(int quantity, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid enum type for InventoryMovementModel quantity comparison.");
            return await GetByInputAsync(quantity.ToString(), type, "inventory_movement_quantity");
        }

        public async Task<InventoryMovementModel?> GetBySourceLocationId(uint locationId) => await GetSingleDataAsync(locationId.ToString(), "source_location_id");

        public async Task<IEnumerable<InventoryMovementModel>> GetBySourceLocationIdsAsync(IEnumerable<uint> locationIds) => await GetByInputsAsync(locationIds.Select(id => id.ToString()), "source_location_id");

        public async Task<IEnumerable<InventoryMovementModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid enum type for InventoryMovementModel year comparison.");
            return await GetByDateTimeAsync("inventory_movement_date", EQueryTimeType.YEAR, type, year);
        }
    }
}
