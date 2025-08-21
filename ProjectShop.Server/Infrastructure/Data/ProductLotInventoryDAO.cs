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
    public class ProductLotInventoryDAO : BaseNoneUpdateDAO<ProductLotInventoryModel>, IProductLotInventoryDAO<ProductLotInventoryModel, ProductLotInventoryKey>
    {
        public ProductLotInventoryDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "product_lot_inventory", "product_lot_id", "inventory_id")
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (product_lot_id, inventory_id, product_lot_inventory_quantity, product_lot_inventory_added_date) 
                      VALUES (@ProductLotId, @InventoryId, @ProductLotInventoryId, @ProductLotInventoryAddedDate); SELECT LAST_INSERT_ID();";
        }

        private string GetSelectByKeysQuery()
        {
            return $@"SELECT * FROM {TableName} WHERE {ColumnIdName} = @ProductLotId AND {SecondColumnIdName} = @InventoryId;";
        }

        public async Task<IEnumerable<ProductLotInventoryModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_lot_inventory_added_date", EQueryTimeType.DATE_TIME, ct, dateTime, maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<ProductLotInventoryModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount)
            => await GetByDateTimeAsync("product_lot_inventory_added_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate), maxGetCount);

        public async Task<IEnumerable<ProductLotInventoryModel>> GetByMonthAndYearAsync(int year, int month, int? maxGetCount)
            => await GetByDateTimeAsync("product_lot_inventory_added_date", EQueryTimeType.MONTH_AND_YEAR, (year, month), maxGetCount);

        public async Task<IEnumerable<ProductLotInventoryModel>> GetByYearAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_lot_inventory_added_date", EQueryTimeType.YEAR, ct, year, maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // ----------- InventoryId -----------
        public async Task<IEnumerable<ProductLotInventoryModel>> GetByInventoryIdAsync(uint inventoryId, int? maxGetCount)
            => await GetByInputAsync(inventoryId.ToString(), "inventory_id", maxGetCount);

        // ----------- InventoryQuantity -----------
        public async Task<IEnumerable<ProductLotInventoryModel>> GetByInventoryQuantityAsync<TCompareType>(int quantity, TCompareType compareType, int? maxGetCount) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDecimalAsync(quantity, ct, "product_lot_inventory_quantity", maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // ----------- ProductLotId -----------
        public async Task<IEnumerable<ProductLotInventoryModel>> GetByProductLotIdAsync(uint productLotId, int? maxGetCount)
            => await GetByInputAsync(productLotId.ToString(), "product_lot_id", maxGetCount);

        // ----------- Keys -----------
        public async Task<ProductLotInventoryModel> GetByKeysAsync(ProductLotInventoryKey keys)
            => await GetSingleByTwoIdAsync(ColumnIdName, SecondColumnIdName, keys.ProductLotId, keys.InventoryId);

        // ----------- ListKeys (giữ nguyên) -----------
        public async Task<IEnumerable<ProductLotInventoryModel>> GetByListKeysAsync(IEnumerable<ProductLotInventoryKey> keys, int? maxGetCount)
        {
            string query = "";
            if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentOutOfRangeException("maxGetCount must be greater than 0.");
            else if (maxGetCount.HasValue && maxGetCount.Value > 0)
                query = $"{GetSelectByKeysQuery()} LIMIT @MaxGetCount";
            else
                query = GetSelectByKeysQuery();

            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.AddDynamicParams(keys);
                parameters.Add("MaxGetCount", maxGetCount);
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<ProductLotInventoryModel> results = await connection.QueryAsync<ProductLotInventoryModel>(query, parameters);
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting data by list keys in {TableName}: {ex.Message}", ex);
            }
        }
    }
}
