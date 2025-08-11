using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class SaleEventDAO : BaseDAO<SaleEventModel>, IGetRelativeAsync<SaleEventModel>, IGetDataByDateTimeAsync<SaleEventModel>, IGetByStatusAsync<SaleEventModel>
    {
        public SaleEventDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "sale_event", "sale_event_id", string.Empty)
        {
        }
        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (sale_event_start_date, sale_event_end_date, sale_event_name, sale_event_status, sale_event_description, sale_event_discount_code) 
                      VALUES (@SaleEventStartDate, @SaleEventEndDate, @SaleEventName, @SaleEventStatus, @SaleEventDescription, @SaleEventDiscountCode); SELECT LAST_INSERT_ID();";
        }
        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET sale_event_status = @SaleEventStatus
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        private string GetByStatusQuery()
        {
            return $@"SELECT * FROM {TableName} 
                      WHERE sale_event_status = @SaleEventStatus";
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

        private string GetByYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE YEAR({colName}) = @Input";
        }

        private string GetByMonthAndYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE YEAR({colName}) = @Year AND MONTH({colName}) = @Month";
        }

        private string GetRelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} LIKE @Input";
        }

        public async Task<List<SaleEventModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "sale_event_start_date")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SaleEventModel> result = await connection.QueryAsync<SaleEventModel>(query, new { Year = year, Month = month });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving SaleEventModels by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<SaleEventModel>> GetAllByYearAsync(int year, string colName = "sale_event_start_date")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SaleEventModel> result = await connection.QueryAsync<SaleEventModel>(query, new { Input = year });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving SaleEventModels by year: {ex.Message}", ex);
            }
        }

        public async Task<List<SaleEventModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "sale_event_start_date")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SaleEventModel> result = await connection.QueryAsync<SaleEventModel>(query, new { FirstTime = startDate, SecondTime = endDate });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving SaleEventModels by date range: {ex.Message}", ex);
            }
        }

        public async Task<List<SaleEventModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "sale_event_start_date")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SaleEventModel> result = await connection.QueryAsync<SaleEventModel>(query, new { Input = dateTime });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving SaleEventModels by date: {ex.Message}", ex);
            }
        }

        public async Task<List<SaleEventModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetByStatusQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SaleEventModel> result = await connection.QueryAsync<SaleEventModel>(query, new { SaleEventStatus = status });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving SaleEventModels by status: {ex.Message}", ex);
            }
        }

        public async Task<List<SaleEventModel>> GetRelativeAsync(string colName, string input)
        {
            try
            {
                string query = GetRelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%"; // Ensure input is a relative search
                IEnumerable<SaleEventModel> result = await connection.QueryAsync<SaleEventModel>(query, new { Input = input });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving SaleEventModels by relative search: {ex.Message}", ex);
            }
        }
    }
}
