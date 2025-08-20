using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class ProductImageDAO : BaseDAO<ProductImageModel>, IProductImageDAO<ProductImageModel>
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

        public async Task<IEnumerable<ProductImageModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_image_created_date", EQueryTimeType.DATE_TIME, ct, dateTime);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<ProductImageModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("product_image_created_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate));

        public async Task<IEnumerable<ProductImageModel>> GetByMonthAndYearAsync(int year, int month)
            => await GetByDateTimeAsync("product_image_created_date", EQueryTimeType.MONTH_AND_YEAR, (year, month));

        public async Task<IEnumerable<ProductImageModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_image_created_date", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // -------------------- LastUpdatedDate --------------------

        public async Task<IEnumerable<ProductImageModel>> GetByLastUpdatedDateAsync<TCompareType>(DateTime dateTime, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_image_last_updated_date", EQueryTimeType.DATE_TIME, ct, dateTime);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<ProductImageModel>> GetByRangeLastUpdatedDateAsync<TCompareType>(DateTime firstTime, DateTime secondTime, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_image_last_updated_date", EQueryTimeType.DATE_TIME_RANGE, ct, (firstTime, secondTime));
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<ProductImageModel>> GetByLastUpdatedMonthAndYearAsync(int month, int year)
            => await GetByDateTimeAsync("product_image_last_updated_date", EQueryTimeType.MONTH_AND_YEAR, (year, month));

        public async Task<IEnumerable<ProductImageModel>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_image_last_updated_date", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // -------------------- ProductBarcode --------------------
        public async Task<IEnumerable<ProductImageModel>> GetByProductBarcodeAsync(string productBarcode)
            => await GetByInputAsync(productBarcode, "product_barcode");
    }
}
