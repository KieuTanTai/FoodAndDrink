using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Configuration;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class InventoryMovementDAO : BaseNoneUpdateDAO<InventoryMovementModel>, IGetAllByIdAsync<InventoryMovementModel>, IGetByEnumAsync<InventoryMovementModel>, IGetDataByDateTimeAsync<InventoryMovementModel>
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

        private string GetByMonthAndYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE YEAR({colName}) = @FirstTime AND MONTH({colName}) = @SecondTime";
        }

        private string GetByYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE Year({colName}) = @Input";
        }

        private string GetByDateTimeRange(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        private string GetByDateTime(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} = DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        public async Task<List<InventoryMovementModel>> GetAllByIdAsync(string id, string colIdName = "source_location_id")
        {
            try
            {
                string query = GetDataQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InventoryMovementModel> movements = await connection.QueryAsync<InventoryMovementModel>(query, new { Input = id });
                return movements.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving data by ID: {ex.Message}", ex);
            }
        }

        public async Task<List<InventoryMovementModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "inventory_movement_date")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InventoryMovementModel> movements = await connection.QueryAsync<InventoryMovementModel>(query, new { Input = dateTime });
                return movements.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving movements by date: {ex.Message}", ex);
            }
        }

        public async Task<List<InventoryMovementModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "inventory_movement_date")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InventoryMovementModel> movements = await connection.QueryAsync<InventoryMovementModel>(query, new { FirstTime = year, SecondTime = month });
                return movements.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving movements by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<InventoryMovementModel>> GetAllByYearAsync(int year, string colName = "inventory_movement_date")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InventoryMovementModel> movements = await connection.QueryAsync<InventoryMovementModel>(query, new { Input = year });
                return movements.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving movements by year: {ex.Message}", ex);
            }
        }

        public async Task<List<InventoryMovementModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "inventory_movement_date")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InventoryMovementModel> movements = await connection.QueryAsync<InventoryMovementModel>(query, new { FirstTime = startDate, SecondTime = endDate });
                return movements.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving movements by date range: {ex.Message}", ex);
            }
        }

        public async Task<List<InventoryMovementModel>> GetAllByEnumAsync<TEnum>(TEnum tEnum, string colName = "inventory_movement_reason") where TEnum : Enum
        {
            try
            {
                if (tEnum is not EInventoryMovementReason)
                    throw new ArgumentException($"Enum type {typeof(TEnum).Name} is not a valid inventory movement reason.");
                string query = GetDataQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InventoryMovementModel> movements = await connection.QueryAsync<InventoryMovementModel>(query, new { Input = tEnum.ToDbValue() });
                return movements.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving movements by enum: {ex.Message}", ex);
            }
        }

        public async Task<InventoryMovementModel> GetByEnumAsync<TEnum>(TEnum tEnum, string colName = "inventory_movement_reason") where TEnum : Enum
        {
            try
            {
                if (tEnum is not EInventoryMovementReason)
                    throw new ArgumentException($"Enum type {typeof(TEnum).Name} is not a valid inventory movement reason.");
                string query = GetDataQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                InventoryMovementModel? movement = await connection.QueryFirstOrDefaultAsync<InventoryMovementModel>(query, new { Input = tEnum.ToDbValue() });
                if (movement == null)
                    throw new Exception($"No movement found for enum value: {tEnum}");
                return movement;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving movement by enum: {ex.Message}", ex);
            }
        }
    }
}
