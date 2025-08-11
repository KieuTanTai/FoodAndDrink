using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class InvoiceDAO : BaseNoneUpdateDAO<InvoiceModel>, IGetDataByDateTimeAsync<InvoiceModel>, IGetByStatusAsync<InvoiceModel>,
                    IGetAllByIdAsync<InvoiceModel>, IGetByRangePriceAsync<InvoiceModel>
    {
        public InvoiceDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "invoice", "invoice_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (customer_id, employee_id, payment_method_id, invoice_total_price, 
                invoice_date, invoice_status) 
                      VALUES (@CustomerId, @EmployeeId, @PaymentMethodId, @InvoiceTotalPrice, @InvoiceDate, @InvoiceStatus); SELECT LAST_INSERT_ID();";
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


        public async Task<List<InvoiceModel>> GetAllByIdAsync(string id, string colIdName = "customer_id")
        {
            try
            {
                string query = GetDataQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InvoiceModel> result = await connection.QueryAsync<InvoiceModel>(query, new { Input = id });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving InvoiceModels by ID: {ex.Message}", ex);
            }
        }

        public async Task<List<InvoiceModel>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice, string colName = "invoice_total_price")
        {
            try
            {
                CheckColumnName(colName);
                string query = $@"SELECT * FROM {TableName} WHERE {colName} BETWEEN @MinPrice AND @MaxPrice";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InvoiceModel> result = await connection.QueryAsync<InvoiceModel>(query, new { MinPrice = minPrice, MaxPrice = maxPrice });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving InvoiceModels by price range: {ex.Message}", ex);
            }
        }

        public async Task<List<InvoiceModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("invoice_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InvoiceModel> result = await connection.QueryAsync<InvoiceModel>(query, new { Input = status });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving InvoiceModels by status: {ex.Message}", ex);
            }
        }

        public async Task<List<InvoiceModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "invoice_date")
        {
            try
            {
                string query = GetByMonthAndYearQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InvoiceModel> result = await connection.QueryAsync<InvoiceModel>(query, new { FirstTime = year, SecondTime = month });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving InvoiceModels by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<InvoiceModel>> GetAllByYearAsync(int year, string colName = "invoice_date")
        {
            try
            {
                string query = GetByYearQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InvoiceModel> result = await connection.QueryAsync<InvoiceModel>(query, new { Input = year });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving InvoiceModels by year: {ex.Message}", ex);
            }
        }

        public async Task<List<InvoiceModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "invoice_date")
        {
            try
            {
                string query = GetByDateTimeRangeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InvoiceModel> result = await connection.QueryAsync<InvoiceModel>(query, new { FirstTime = startDate, SecondTime = endDate });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving InvoiceModels by date range: {ex.Message}", ex);
            }
        }

        public async Task<List<InvoiceModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "invoice_date")
        {
            try
            {
                string query = GetByDateTimeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InvoiceModel> result = await connection.QueryAsync<InvoiceModel>(query, new { Input = dateTime });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving InvoiceModels by date: {ex.Message}", ex);
            }
        }
    }
}
