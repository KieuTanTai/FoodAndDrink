using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DetailCartDAO : BaseDAO<DetailCartModel>, IGetDataByDateTimeAsync<DetailCartModel>, IGetAllByIdAsync<DetailCartModel>, IGetByRangePriceAsync<DetailCartModel>
    {
        public DetailCartDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "detail_cart", "detail_cart_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (cart_id, product_barcode, detail_cart_quantity, detail_cart_price, detail_cart_added_date) 
                      VALUES (@CartId, @ProductBarcode, @DetailCartQuantity, @DetailCartPrice, @DetailCartAddedDate); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET detail_cart_quantity = @DetailCartQuantity, detail_cart_price = @DetailCartPrice, 
                        detail_cart_added_date = @DetailCartAddedDate 
                      WHERE {ColumnIdName} = @{colIdName}";
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

        public async Task<List<DetailCartModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "detail_cart_added_date")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailCartModel> accounts = await connection.QueryAsync<DetailCartModel>(query, new { FirstTime = year, SecondTime = month });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving detail carts by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailCartModel>> GetAllByYearAsync(int year, string colName = "detail_cart_added_date")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailCartModel> accounts = await connection.QueryAsync<DetailCartModel>(query, new { Input = year });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving detail carts by year: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailCartModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "detail_cart_added_date")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailCartModel> accounts = await connection.QueryAsync<DetailCartModel>(query, new { FirstTime = startDate, SecondTime = endDate });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving detail carts by date range: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailCartModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "detail_cart_added_date")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailCartModel> accounts = await connection.QueryAsync<DetailCartModel>(query, new { Input = dateTime });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving detail carts by date: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailCartModel>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice, string colName = "detail_cart_price")
        {
            try
            {
                CheckColumnName(colName);
                string query = $"SELECT * FROM {TableName} WHERE {colName} BETWEEN @MinPrice AND @MaxPrice";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailCartModel> detailCarts = await connection.QueryAsync<DetailCartModel>(query, new { MinPrice = minPrice, MaxPrice = maxPrice });
                return detailCarts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving detail carts by price range: {ex.Message}", ex);
            }
        }

        //public async Task<List<DetailCartModel>> GetByCartQuantityAsync(int quantity)
        //{
        //    try
        //    {
        //        string query = GetDataQuery("detail_cart_quantity");
        //        using IDbConnection connection = ConnectionFactory.CreateConnection();
        //        IEnumerable<DetailCartModel> detailCarts = await connection.QueryAsync<DetailCartModel>(query, new { Input = quantity });
        //        return detailCarts.AsList();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception (log it, rethrow it, etc.)
        //        throw new Exception($"Error retrieving detail carts by quantity: {ex.Message}", ex);
        //    }
        //}

        public async Task<List<DetailCartModel>> GetAllByIdAsync(string id, string colIdName)
        {
            try
            {
                string query = GetDataQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailCartModel> detailCarts = await connection.QueryAsync<DetailCartModel>(query, new { Input = id });
                return detailCarts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving detail carts by ID: {ex.Message}", ex);
            }
        }
    }
}
