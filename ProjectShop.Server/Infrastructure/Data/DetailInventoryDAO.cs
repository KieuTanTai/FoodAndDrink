using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DetailInventoryDAO : BaseDAO<DetailInventoryModel>, IGetDataByDateTimeAsync<DetailInventoryModel>
    {
        public DetailInventoryDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "detail_inventory", "detail_inventory_id", string.Empty)
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

        public async Task<List<DetailInventoryModel>> GetAllByMonthAndYearAsync(int month, int year, string colName = "detail_inventory_added_date")
        {
            try
            {
                string query = GetByMonthAndYearQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailInventoryModel> result = await connection.QueryAsync<DetailInventoryModel>(query, new { FirstTime = year, SecondTime = month });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving DetailInventoryModels by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailInventoryModel>> GetAllByYearAsync(int year, string colName = "detail_inventory_added_date")
        {
            try
            {
                string query = GetByYearQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailInventoryModel> result = await connection.QueryAsync<DetailInventoryModel>(query, new { Input = year });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving DetailInventoryModels by year: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailInventoryModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "detail_inventory_added_date")
        {
            try
            {
                string query = GetByDateTimeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailInventoryModel> result = await connection.QueryAsync<DetailInventoryModel>(query, new { Input = dateTime });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving DetailInventoryModels by date: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailInventoryModel>> GetAllByDateTimeRangeAsync(DateTime firstTime, DateTime secondTime, string colName = "detail_inventory_added_date")
        {
            try
            {
                string query = GetByDateTimeRangeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailInventoryModel> result = await connection.QueryAsync<DetailInventoryModel>(query, new { FirstTime = firstTime, SecondTime = secondTime });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving DetailInventoryModels by date range: {ex.Message}", ex);
            }
        }
    }
}
