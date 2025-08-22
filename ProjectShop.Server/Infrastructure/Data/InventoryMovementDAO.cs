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
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "inventory_movement", "inventory_movement_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (source_location_id, destination_location_id, inventory_movement_quantity
                    , inventory_movement_date, inventory_movement_reason) 
                      VALUES (@SourceLocationId, @DestinationLocationId, @InventoryMovementQuantity
                        , @InventoryMovementDate, InventoryMovementReason); SELECT LAST_INSERT_ID();";
        }

        public async Task<InventoryMovementModel?> GetByEnumAsync<TEnum>(TEnum tEnum) where TEnum : Enum
        {
            if (tEnum is not EInventoryMovementReason reason)
                throw new ArgumentException("Invalid enum type for InventoryMovementModel.");
            return await GetSingleDataAsync(reason.ToString(), "inventory_movement_reason");
        }

        public async Task<InventoryMovementModel?> GetByDestinationLocationId(uint locationId)
            => await GetSingleDataAsync(locationId.ToString(), "destination_location_id");

        public async Task<InventoryMovementModel?> GetByInventoryIdAsync(uint inventoryId)
            => await GetSingleDataAsync(inventoryId.ToString(), "inventory_id");

        public async Task<InventoryMovementModel?> GetBySourceLocationId(uint locationId)
            => await GetSingleDataAsync(locationId.ToString(), "source_location_id");

        public async Task<IEnumerable<InventoryMovementModel>> GetAllByEnumAsync<TEnum>(TEnum tEnum, int? maxGetCount) where TEnum : Enum
        {
            if (tEnum is not EInventoryMovementReason reason)
                throw new ArgumentException("Invalid enum type for InventoryMovementModel.");
            return await GetByInputAsync(reason.ToString(), "inventory_movement_reason", maxGetCount);
        }

        public async Task<IEnumerable<InventoryMovementModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid enum type for InventoryMovementModel date comparison.");
            return await GetByDateTimeAsync("inventory_movement_date", EQueryTimeType.DATE_TIME, type, dateTime, maxGetCount);
        }

        public async Task<IEnumerable<InventoryMovementModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount)
            => await GetByDateTimeAsync("inventory_movement_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate), maxGetCount);

        public async Task<IEnumerable<InventoryMovementModel>> GetByDestinationLocationIdsAsync(IEnumerable<uint> locationIds, int? maxGetCount) 
            => await GetByInputsAsync(locationIds.Select(locationId => locationId.ToString()), "destination_location_id", maxGetCount);

        public async Task<IEnumerable<InventoryMovementModel>> GetByInventoryIdsAsync(IEnumerable<uint> inventoryIds, int? maxGetCount) 
            => await GetByInputsAsync(inventoryIds.Select(id => id.ToString()), "inventory_id", maxGetCount);

        public async Task<IEnumerable<InventoryMovementModel>> GetByMonthAndYearAsync(int year, int month, int? maxGetCount) 
            => await GetByDateTimeAsync("inventory_movement_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month), maxGetCount);

        public async Task<IEnumerable<InventoryMovementModel>> GetByQuantityAsync<TCompareType>(int quantity, TCompareType compareType, int? maxGetCount) where TCompareType : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid enum type for InventoryMovementModel quantity comparison.");
            return await GetByInputAsync(quantity.ToString(), type, "inventory_movement_quantity", maxGetCount);
        }

        public async Task<IEnumerable<InventoryMovementModel>> GetBySourceLocationIdsAsync(IEnumerable<uint> locationIds, int? maxGetCount) 
            => await GetByInputsAsync(locationIds.Select(id => id.ToString()), "source_location_id", maxGetCount);

        public async Task<IEnumerable<InventoryMovementModel>> GetByYearAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid enum type for InventoryMovementModel year comparison.");
            return await GetByDateTimeAsync("inventory_movement_date", EQueryTimeType.YEAR, type, year, maxGetCount);
        }
    }
}
