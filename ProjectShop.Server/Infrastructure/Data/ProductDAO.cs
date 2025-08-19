using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class ProductDAO : BaseDAO<ProductModel>, IProductDAO<ProductModel>
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

        public async Task<IEnumerable<ProductModel>> GetAllByEnumAsync<TEnum>(TEnum tEnum) where TEnum : Enum
        {
            if (tEnum is not EProductUnit unit)
                throw new ArgumentException("Invalid enum type for GetAllByEnumAsync");
            return await GetByInputAsync(unit.ToString(), "product_unit");
        }

        public async Task<IEnumerable<ProductModel>> GetByCategoryIdAsync(uint categoryId) => await GetByInputAsync(categoryId.ToString(), "category_id");

        public async Task<IEnumerable<ProductModel>> GetByCategoryIdsAsync(IEnumerable<uint> categoryIds) => await GetByInputsAsync(categoryIds.Select(categoryId => categoryId.ToString()), "category_id");

        public async Task<IEnumerable<ProductModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid enum type for GetByDateTimeAsync");
            return await GetByDateTimeAsync("product_added_date", EQueryTimeType.DATE_TIME, type, dateTime);
        }

        public async Task<IEnumerable<ProductModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate) => await GetByDateTimeAsync("product_added_date", EQueryTimeType.DATE_TIME, new Tuple<DateTime, DateTime>(startDate, endDate));

        public async Task<ProductModel?> GetByEnumAsync<TEnum>(TEnum tEnum) where TEnum : Enum
        {
            if (tEnum is not EProductUnit unit)
                throw new ArgumentException("Invalid enum type for GetByEnumAsync");
            return await GetSingleDataAsync(unit.ToString(), "product_unit");
        }

        public async Task<IEnumerable<ProductModel>> GetByInputPriceAsync<TEnum>(decimal price, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid enum type for GetByInputPriceAsync");
            return await GetByDecimalAsync(price, type, "product_base_price");
        }

        public async Task<IEnumerable<ProductModel>> GetByLikeStringAsync(string input) => await GetByLikeStringAsync(input, "product_name");

        public async Task<IEnumerable<ProductModel>> GetByLikeStringAsync(string input, int maxGetCount)
            => await GetByLikeStringAsync(input, "product_name", maxGetCount);

        public async Task<IEnumerable<ProductModel>> GetByMonthAndYearAsync(int year, int month) => await GetByDateTimeAsync("product_added_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));

        public async Task<IEnumerable<ProductModel>> GetByNetWeightAsync<TCompareType>(decimal netWeight, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid enum type for GetByNetWeightAsync");
            return await GetByDecimalAsync(netWeight, type, "product_net_weight");
        }

        public async Task<ProductModel?> GetByProductNameAsync(string productName) => await GetSingleDataAsync(productName, "product_name");

        public async Task<IEnumerable<ProductModel>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice) => await GetByRangeDecimalAsync(minPrice, maxPrice, "product_base_price");

        public async Task<IEnumerable<ProductModel>> GetByRatingAgeAsync(string ratingAge) => await GetByInputAsync(ratingAge, "product_rating_age");

        public async Task<IEnumerable<ProductModel>> GetByStatusAsync(bool status) => await GetByInputAsync(GetTinyIntString(status), "product_status");

        public async Task<IEnumerable<ProductModel>> GetByStatusAsync(bool status, int maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "product_status", maxGetCount);

        public async Task<IEnumerable<ProductModel>> GetBySupplierIdAsync(uint supplierId) => await GetByInputAsync(supplierId.ToString(), "supplier_id");

        public async Task<IEnumerable<ProductModel>> GetBySupplierIdsAsync(IEnumerable<uint> supplierIds) => await GetByInputsAsync(supplierIds.Select(supplierId => supplierId.ToString()), "supplier_id");

        public async Task<IEnumerable<ProductModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid enum type for GetByYearAsync");
            return await GetByDateTimeAsync("product_added_date", EQueryTimeType.YEAR, type, year);
        }

        public Task<IEnumerable<ProductModel>> GetByLastUpdatedDateAsync<TCompareType>(DateTime lastUpdatedDate, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid enum type for GetByLastUpdatedDateAsync");
            return GetByDateTimeAsync("product_last_updated_date", EQueryTimeType.DATE_TIME, type, lastUpdatedDate);
        }

        public async Task<IEnumerable<ProductModel>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate) => await GetByDateTimeAsync("product_last_updated_date", EQueryTimeType.DATE_TIME, new Tuple<DateTime, DateTime>(startDate, endDate));

        public Task<IEnumerable<ProductModel>> GetByLastUpdateYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid enum type for GetByLastUpdateYearAsync");
            return GetByDateTimeAsync("product_last_updated_date", EQueryTimeType.YEAR, type, year);
        }

        public Task<IEnumerable<ProductModel>> GetByLastUpdateMonthAndYearAsync(int year, int month)
            => GetByDateTimeAsync("product_last_updated_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));
    }
}
