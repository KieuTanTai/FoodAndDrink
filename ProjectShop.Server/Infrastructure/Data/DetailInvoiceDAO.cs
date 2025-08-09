using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DetailInvoiceDAO : BaseDAO<DetailInvoiceModel>, IGetByStatusAsync<DetailInvoiceModel>, IGetAllByIdAsync<DetailInvoiceModel>, IGetByRangePriceAsync<DetailInvoiceModel>
    {
        public DetailInvoiceDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "detail_invoice", "detail_invoice_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (invoice_id, product_barcode, detail_invoice_quantity, detail_invoice_price, detail_invoice_status) 
                      VALUES (@InvoiceId, @ProductBarcode, @DetailInvoiceQuantity, @DetailInvoicePrice, @DetailInvoiceStatus); SELECT LAST_INSERT_ID();";
        }
        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET detail_invoice_status = @DetailInvoiceStatus 
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        private string GetAllByIdQuery(string colIdName)
        {
            CheckColumnName(colIdName);
            return $"SELECT * FROM {TableName} WHERE {colIdName} = @Input";
        }

        public async Task<List<DetailInvoiceModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = $@"SELECT * FROM {TableName} 
                                 WHERE detail_invoice_status = @Status";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailInvoiceModel> result = await connection.QueryAsync<DetailInvoiceModel>(query, new { Status = status });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving DetailInvoiceModels by status: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailInvoiceModel>> GetByRangePrice(decimal minPrice, decimal maxPrice)
        {
            try
            {
                string query = $@"SELECT * FROM {TableName} 
                                 WHERE detail_invoice_price BETWEEN @MinPrice AND @MaxPrice";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailInvoiceModel> result = await connection.QueryAsync<DetailInvoiceModel>(query, new { MinPrice = minPrice, MaxPrice = maxPrice });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving DetailInvoiceModels by price range: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailInvoiceModel>?> GetAllByIdAsync(string id, string colIdName)
        {
            try
            {
                string query = GetAllByIdQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailInvoiceModel> result = await connection.QueryAsync<DetailInvoiceModel>(query, new { Input = id });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving DetailInvoiceModels by ID: {ex.Message}", ex);
            }
        }
    }
}
