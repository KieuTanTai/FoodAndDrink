using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DetailProductLotDAO : BaseNoneUpdateDAO<DetailProductLotModel>, IDetailProductLotDAO<DetailProductLotModel, DetailProductLotKey>
    {
        public DetailProductLotDAO(
        IDbConnectionFactory connectionFactory,
        IColumnService colService,
        IStringConverter converter,
        IStringChecker checker)
        : base(connectionFactory, colService, converter, checker, "detail_product_lot", "product_lot_id", "product_barcode")
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (product_lot_id, product_barcode, product_lot_mfg_date, product_lot_exp_date, product_lot_initial_quantity) 
                      VALUES (@ProductLotId, @ProductBarcode, @ProductLotMfgDate, @ProductLotExpDate, @ProductLotInitialQuantity); SELECT LAST_INSERT_ID();";
        }

        public async Task<IEnumerable<DetailProductLotModel>> GetByEXPDateAsync(int year, int month, int? maxGetCount = null)
            => await GetByDateTimeAsync("product_lot_exp_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month), maxGetCount);

        public async Task<IEnumerable<DetailProductLotModel>> GetByEXPDateAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("product_lot_exp_date", EQueryTimeType.YEAR, type, year, maxGetCount);
        }

        public async Task<IEnumerable<DetailProductLotModel>> GetByEXPDateAsync(DateTime startDate, DateTime endDate, int? maxGetCount)
            => await GetByDateTimeAsync("product_lot_exp_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate), maxGetCount);


        public async Task<IEnumerable<DetailProductLotModel>> GetByEXPDateAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("product_lot_exp_date", EQueryTimeType.DATE_TIME, type, dateTime, maxGetCount);
        }

        public async Task<IEnumerable<DetailProductLotModel>> GetByInitialQuantityAsync<TEnum>(int initialQuantity, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByInputAsync(initialQuantity.ToString(), type, "product_lot_initial_quantity", maxGetCount);
        }

        // CAN'T WRITE LIKE DRY METHOD BECAUSE ITS CAN'T BE COVER MOST OF THE CASES
        public async Task<DetailProductLotModel> GetByKeysAsync(DetailProductLotKey keys) 
            => await GetSingleByTwoIdAsync(ColumnIdName, SecondColumnIdName, keys.ProductLotId, keys.ProductBarcode);

        public async Task<IEnumerable<DetailProductLotModel>> GetByListKeysAsync(IEnumerable<DetailProductLotKey> keys, int? maxGetCount)
        {
            if (keys == null || !keys.Any())
                throw new ArgumentException("Keys collection cannot be null or empty.");
            string query = "";
            if (maxGetCount.HasValue && maxGetCount.Value > 0)
                query = $@"SELECT * FROM {TableName} WHERE (product_lot_id, product_barcode) IN @Keys LIMIT @MaxGetCount";
            else if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentException("MaxGetCount must be greater than zero.");
            else
                query = $@"SELECT * FROM {TableName} WHERE (product_lot_id, product_barcode) IN @Keys";

            try
            {
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<DetailProductLotModel> results = await connection.QueryAsync<DetailProductLotModel>(query, new { Keys = keys, MaxGetCount = maxGetCount});

                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {query} with parameters {keys}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting list of keys from {TableName}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<DetailProductLotModel>> GetByMFGDateAsync(int year, int month, int? maxGetCount)
            => await GetByDateTimeAsync("product_lot_mfg_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month), maxGetCount);


        public async Task<IEnumerable<DetailProductLotModel>> GetByMFGDateAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("product_lot_exp_date", EQueryTimeType.YEAR, type, year, maxGetCount);
        }

        public async Task<IEnumerable<DetailProductLotModel>> GetByMFGDateAsync(DateTime startDate, DateTime endDate, int? maxGetCount)
            => await GetByDateTimeAsync("product_lot_mfg_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate), maxGetCount);

        public async Task<IEnumerable<DetailProductLotModel>> GetByMFGDateAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("product_lot_mfg_date", EQueryTimeType.DATE_TIME, type, dateTime, maxGetCount);
        }

        public async Task<IEnumerable<DetailProductLotModel>> GetByProductBarcode(string barcode, int? maxGetCount) 
            => await GetByInputAsync(barcode, "product_barcode", maxGetCount);
    }
}
