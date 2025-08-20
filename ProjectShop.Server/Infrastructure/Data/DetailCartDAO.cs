using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DetailCartDAO : BaseDAO<DetailCartModel>, IDetailCartDAO<DetailCartModel>
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

        public async Task<IEnumerable<DetailCartModel>> GetByMonthAndYearAsync(int year, int month)
                    => await GetByDateTimeAsync("detail_cart_added_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));

        public async Task<IEnumerable<DetailCartModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("detail_cart_added_date", EQueryTimeType.YEAR, type, year);
        }

        public async Task<IEnumerable<DetailCartModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("detail_cart_added_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate));

        public async Task<IEnumerable<DetailCartModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("detail_cart_added_date", EQueryTimeType.DATE_TIME, type, dateTime);
        }

        public async Task<IEnumerable<DetailCartModel>> GetByInputPriceAsync<TEnum>(decimal price, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDecimalAsync(price, compareType, "detail_cart_price");
        }

        public async Task<IEnumerable<DetailCartModel>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice) => await GetByRangeDecimalAsync(minPrice, maxPrice, "detail_cart_price");

        public async Task<IEnumerable<DetailCartModel>> GetByCartId(uint cartId) => await GetByInputAsync(cartId.ToString(), "cart_id");

        public async Task<IEnumerable<DetailCartModel>> GetByProductBarcode(string barcode) => await GetByInputAsync(barcode, "product_barcode");
    }
}
