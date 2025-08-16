using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class InvoiceDiscountDAO : BaseNoneUpdateDAO<InvoiceDiscountModel>, IInvoiceDiscountDAO<InvoiceDiscountModel, InvoiceDiscountKey>
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

        public async Task<IEnumerable<InvoiceDiscountModel>> GetByInvoiceId(uint invoiceId) => await GetByInputAsync(invoiceId.ToString(), "invoice_id");

        public async Task<InvoiceDiscountModel> GetByKeysAsync(InvoiceDiscountKey keys) => await GetSingleByTwoIdAsync(ColumnIdName, SecondColumnIdName, keys.InvoiceId, keys.SaleEventId);

        public async Task<IEnumerable<InvoiceDiscountModel>> GetByListKeysAsync(IEnumerable<InvoiceDiscountKey> keys)
        {
            try
            {
                if (keys == null || !keys.Any())
                    throw new ArgumentException("Keys collection cannot be null or empty.");
                string query = $@"SELECT * FROM {TableName} WHERE ({ColumnIdName}, {SecondColumnIdName}) IN @Keys";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<InvoiceDiscountModel> results = await connection.QueryAsync<InvoiceDiscountModel>(query, new { Keys = keys });

                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {query} with parameters {keys}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving InvoiceDiscounts by keys: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<InvoiceDiscountModel>> GetBySaleEventId(uint saleEventId) => await GetByInputAsync(saleEventId.ToString(), "sale_event_id");
    }
}
