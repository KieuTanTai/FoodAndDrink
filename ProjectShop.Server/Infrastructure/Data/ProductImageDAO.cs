using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class ProductImageDAO : BaseDAO<ProductImageModel>, IGetAllByIdAsync<ProductImageModel>, IGetDataByDateTimeAsync<ProductImageModel>
    {
        public ProductImageDAO(
                    IDbConnectionFactory connectionFactory,
                    IColumnService colService,
                    IStringConverter converter,
                    IStringChecker checker)
                    : base(connectionFactory, colService, converter, checker, "product_image", "product_image_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (product_barcode, product_image_url, product_image_priority, product_image_created_date) 
                      VALUES (@ProductId, @ProductImageUrl, @ProductImagePriority, @ProductImageCreateDate); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET product_image_url = @ProductImageUrl, 
                          product_image_priority = @ProductImagePriority
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        private string GetByDateTime(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} = DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        private string GetByDateTimeRange(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        private string GetByYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE Year({colName}) = @Input";
        }

        private string GetByMonthAndYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE YEAR({colName}) = @FirstTime AND MONTH({colName}) = @SecondTime";
        }

        public async Task<List<ProductImageModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "product_image_created_date")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductImageModel> productImages = await connection.QueryAsync<ProductImageModel>(query, new { Input = dateTime });
                return productImages.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductImageDAO)}.{nameof(GetAllByDateTimeAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductImageModel>> GetAllByDateTimeRangeAsync(DateTime firstTime, DateTime secondTime, string colName = "product_image_created_date")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductImageModel> productImages = await connection.QueryAsync<ProductImageModel>(query, new { FirstTime = firstTime, SecondTime = secondTime });
                return productImages.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductImageDAO)}.{nameof(GetAllByDateTimeRangeAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductImageModel>> GetAllByYearAsync(int year, string colName = "product_image_created_date")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductImageModel> productImages = await connection.QueryAsync<ProductImageModel>(query, new { Input = year });
                return productImages.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductImageDAO)}.{nameof(GetAllByYearAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductImageModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "product_image_created_date")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductImageModel> productImages = await connection.QueryAsync<ProductImageModel>(query, new { FirstTime = year, SecondTime = month });
                return productImages.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductImageDAO)}.{nameof(GetAllByMonthAndYearAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductImageModel>> GetAllByIdAsync(string id, string colIdName)
        {
            try
            {
                string query = GetDataQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductImageModel> productImages = await connection.QueryAsync<ProductImageModel>(query, new { Input = id });
                return productImages.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductImageDAO)}.{nameof(GetAllByIdAsync)}: {ex.Message}", ex);
            }
        }
    }
}
