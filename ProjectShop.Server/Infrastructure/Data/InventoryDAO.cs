using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class InventoryDAO : BaseDAO<InventoryModel>, IGetAllByIdAsync<InventoryModel>, IGetByStatusAsync<InventoryModel>, IGetDataByDateTimeAsync<InventoryModel>
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

        public async Task<List<InventoryModel>> GetAllByIdAsync(string id, string colIdName = "location_id")
        {
            try
            {
                string query = GetDataQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InventoryModel> inventories = await connection.QueryAsync<InventoryModel>(query, new { Input = id });
                return inventories.AsList();
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed, such as logging
                throw new InvalidOperationException($"Error retrieving inventory by ID: {ex.Message}", ex);
            }
        }

        public async Task<List<InventoryModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("inventory_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InventoryModel> inventories = await connection.QueryAsync<InventoryModel>(query, new { Input = status });
                return inventories.AsList();
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed, such as logging
                throw new InvalidOperationException($"Error retrieving inventory by status: {ex.Message}", ex);
            }
        }

        public async Task<List<InventoryModel>> GetAllByDateTimeAsync(DateTime input ,string colName = "inventory_last_updated_date")
        {
            try
            {
                string query = GetByDateTimeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InventoryModel> inventories = await connection.QueryAsync<InventoryModel>(query, new { Input = input });
                return inventories.AsList();
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed, such as logging
                throw new InvalidOperationException($"Error retrieving inventory by date: {ex.Message}", ex);
            }
        }

        public async Task<List<InventoryModel>> GetAllByDateTimeRangeAsync(DateTime firstTime, DateTime secondTime, string colName = "inventory_last_updated_date")
        {
            try
            {
                string query = GetByDateTimeRangeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InventoryModel> inventories = await connection.QueryAsync<InventoryModel>(query, new { FirstTime = firstTime, SecondTime = secondTime });
                return inventories.AsList();
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed, such as logging
                throw new InvalidOperationException($"Error retrieving inventory by date range: {ex.Message}", ex);
            }
        }

        public async Task<List<InventoryModel>> GetAllByMonthAndYearAsync(int month, int year, string colName = "inventory_last_updated_date")
        {
            try
            {
                string query = GetByMonthAndYearQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InventoryModel> inventories = await connection.QueryAsync<InventoryModel>(query, new { FirstTime = year, SecondTime = month });
                return inventories.AsList();
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed, such as logging
                throw new InvalidOperationException($"Error retrieving inventory by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<InventoryModel>> GetAllByYearAsync(int year, string colName = "inventory_last_updated_date")
        {
            try
            {
                string query = GetByYearQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InventoryModel> inventories = await connection.QueryAsync<InventoryModel>(query, new { Input = year });
                return inventories.AsList();
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed, such as logging
                throw new InvalidOperationException($"Error retrieving inventory by year: {ex.Message}", ex);
            }

        }
    }
}
