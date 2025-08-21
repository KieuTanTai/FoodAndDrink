using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DetailInvoiceDAO : BaseNoneUpdateDAO<DetailInvoiceModel>, IDetailInvoiceDAO<DetailInvoiceModel>
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

        public async Task<IEnumerable<DetailInvoiceModel>> GetByStatusAsync(bool status, int? maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "detail_invoice_status", maxGetCount);

        public async Task<IEnumerable<DetailInvoiceModel>> GetByInputPriceAsync<TEnum>(decimal price, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDecimalAsync(price, type, "detail_invoice_price", maxGetCount);
        }

        public async Task<IEnumerable<DetailInvoiceModel>> GetByInvoiceIdAsync(uint invoiceId, int? maxGetCount) 
            => await GetByInputAsync(invoiceId.ToString(), "invoice_id", maxGetCount);

        public async Task<IEnumerable<DetailInvoiceModel>> GetByProductBarcodeAsync(string barcode, int? maxGetCount) 
            => await GetByInputAsync(barcode, "product_barcode", maxGetCount);

        public async Task<IEnumerable<DetailInvoiceModel>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice, int? maxGetCount) 
            => await GetByRangeDecimalAsync(minPrice, maxPrice, "detail_invoice_price", maxGetCount);

        public async Task<IEnumerable<DetailInvoiceModel>> GetByQuantityAsync<TEnum>(int quantity, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByInputAsync(quantity.ToString(), type, "detail_invoice_quantity", maxGetCount);
        }
    }
}
