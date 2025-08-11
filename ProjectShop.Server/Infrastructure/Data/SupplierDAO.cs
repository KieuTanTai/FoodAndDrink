using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class SupplierDAO : BaseNoneUpdateDAO<SupplierModel>, IGetRelativeAsync<SupplierModel>, IGetByStatusAsync<SupplierModel>, IGetDataByDateTimeAsync<SupplierModel>
    {
        public SupplierDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "supplier", "supplier_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            // add field `supplier_cooperation_date` if needed
            return $@"INSERT INTO {TableName} (
                `supplier_name`,
                `supplier_phone`,
                `supplier_email`,
                `supplier_company_house_number`,
                `supplier_company_street`,
                `supplier_company_ward_id`,
                `supplier_company_district_id`,
                `supplier_company_city_id`,
                `supplier_store_house_number`,
                `supplier_store_street`,
                `supplier_store_ward_id`,
                `supplier_store_district_id`,
                `supplier_store_city_id`,
                `supplier_status`
            ) VALUES (
                @SupplierName,
                @SupplierPhone,
                @SupplierEmail,
                @SupplierCompanyHouseNumber,
                @SupplierCompanyStreet,
                @SupplierCompanyWardId,
                @SupplierCompanyDistrictId,
                @SupplierCompanyCityId,
                @SupplierStoreHouseNumber,
                @SupplierStoreStreet,
                @SupplierStoreWardId,
                @SupplierStoreDistrictId,
                @SupplierStoreCityId,
                @SupplierStatus
            ); SELECT LAST_INSERT_ID();";
        }

        private string GetByStatusQuery()
        {
            return $@"SELECT * FROM {TableName} 
                      WHERE supplier_status = @SupplierStatus";
        }

        private string GetRelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} 
                      WHERE {colName} LIKE @Input";
        }

        private string GetByDateTimeRange(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} 
                      WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        private string GetByDateTime(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} 
                      WHERE {colName} = DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        private string GetByYear(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} 
                      WHERE YEAR({colName}) = @Input";
        }

        private string GetByMonthAndYear(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} 
                      WHERE YEAR({colName}) = @Year AND MONTH({colName}) = @Month";
        }

        public async Task<List<SupplierModel>> GetAllByDateTimeAsync(DateTime input, string colName = "supplier_cooperation_date")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SupplierModel> result = await connection.QueryAsync<SupplierModel>(query, new { Input = input });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving SupplierModels by date: {ex.Message}", ex);
            }
        }

        public async Task<List<SupplierModel>> GetAllByDateTimeRangeAsync(DateTime firstTime, DateTime secondTime, string colName = "supplier_cooperation_date")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SupplierModel> result = await connection.QueryAsync<SupplierModel>(query, new { FirstTime = firstTime, SecondTime = secondTime });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving SupplierModels by date range: {ex.Message}", ex);
            }
        }

        public async Task<List<SupplierModel>> GetAllByYearAsync(int year, string colName = "supplier_cooperation_date")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SupplierModel> result = await connection.QueryAsync<SupplierModel>(query, new { Input = year });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving SupplierModels by year: {ex.Message}", ex);
            }
        }

        public async Task<List<SupplierModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "supplier_cooperation_date")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SupplierModel> result = await connection.QueryAsync<SupplierModel>(query, new { Year = year, Month = month });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving SupplierModels by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<SupplierModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetByStatusQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SupplierModel> result = await connection.QueryAsync<SupplierModel>(query, new { SupplierStatus = status });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving SupplierModels by status: {ex.Message}", ex);
            }
        }

        public async Task<List<SupplierModel>> GetRelativeAsync(string input, string colName = "supplier_name")
        {
            try
            {
                string query = GetRelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SupplierModel> result = await connection.QueryAsync<SupplierModel>(query, new { Input = $"%{input}%" });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving SupplierModels by relative input: {ex.Message}", ex);
            }
        }
    }
}
