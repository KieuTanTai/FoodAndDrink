using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class CustomerDAO : BaseDAO<CustomerModel>, IGetByStatusAsync<CustomerModel>, IGetRelativeAsync<CustomerModel>, IGetDataByDateTimeAsync<CustomerModel>
    {
        public CustomerDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "customer", "customer_id", string.Empty)
        {
        }

        protected override string GetAllQuery()
        {
            return $@"
                SELECT
                    customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                    customer_birthday AS Birthday, customer_phone AS Phone, customer_email AS Email,
                    customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                    customer_status AS Status
                FROM {TableName}";
        }

        protected override string GetDataQuery(string colIdName)
        {
            CheckColumnName(colIdName);
            return $@"
                SELECT
                    customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                    customer_birthday AS Birthday, customer_phone AS Phone, customer_email AS Email,
                    customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                    customer_status AS Status
                FROM {TableName}
                WHERE {colIdName} = @Input";
        }

        protected override string GetInsertQuery()
        {
            return $@"
                INSERT INTO {TableName} (
                    account_id, customer_name, customer_birthday, customer_phone,
                    customer_email, customer_avatar_url,
                    customer_gender, customer_status
                )
                VALUES (
                    @AccountId, @Name, @Birthday, @Phone,
                    @Email, @AvatarUrl,
                    @Gender, @Status
                ); SELECT LAST_INSERT_ID();";
        }
        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"
                UPDATE {TableName} SET
                    account_id = @AccountId,
                    customer_name = @Name,
                    customer_birthday = @Birthday,
                    customer_phone = @Phone,
                    customer_email = @Email,
                    customer_avatar_url = @AvatarUrl,
                    customer_gender = @Gender,
                    customer_status = @Status
                WHERE {ColumnIdName} = @{colIdName}";
        }

        private string RelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $@"
                SELECT
                    customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                    customer_birthday AS Birthday, customer_phone AS Phone, 
                    customer_email AS Email, customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                    customer_status AS Status
                FROM {TableName} WHERE {colName} LIKE @Input";
        }

        private string GetByMonthAndYear(string colName)
        {
            CheckColumnName(colName);
            return $@"
                    SELECT
                        customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                        customer_birthday AS Birthday, customer_phone AS Phone, 
                        customer_email AS Email, customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                        customer_status AS Status
                    FROM {TableName} WHERE YEAR({colName}) = @FirstTime AND MONTH({colName}) = @SecondTime";
        }

        private string GetByYear(string colName)
        {
            CheckColumnName(colName);
            return $@"
                    SELECT
                        customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                        customer_birthday AS Birthday, customer_phone AS Phone, 
                        customer_email AS Email, customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                        customer_status AS Status
                    FROM {TableName} WHERE Year({colName}) = @Input";
        }

        private string GetByDateTimeRange(string colName)
        {
            CheckColumnName(colName);
            return $@"
                    SELECT
                        customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                        customer_birthday AS Birthday, customer_phone AS Phone, 
                        customer_email AS Email, customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                        customer_status AS Status
                    FROM {TableName} WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        private string GetByDateTime(string colName)
        {
            CheckColumnName(colName);
            return $@"
                    SELECT
                        customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                        customer_birthday AS Birthday, customer_phone AS Phone, 
                        customer_email AS Email, customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                        customer_status AS Status
                    FROM {TableName} WHERE {colName} = DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        public async Task<List<CustomerModel>> GetRelativeAsync(string input, string colName)
        {
            try
            {
                string query = RelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%"; // Ensure input is a relative search
                IEnumerable<CustomerModel> customers = await connection.QueryAsync<CustomerModel>(query, new { Input = input });
                return customers.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting relative customers", ex);
            }
        }

        public async Task<List<CustomerModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "customer_birthday")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<CustomerModel> accounts = await connection.QueryAsync<CustomerModel>(query, new { FirstTime = year, SecondTime = month });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving customers by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<CustomerModel>> GetAllByYearAsync(int year, string colName = "customer_birthday")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<CustomerModel> accounts = await connection.QueryAsync<CustomerModel>(query, new { Input = year });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving customers by year: {ex.Message}", ex);
            }
        }

        public async Task<List<CustomerModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "customer_birthday")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<CustomerModel> accounts = await connection.QueryAsync<CustomerModel>(query, new { FirstTime = startDate, SecondTime = endDate });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving customers by date range: {ex.Message}", ex);
            }
        }

        public async Task<List<CustomerModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "customer_birthday")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<CustomerModel> accounts = await connection.QueryAsync<CustomerModel>(query, new { Input = dateTime });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving customers by date: {ex.Message}", ex);
            }
        }

        public async Task<List<CustomerModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("customer_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<CustomerModel> customers = await connection.QueryAsync<CustomerModel>(query, new { Input = status });
                return customers.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting customers by status", ex);
            }
        }

        public async Task<List<CustomerModel>> GetAllByGender(bool gender)
        {
            try
            {
                string query = GetDataQuery("customer_gender");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<CustomerModel> customers = await connection.QueryAsync<CustomerModel>(query, new { Input = gender });
                return customers.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting customers by gender", ex);
            }
        }
    }
}
