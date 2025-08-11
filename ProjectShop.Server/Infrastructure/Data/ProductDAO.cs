using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class ProductDAO : BaseDAO<ProductModel>, IGetByStatusAsync<ProductModel>, IGetAllByIdAsync<ProductModel>, IGetByEnumAsync<ProductModel>, IGetRelativeAsync<ProductModel>, IGetDataByDateTimeAsync<ProductModel>
    {
        public ProductDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "product", "product_barcode", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            // add field `product_added_date` if needed
            return $@"INSERT INTO {TableName} (
                product_barcode,
                supplier_id,
                product_name,
                product_net_weight,
                product_weight_range,
                product_unit,
                product_base_price,
                product_rating_age,
                product_status
            ) VALUES (
                @ProductBarcode,
                @SupplierId,
                @ProductName,
                @ProductNetWeight,
                @ProductWeightRange,
                @ProductUnit,
                @ProductBasePrice,
                @ProductRatingAge,
                @ProductStatus
            ); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET product_name = @ProductName, 
                          product_net_weight = @ProductNetWeight, 
                          product_weight_range = @ProductWeightRange, 
                          product_unit = @ProductUnit, 
                          product_base_price = @ProductBasePrice, 
                          product_rating_age = @ProductRatingAge, 
                          product_status = @ProductStatus 
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        private string GetRelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} LIKE @Input";
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

        public async Task<List<ProductModel>> GetAllByDateTimeRangeAsync(DateTime firstTime, DateTime secondTime, string colName = "product_added_date")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductModel> products = await connection.QueryAsync<ProductModel>(query, new { FirstTime = firstTime, SecondTime = secondTime });
                return products.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductDAO)}.{nameof(GetAllByDateTimeRangeAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductModel>> GetAllByDateTimeAsync(DateTime input, string colName = "product_added_date")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductModel> products = await connection.QueryAsync<ProductModel>(query, new { Input = input });
                return products.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductDAO)}.{nameof(GetAllByDateTimeAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductModel>> GetAllByYearAsync(int year, string colName = "product_added_date")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductModel> products = await connection.QueryAsync<ProductModel>(query, new { Input = year });
                return products.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductDAO)}.{nameof(GetAllByYearAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "product_added_date")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductModel> products = await connection.QueryAsync<ProductModel>(query, new { Year = year, Month = month });
                return products.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductDAO)}.{nameof(GetAllByMonthAndYearAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductModel>> GetAllByEnumAsync<TEnum>(TEnum tEnum, string colName) where TEnum : Enum
        {
            try
            {
                if (tEnum is not EProductUnit)
                    throw new ArgumentException($"Invalid enum type: {typeof(TEnum).Name}. Expected EProductUnit.");
                string query = GetDataQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductModel> products = await connection.QueryAsync<ProductModel>(query, new { Input = tEnum });
                return products.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductDAO)}.{nameof(GetAllByEnumAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<ProductModel> GetByEnumAsync<TEnum>(TEnum tEnum, string colName) where TEnum : Enum
        {
            try
            {
                if (tEnum is not EProductUnit)
                    throw new ArgumentException($"Invalid enum type: {typeof(TEnum).Name}. Expected EProductUnit.");
                string query = GetDataQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                ProductModel? product = await connection.QueryFirstOrDefaultAsync<ProductModel>(query, new { Input = tEnum });
                if (product == null)
                    throw new KeyNotFoundException($"No product found with {colName} = {tEnum}");
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductDAO)}.{nameof(GetByEnumAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("product_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductModel> products = await connection.QueryAsync<ProductModel>(query, new { Input = status });
                return products.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductDAO)}.{nameof(GetAllByStatusAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductModel>> GetAllByIdAsync(string id, string colName = "supplier_id")
        {
            try
            {
                string query = GetDataQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductModel> products = await connection.QueryAsync<ProductModel>(query, new { Input = id });
                return products.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductDAO)}.{nameof(GetAllByIdAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductModel>> GetRelativeAsync(string input, string colName = "product_name")
        {
            try
            {
                string query = GetRelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%"; // Ensure input is formatted for LIKE query
                IEnumerable<ProductModel> products = await connection.QueryAsync<ProductModel>(query, new { Input = input });
                return products.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductDAO)}.{nameof(GetRelativeAsync)}: {ex.Message}", ex);
            }
        }
    }
}
