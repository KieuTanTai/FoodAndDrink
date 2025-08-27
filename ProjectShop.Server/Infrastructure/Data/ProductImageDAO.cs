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
                    IStringConverter converter,
                    ILogService logger)
                    : base(connectionFactory, converter, logger, "product_image", "product_image_id", string.Empty)
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

        public async Task<IEnumerable<ProductImageModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_image_created_date", EQueryTimeType.DATE_TIME, ct, dateTime, maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<ProductImageModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount)
            => await GetByDateTimeAsync("product_image_created_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate), maxGetCount);

        public async Task<IEnumerable<ProductImageModel>> GetByMonthAndYearAsync(int year, int month, int? maxGetCount)
            => await GetByDateTimeAsync("product_image_created_date", EQueryTimeType.MONTH_AND_YEAR, (year, month), maxGetCount);

        public async Task<IEnumerable<ProductImageModel>> GetByYearAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_image_created_date", EQueryTimeType.YEAR, ct, year, maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // -------------------- LastUpdatedDate --------------------

        public async Task<IEnumerable<ProductImageModel>> GetByLastUpdatedDateAsync<TCompareType>(DateTime dateTime, TCompareType compareType, int? maxGetCount) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_image_last_updated_date", EQueryTimeType.DATE_TIME, ct, dateTime, maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<ProductImageModel>> GetByLastUpdatedDateRangeAsync<TCompareType>(DateTime firstTime, DateTime secondTime, TCompareType compareType, int? maxGetCount) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_image_last_updated_date", EQueryTimeType.DATE_TIME_RANGE, ct, (firstTime, secondTime), maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<ProductImageModel>> GetByMonthAndYearLastUpdatedDateAsync(int month, int year, int? maxGetCount)
            => await GetByDateTimeAsync("product_image_last_updated_date", EQueryTimeType.MONTH_AND_YEAR, (year, month), maxGetCount);

        public async Task<IEnumerable<ProductImageModel>> GetByYearLastUpdatedDateAsync<TCompareType>(int year, TCompareType compareType, int? maxGetCount) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("product_image_last_updated_date", EQueryTimeType.YEAR, ct, year, maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // -------------------- ProductBarcode --------------------
        public async Task<IEnumerable<ProductImageModel>> GetByProductBarcodeAsync(string productBarcode, int? maxGetCount)
            => await GetByInputAsync(productBarcode, "product_barcode", maxGetCount);

        public async Task<IEnumerable<ProductImageModel>> GetByProductBarcodesAsync(IEnumerable<string> productBarcodes, int? maxGetCount)
            => await GetByInputsAsync(productBarcodes, "product_barcode", maxGetCount);
    }
}
