using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class ProductLotDAO : BaseNoneUpdateDAO<ProductLotModel>, IProductLotDAO<ProductLotModel>
    {
        public ProductLotDAO(
        IDbConnectionFactory connectionFactory,
        IColumnService colService,
        IStringConverter converter,
        IStringChecker checker)
        : base(connectionFactory, colService, converter, checker, "product_lot", "product_lot_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (inventory_id, product_lot_created_date) 
                      VALUES (@InventoryId, @ProductLotCreatedDate); SELECT LAST_INSERT_ID();";
        }

        public async Task<IEnumerable<ProductLotModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_lot_created_date", EQueryTimeType.DATE_TIME, ct, dateTime);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<ProductLotModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate) => await GetByDateTimeAsync("product_lot_created_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate));

        public async Task<IEnumerable<ProductLotModel>> GetByMonthAndYearAsync(int year, int month) => await GetByDateTimeAsync("product_lot_created_date", EQueryTimeType.MONTH_AND_YEAR, (year, month));

        public async Task<IEnumerable<ProductLotModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_lot_created_date", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<ProductLotModel>> GetByProductBarcodeAsync(string barcode) => await GetByInputAsync(barcode, "product_barcode");
    }
}
