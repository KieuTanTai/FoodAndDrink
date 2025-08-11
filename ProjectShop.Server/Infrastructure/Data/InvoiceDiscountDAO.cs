using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class InvoiceDiscountDAO : BaseNoneUpdateDAO<InvoiceDiscountModel>, IGetAllByIdAsync<InvoiceDiscountModel>,
                    IGetByKeysAsync<InvoiceDiscountModel, InvoiceDiscountKey>
    {
        public InvoiceDiscountDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "invoice_discount", "invoice_id", "sale_event_id")
        {
        }
        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (invoice_id, sale_event_id) 
                      VALUES (@InvoiceId, @SaleEventId); SELECT LAST_INSERT_ID();";
        }

        private string GetByIdsQuery()
        {
            string colIdName1 = Converter.SnakeCaseToPascalCase(ColumnIdName);
            string colIdName2 = Converter.SnakeCaseToPascalCase(SecondColumnIdName);
            return $@"SELECT * FROM {TableName} 
                      WHERE {ColumnIdName} = @{colIdName1} AND {SecondColumnIdName} = @{colIdName2}";
        }

        public async Task<List<InvoiceDiscountModel>> GetAllByIdAsync(string id, string colIdName = "sale_event_id")
        {
            try
            {
                string query = GetDataQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InvoiceDiscountModel> result = await connection.QueryAsync<InvoiceDiscountModel>(query, new { Input = id });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving InvoiceDiscountModels by ID: {ex.Message}", ex);
            }
        }

        public async Task<InvoiceDiscountModel> GetByKeysAsync(InvoiceDiscountKey keys)
        {
            try
            {
                string query = GetByIdsQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                InvoiceDiscountModel? result = await connection.QueryFirstOrDefaultAsync<InvoiceDiscountModel>(query, keys);

                if (result == null)
                    throw new KeyNotFoundException($"No InvoiceDiscount found for InvoiceId: {keys.InvoiceId} and SaleEventId: {keys.SaleEventId}");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(InvoiceDiscountDAO)}.{nameof(GetByKeysAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<InvoiceDiscountModel>> GetByListKeysAsync(IEnumerable<InvoiceDiscountKey> keys)
        {
            try
            {
                string query = GetByIdsQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InvoiceDiscountModel> result = await connection.QueryAsync<InvoiceDiscountModel>(query, keys);
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(InvoiceDiscountDAO)}.{nameof(GetByListKeysAsync)}: {ex.Message}", ex);
            }
        }
    }
}
