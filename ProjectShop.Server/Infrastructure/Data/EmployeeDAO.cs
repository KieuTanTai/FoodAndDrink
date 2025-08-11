using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class EmployeeDAO : BaseDAO<EmployeeModel>, IGetByStatusAsync<EmployeeModel>, IGetRelativeAsync<EmployeeModel>, IGetDataByDateTimeAsync<EmployeeModel>
    {
        public EmployeeDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "employee", "employee_id", string.Empty)
        {
        }

        protected override string GetAllQuery()
        {
            return $@"
                SELECT
                    employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                    employee_birthday AS Birthday, employee_phone AS Phone, employee_house_number AS HouseNumber,
                    employee_street AS Street, employee_ward_id AS WardId, employee_district_id AS DistrictId,
                    employee_city_id AS CityId, employee_avatar_url AS AvatarUrl, employee_gender AS Gender,
                    employee_status AS Status
                FROM {TableName}";
        }

        protected override string GetDataQuery(string colIdName)
        {
            CheckColumnName(colIdName);
            string colIdNamePascal = Converter.SnakeCaseToPascalCase(colIdName);
            return $@"
                SELECT
                    employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                    employee_birthday AS Birthday, employee_phone AS Phone, employee_house_number AS HouseNumber,
                    employee_street AS Street, employee_ward_id AS WardId, employee_district_id AS DistrictId,
                    employee_city_id AS CityId, employee_avatar_url AS AvatarUrl, employee_gender AS Gender,
                    employee_status AS Status
                FROM {TableName}
                WHERE {colIdName} = @Input";
        }

        protected override string GetInsertQuery()
        {
            return $@"
                INSERT INTO {TableName} (
                    account_id, employee_name, employee_birthday, employee_phone,
                    employee_house_number, employee_street, employee_ward_id,
                    employee_district_id, employee_city_id, employee_avatar_url,
                    employee_gender, employee_status
                )
                VALUES (
                    @AccountId, @Name, @Birthday, @Phone,
                    @HouseNumber, @Street, @WardId,
                    @DistrictId, @CityId, @AvatarUrl,
                    @Gender, @Status
                ); SELECT LAST_INSERT_ID();";
        }
        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"
                UPDATE {TableName} SET
                    account_id = @AccountId,
                    employee_name = @Name,
                    employee_birthday = @Birthday,
                    employee_phone = @Phone,
                    employee_house_number = @HouseNumber,
                    employee_street = @Street,
                    employee_ward_id = @WardId,
                    employee_district_id = @DistrictId,
                    employee_city_id = @CityId,
                    employee_avatar_url = @AvatarUrl,
                    employee_gender = @Gender,
                    employee_status = @Status
                WHERE {ColumnIdName} = @{colIdName}";
        }

        private string RelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $@"
                SELECT
                    employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                    employee_birthday AS Birthday, employee_phone AS Phone, employee_house_number AS HouseNumber,
                    employee_street AS Street, employee_ward_id AS WardId, employee_district_id AS DistrictId,
                    employee_city_id AS CityId, employee_avatar_url AS AvatarUrl, employee_gender AS Gender,
                    employee_status AS Status
                FROM {TableName} WHERE {colName} LIKE @Input";
        }

        private string GetByMonthAndYear(string colName)
        {
            CheckColumnName(colName);
            return $@"
                    SELECT
                        employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                        employee_birthday AS Birthday, employee_phone AS Phone, employee_house_number AS HouseNumber,
                        employee_street AS Street, employee_ward_id AS WardId, employee_district_id AS DistrictId,
                        employee_city_id AS CityId, employee_avatar_url AS AvatarUrl, employee_gender AS Gender, employee_status AS Status
                    FROM {TableName} WHERE YEAR({colName}) = @FirstTime AND MONTH({colName}) = @SecondTime";
        }

        private string GetByYear(string colName)
        {
            CheckColumnName(colName);
            return $@"
                    SELECT
                        employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                        employee_birthday AS Birthday, employee_phone AS Phone, employee_house_number AS HouseNumber,
                        employee_street AS Street, employee_ward_id AS WardId, employee_district_id AS DistrictId,
                        employee_city_id AS CityId, employee_avatar_url AS AvatarUrl, employee_gender AS Gender, employee_status AS Status
                    FROM {TableName} WHERE Year({colName}) = @Input";
        }

        private string GetByDateTimeRange(string colName)
        {
            CheckColumnName(colName);
            return $@"
                    SELECT
                        employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                        employee_birthday AS Birthday, employee_phone AS Phone, employee_house_number AS HouseNumber,
                        employee_street AS Street, employee_ward_id AS WardId, employee_district_id AS DistrictId,
                        employee_city_id AS CityId, employee_avatar_url AS AvatarUrl, employee_gender AS Gender, employee_status AS Status
                    FROM {TableName} WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        private string GetByDateTime(string colName)
        {
            CheckColumnName(colName);
            return $@"
                    SELECT
                        employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                        employee_birthday AS Birthday, employee_phone AS Phone, employee_house_number AS HouseNumber,
                        employee_street AS Street, employee_ward_id AS WardId, employee_district_id AS DistrictId,
                        employee_city_id AS CityId, employee_avatar_url AS AvatarUrl, employee_gender AS Gender, employee_status AS Status
                    FROM {TableName} WHERE {colName} = DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        public async Task<List<EmployeeModel>> GetRelativeAsync(string input, string colName = "employee_name")
        {
            try
            {
                string query = RelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%"; // Ensure input is a relative search
                IEnumerable<EmployeeModel> employees = await connection.QueryAsync<EmployeeModel>(query, new { Input = input });
                return employees.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting relative employees", ex);
            }
        }

        public async Task<List<EmployeeModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "employee_birthday")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<EmployeeModel> accounts = await connection.QueryAsync<EmployeeModel>(query, new { FirstTime = year, SecondTime = month });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving employees by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<EmployeeModel>> GetAllByYearAsync(int year, string colName = "employee_birthday")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<EmployeeModel> accounts = await connection.QueryAsync<EmployeeModel>(query, new { Input = year });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving employees by year: {ex.Message}", ex);
            }
        }

        public async Task<List<EmployeeModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "employee_birthday")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<EmployeeModel> accounts = await connection.QueryAsync<EmployeeModel>(query, new { FirstTime = startDate, SecondTime = endDate });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving employees by date range: {ex.Message}", ex);
            }
        }

        public async Task<List<EmployeeModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "employee_birthday")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<EmployeeModel> accounts = await connection.QueryAsync<EmployeeModel>(query, new { Input = dateTime });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving employees by date: {ex.Message}", ex);
            }
        }

        public async Task<List<EmployeeModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("employee_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<EmployeeModel> employees = await connection.QueryAsync<EmployeeModel>(query, new { Input = status });
                return employees.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting employees by status", ex);
            }
        }

        public async Task<List<EmployeeModel>> GetAllByGender(bool gender)
        {
            try
            {
                string query = GetDataQuery("employee_gender");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<EmployeeModel> employees = await connection.QueryAsync<EmployeeModel>(query, new { Input = gender });
                return employees.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting employees by gender", ex);
            }
        }
    }
}
