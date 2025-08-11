using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class ProductLotInventoryDAO : BaseNoneUpdateDAO<ProductLotInventoryModel>, IGetDataByDateTimeAsync<ProductLotInventoryModel>, IGetByKeysAsync<ProductLotInventoryModel, ProductLotInventoryKey>
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

        private string GetByDateTimeRangeQuery(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} 
                      WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        private string GetByDateTimeQuery(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} WHERE {colName} = DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        private string GetByMonthAndYearQuery(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} 
                      WHERE YEAR({colName}) = @FirstTime AND MONTH({colName}) = @SecondTime";
        }

        private string GetByYearQuery(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} WHERE YEAR({colName}) = @Input";
        }

        public async Task<List<ProductLotInventoryModel>> GetByListKeysAsync(IEnumerable<ProductLotInventoryKey> keys)
        {
            try
            {
                string query = $@"SELECT * FROM {TableName} 
                                  WHERE product_lot_id = @ProductLotId AND inventory_id = @InventoryId";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductLotInventoryModel> results = await connection.QueryAsync<ProductLotInventoryModel>(query, keys);
                return results.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting ProductLotInventory by keys: {ex.Message}", ex);
            }   
        }

        public async Task<ProductLotInventoryModel> GetByKeysAsync(ProductLotInventoryKey keys)
        {
            try
            {
                string query = $@"SELECT * FROM {TableName} 
                                  WHERE product_lot_id = @ProductLotId AND inventory_id = @InventoryId";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                ProductLotInventoryModel? result = await connection.QueryFirstOrDefaultAsync<ProductLotInventoryModel>(query, keys);
                if (result == null)
                    throw new KeyNotFoundException($"ProductLotInventory with key {keys.ProductLotId}, {keys.InventoryId} not found.");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving ProductLotInventory by key: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductLotInventoryModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "product_lot_inventory_added_date")
        {
            try
            {
                string query = GetByMonthAndYearQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductLotInventoryModel> result = await connection.QueryAsync<ProductLotInventoryModel>(query, new { FirstTime = year, SecondTime = month });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving ProductLotInventoryModels by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductLotInventoryModel>> GetAllByYearAsync(int year, string colName = "product_lot_inventory_added_date")
        {
            try
            {
                string query = GetByYearQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductLotInventoryModel> result = await connection.QueryAsync<ProductLotInventoryModel>(query, new { Input = year });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving ProductLotInventoryModels by year: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductLotInventoryModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "product_lot_inventory_added_date")
        {
            try
            {
                string query = GetByDateTimeRangeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductLotInventoryModel> result = await connection.QueryAsync<ProductLotInventoryModel>(query, new { FirstTime = startDate, SecondTime = endDate });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving ProductLotInventoryModels by date range: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductLotInventoryModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "product_lot_inventory_added_date")
        {
            try
            {
                string query = GetByDateTimeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductLotInventoryModel> result = await connection.QueryAsync<ProductLotInventoryModel>(query, new { Input = dateTime });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving ProductLotInventoryModels by date: {ex.Message}", ex);
            }
        }
    }
}
