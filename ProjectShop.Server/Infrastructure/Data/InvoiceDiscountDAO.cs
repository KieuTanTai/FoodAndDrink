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
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "invoice_discount", "invoice_id", "sale_event_id")
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (invoice_id, sale_event_id) 
                      VALUES (@InvoiceId, @SaleEventId); SELECT LAST_INSERT_ID();";
        }

        public async Task<IEnumerable<InvoiceDiscountModel>> GetByInvoiceId(uint invoiceId, int? maxGetCount) 
            => await GetByInputAsync(invoiceId.ToString(), "invoice_id", maxGetCount);

        public async Task<InvoiceDiscountModel?> GetByKeysAsync(InvoiceDiscountKey keys) 
            => await GetSingleByTwoIdAsync(ColumnIdName, SecondColumnIdName, keys.InvoiceId, keys.SaleEventId);

        public async Task<IEnumerable<InvoiceDiscountModel>> GetByListKeysAsync(IEnumerable<InvoiceDiscountKey> keys, int? maxGetCount)
        {
            if (keys == null || !keys.Any())
                throw new ArgumentException("Keys collection cannot be null or empty.");
            string query = $@"";
            if (maxGetCount.HasValue && maxGetCount.Value > 0)
                query = $@"SELECT * FROM {TableName} 
                          WHERE (invoice_id, sale_event_id) IN @Keys 
                          LIMIT @MaxGetCount";
            else if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentOutOfRangeException(query, "maxGetCount must be greater than 0.");
            else
                query = $"SELECT * FROM {TableName} WHERE ({ColumnIdName}, {SecondColumnIdName}) IN @Keys";

            try
            {
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<InvoiceDiscountModel> results = await connection.QueryAsync<InvoiceDiscountModel>(query, new { Keys = keys, MaxGetCount = maxGetCount });
                Logger.LogInfo<IEnumerable<InvoiceDiscountModel>, InvoiceDiscountDAO>($"Retrieved InvoiceDiscounts by keys successfully.");
                return results;
            }
            catch (Exception ex)
            {
                Logger.LogError<IEnumerable<InvoiceDiscountModel>, InvoiceDiscountDAO>($"Error retrieving InvoiceDiscounts by keys: {ex.Message}", ex); 
                throw new Exception($"Error retrieving InvoiceDiscounts by keys: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<InvoiceDiscountModel>> GetBySaleEventId(uint saleEventId, int? maxGetCount) 
            => await GetByInputAsync(saleEventId.ToString(), "sale_event_id", maxGetCount);
    }
}
