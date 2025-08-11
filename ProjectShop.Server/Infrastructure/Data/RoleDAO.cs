using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class RoleDAO : BaseDAO<RoleModel>, IGetRelativeAsync<RoleModel>, IGetByStatusAsync<RoleModel>, IGetDataByDateTimeAsync<RoleModel>
    {
        public RoleDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "role", "role_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (role_name, role_status, role_created_date, role_last_updated_date) 
                      VALUES (@RoleName, @RoleStatus, @RoleCreatedDate, @RoleUpdatedDate); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET role_name = @RoleName, role_status = @RoleStatus
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        private string GetQueryByDateTime(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} 
                      WHERE {colName} = DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        private string GetQueryByYear(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} 
                      WHERE YEAR({colName}) = @Year";
        }

        private string GetQueryByMonthAndYear(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} 
                      WHERE MONTH({colName}) = @Month AND YEAR({colName}) = @Year";
        }

        private string GetQueryByDateTimeRange(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} 
                      WHERE {colName} >= @StartDate AND {colName} < DATE_ADD(@EndDate, INTERVAL 1 DAY)";
        }

        private string GetRelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} 
                      WHERE {colName} LIKE @Input";
        }

        public async Task<List<RoleModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "role_created_date")
        {
            try
            {
                string query = GetQueryByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<RoleModel> roles = await connection.QueryAsync<RoleModel>(query, new { Input = dateTime });
                return roles.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving roles by date time: {ex.Message}", ex);
            }
        }

        public async Task<List<RoleModel>> GetAllByYearAsync(int year, string colName = "role_created_date")
        {
            try
            {
                string query = GetQueryByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<RoleModel> roles = await connection.QueryAsync<RoleModel>(query, new { Year = year });
                return roles.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving roles by year: {ex.Message}", ex);
            }
        }

        public async Task<List<RoleModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "role_created_date")
        {
            try
            {
                string query = GetQueryByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<RoleModel> roles = await connection.QueryAsync<RoleModel>(query, new { Year = year, Month = month });
                return roles.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving roles by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<RoleModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "role_created_date")
        {
            try
            {
                string query = GetQueryByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<RoleModel> roles = await connection.QueryAsync<RoleModel>(query, new { StartDate = startDate, EndDate = endDate });
                return roles.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving roles by date range: {ex.Message}", ex);
            }
        }

        public async Task<List<RoleModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("role_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<RoleModel> roles = await connection.QueryAsync<RoleModel>(query, new { Status = status });
                return roles.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving roles by status: {ex.Message}", ex);
            }
        }

        public async Task<List<RoleModel>> GetRelativeAsync(string input, string colName = "role_name")
        {
            try
            {
                string query = GetRelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%"; // Ensure input is a wildcard search
                IEnumerable<RoleModel> roles = await connection.QueryAsync<RoleModel>(query, new { Input = input });
                return roles.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving roles relative to input: {ex.Message}", ex);
            }
        }
    }
}
