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

        public async Task<IEnumerable<ProductLotInventoryModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_lot_inventory_added_date", EQueryTimeType.DATE_TIME, ct, dateTime);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<ProductLotInventoryModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate) => await GetByDateTimeAsync("product_lot_inventory_added_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate));

        public async Task<IEnumerable<ProductLotInventoryModel>> GetByMonthAndYearAsync(int year, int month) => await GetByDateTimeAsync("product_lot_inventory_added_date", EQueryTimeType.MONTH_AND_YEAR, (year, month));

        public async Task<IEnumerable<ProductLotInventoryModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_lot_inventory_added_date", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // ----------- InventoryId -----------
        public async Task<IEnumerable<ProductLotInventoryModel>> GetByInventoryIdAsync(uint inventoryId) => await GetByInputAsync(inventoryId.ToString(), "inventory_id");

        // ----------- InventoryQuantity -----------
        public async Task<IEnumerable<ProductLotInventoryModel>> GetByInventoryQuantityAsync<TCompareType>(int quantity, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDecimalAsync(quantity, ct, "product_lot_inventory_quantity");
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // ----------- ProductLotId -----------
        public async Task<IEnumerable<ProductLotInventoryModel>> GetByProductLotIdAsync(uint productLotId) => await GetByInputAsync(productLotId.ToString(), "product_lot_id");

        // ----------- Keys -----------
        public async Task<ProductLotInventoryModel> GetByKeysAsync(ProductLotInventoryKey keys) => await GetSingleByTwoIdAsync(ColumnIdName, SecondColumnIdName, keys.ProductLotId, keys.InventoryId);

        // ----------- ListKeys (giữ nguyên) -----------
        public async Task<IEnumerable<ProductLotInventoryModel>> GetByListKeysAsync(IEnumerable<ProductLotInventoryKey> keys)
        {
            try
            {
                string query = GetSelectByKeysQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductLotInventoryModel> results = await connection.QueryAsync<ProductLotInventoryModel>(query, keys);
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting data by list keys in {TableName}: {ex.Message}", ex);
            }
        }
    }
}
