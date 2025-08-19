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

        public async Task<IEnumerable<DetailProductLotModel>> GetByEXPDateAsync(int year, int month)
            => await GetByDateTimeAsync("product_lot_exp_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));

        public async Task<IEnumerable<DetailProductLotModel>> GetByEXPDateAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("product_lot_exp_date", EQueryTimeType.YEAR, type, year);
        }

        public async Task<IEnumerable<DetailProductLotModel>> GetByEXPDateAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("product_lot_exp_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate));


        public async Task<IEnumerable<DetailProductLotModel>> GetByEXPDateAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("product_lot_exp_date", EQueryTimeType.DATE_TIME, type, dateTime);
        }

        public async Task<IEnumerable<DetailProductLotModel>> GetByInitialQuantityAsync<TEnum>(int initialQuantity, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByInputAsync(initialQuantity.ToString(), type, "product_lot_initial_quantity");
        }

        // CAN'T WRITE LIKE DRY METHOD BECAUSE ITS CAN'T BE COVER MOST OF THE CASES
        public async Task<DetailProductLotModel> GetByKeysAsync(DetailProductLotKey keys) => await GetSingleByTwoIdAsync(ColumnIdName, SecondColumnIdName, keys.ProductLotId, keys.ProductBarcode);

        public async Task<IEnumerable<DetailProductLotModel>> GetByListKeysAsync(IEnumerable<DetailProductLotKey> keys)
        {
            try
            {
                if (keys == null || !keys.Any())
                    throw new ArgumentException("Keys collection cannot be null or empty.");
                string query = $@"SELECT * FROM {TableName} WHERE ({ColumnIdName}, {SecondColumnIdName}) IN @Keys";
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<DetailProductLotModel> results = await connection.QueryAsync<DetailProductLotModel>(query, new { Keys = keys });

                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {query} with parameters {keys}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting list of keys from {TableName}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<DetailProductLotModel>> GetByMFGDateAsync(int year, int month)
            => await GetByDateTimeAsync("product_lot_mfg_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));


        public async Task<IEnumerable<DetailProductLotModel>> GetByMFGDateAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("product_lot_exp_date", EQueryTimeType.YEAR, type, year);
        }

        public async Task<IEnumerable<DetailProductLotModel>> GetByMFGDateAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("product_lot_mfg_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate));

        public async Task<IEnumerable<DetailProductLotModel>> GetByMFGDateAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("product_lot_mfg_date", EQueryTimeType.DATE_TIME, type, dateTime);
        }

        public async Task<IEnumerable<DetailProductLotModel>> GetByProductBarcode(string barcode) => await GetByInputAsync(barcode, "product_barcode");
    }
}
