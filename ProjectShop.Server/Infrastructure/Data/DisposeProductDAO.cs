using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DisposeProductDAO : BaseNoneUpdateDAO<DisposeProductModel>, IGetAllByIdAsync<DisposeProductModel>,
                    IGetDataByDateTimeAsync<DisposeProductModel>
    {
        public DisposeProductDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "dispose_product", "dispose_product_id", string.Empty)
        {
        }
        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (product_barcode, location_id, dispose_by_employee_id, 
                        dispose_reason_id, dispose_quantity, disposed_date) 
                      VALUES (@ProductBarcode, @LocationId, @DisposeByEmployeeId, @DisposeReasonId, 
                              @DisposeQuantity, @DisposedDate); SELECT LAST_INSERT_ID();";
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

        public async Task<List<DisposeProductModel>> GetAllByIdAsync(string id, string colIdName = "product_lot_id")
        {
            try
            {
                string query = GetDataQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DisposeProductModel> disposeProducts = await connection.QueryAsync<DisposeProductModel>(query, new { Input = id });
                return disposeProducts.AsList();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Error in GetAllByIdAsync: {ex.Message}", ex);
            }
        }

        public async Task<List<DisposeProductModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "disposed_date")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DisposeProductModel> disposeProducts = await connection.QueryAsync<DisposeProductModel>(query, new { FirstTime = year, SecondTime = month });
                return disposeProducts.AsList();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Error retrieving dispose products by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<DisposeProductModel>> GetAllByYearAsync(int year, string colName = "disposed_date")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DisposeProductModel> disposeProducts = await connection.QueryAsync<DisposeProductModel>(query, new { Input = year });
                return disposeProducts.AsList();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Error retrieving dispose products by year: {ex.Message}", ex);
            }
        }

        public async Task<List<DisposeProductModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "disposed_date")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DisposeProductModel> disposeProducts = await connection.QueryAsync<DisposeProductModel>(query, new { FirstTime = startDate, SecondTime = endDate });
                return disposeProducts.AsList();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Error retrieving dispose products by date range: {ex.Message}", ex);
            }
        }

        public async Task<List<DisposeProductModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "disposed_date")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DisposeProductModel> disposeProducts = await connection.QueryAsync<DisposeProductModel>(query, new { Input = dateTime });
                return disposeProducts.AsList();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Error retrieving dispose products by date: {ex.Message}", ex);
            }
        }
    }
}
