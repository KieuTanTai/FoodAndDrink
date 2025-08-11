using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class SaleEventImageDAO : BaseDAO<SaleEventImageModel>, IGetAllByIdAsync<SaleEventImageModel>, IGetDataByDateTimeAsync<SaleEventImageModel>
    {
        public SaleEventImageDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "sale_event_image", "sale_event_image_id", string.Empty)
        {
        }
        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (sale_event_id, sale_event_image_url, sale_event_image_priority) 
                      VALUES (@SaleEventId, @SaleEventImageUrl, @SaleEventImagePriority); 
                      SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName); 
            return $@"UPDATE {TableName} 
                      SET sale_event_image_url = @SaleEventImageUrl, 
                          sale_event_image_priority = @SaleEventImagePriority 
                      WHERE {ColumnIdName} = @{colIdName};";
        }

        private string GetDataByDateTime(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} = DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        private string GetDataByYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE Year({colName}) = @Input";
        }

        private string GetDataByMonthAndYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE YEAR({colName}) = @FirstTime AND MONTH({colName}) = @SecondTime";
        }

        private string GetDataByDateTimeRange(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        public async Task<List<SaleEventImageModel>> GetAllByIdAsync(string id, string colIdName = "sale_event_id")
        {
            try
            {
                string query = GetDataQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SaleEventImageModel> saleEventImages = await connection.QueryAsync<SaleEventImageModel>(query, new { Input = id });
                return saleEventImages.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching Sale Event Images by ID: {ex.Message}", ex);
            }
        }

        public async Task<List<SaleEventImageModel>> GetAllByDateTimeAsync(DateTime input, string colName = "sale_event_image_created_date")
        {
            try
            {
                string query = GetDataByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SaleEventImageModel> saleEventImages = await connection.QueryAsync<SaleEventImageModel>(query, new { Input = input });
                return saleEventImages.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching Sale Event Images by DateTime: {ex.Message}", ex);
            }
        }

        public async Task<List<SaleEventImageModel>> GetAllByYearAsync(int year, string colName = "sale_event_image_created_date")
        {
            try
            {
                string query = GetDataByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SaleEventImageModel> saleEventImages = await connection.QueryAsync<SaleEventImageModel>(query, new { Input = year });
                return saleEventImages.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching Sale Event Images by Year: {ex.Message}", ex);
            }
        }

        public async Task<List<SaleEventImageModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "sale_event_image_created_date")
        {
            try
            {
                string query = GetDataByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SaleEventImageModel> saleEventImages = await connection.QueryAsync<SaleEventImageModel>(query, new { FirstTime = year, SecondTime = month });
                return saleEventImages.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching Sale Event Images by Month and Year: {ex.Message}", ex);
            }
        }

        public async Task<List<SaleEventImageModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "sale_event_image_created_date")
        {
            try
            {
                string query = GetDataByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<SaleEventImageModel> saleEventImages = await connection.QueryAsync<SaleEventImageModel>(query, new { FirstTime = startDate, SecondTime = endDate });
                return saleEventImages.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching Sale Event Images by DateTime Range: {ex.Message}", ex);
            }
        }
    }
}
