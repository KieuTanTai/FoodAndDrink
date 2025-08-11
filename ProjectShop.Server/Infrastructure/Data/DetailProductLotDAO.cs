using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DetailProductLotDAO : BaseNoneUpdateDAO<DetailProductLotModel>, IGetDataByDateTimeAsync<DetailProductLotModel>, IGetByKeysAsync<DetailProductLotModel, DetailProductLotKey>,
        IGetAllByIdAsync<DetailProductLotModel>
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

        private string GetByDateTimeRange(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        private string GetByDateTime(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} = DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        private string GetByYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE Year({colName}) = @Input";
        }

        private string GetByMonthAndYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE YEAR({colName}) = @FirstTime AND MONTH({colName}) = @SecondTime";
        }

        private string GetByKeys()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            string secondColIdName = Converter.SnakeCaseToPascalCase(SecondColumnIdName);
            return $"SELECT * FROM {TableName} WHERE {ColumnIdName} = @{colIdName} AND {SecondColumnIdName} = @{secondColIdName}";
        }

        public async Task<List<DetailProductLotModel>> GetAllByIdAsync(string id, string colIdName = "product_barcode")
        {
            try
            {
                string query = GetDataQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailProductLotModel> productLots = await connection.QueryAsync<DetailProductLotModel>(query, new { Input = id });
                return productLots.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductLotDAO)}.{nameof(GetAllByIdAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailProductLotModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "product_lot_mfg_date")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailProductLotModel> productLots = await connection.QueryAsync<DetailProductLotModel>(query, new { Input = dateTime });
                return productLots.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductLotDAO)}.{nameof(GetAllByDateTimeAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailProductLotModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "product_lot_mfg_date")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailProductLotModel> productLots = await connection.QueryAsync<DetailProductLotModel>(query, new { FirstTime = startDate, SecondTime = endDate });
                return productLots.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductLotDAO)}.{nameof(GetAllByDateTimeRangeAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailProductLotModel>> GetAllByMonthAndYearAsync(int month, int year, string colName = "product_lot_mfg_date")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailProductLotModel> productLots = await connection.QueryAsync<DetailProductLotModel>(query, new { FirstTime = year, SecondTime = month });
                return productLots.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductLotDAO)}.{nameof(GetAllByMonthAndYearAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailProductLotModel>> GetAllByYearAsync(int year, string colName = "product_lot_mfg_date")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailProductLotModel> productLots = await connection.QueryAsync<DetailProductLotModel>(query, new { Input = year });
                return productLots.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductLotDAO)}.{nameof(GetAllByYearAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<DetailProductLotModel> GetByKeysAsync(DetailProductLotKey keys)
        {
            try
            {
                string query = GetByKeys();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                DetailProductLotModel? productLot = await connection.QueryFirstOrDefaultAsync<DetailProductLotModel>(query, new { ProductLotId = keys.ProductLotId, ProductBarcode = keys.ProductBarcode });
                if (productLot == null)
                    throw new KeyNotFoundException($"No product lot found with keys: {keys.ProductLotId}, {keys.ProductBarcode}");
                return productLot;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductLotDAO)}.{nameof(GetByKeysAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<DetailProductLotModel>> GetByListKeysAsync(IEnumerable<DetailProductLotKey> keys)
        {
            try
            {
                string query = GetByKeys();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<DetailProductLotModel> productLots = await connection.QueryAsync<DetailProductLotModel>(query, keys);
                return productLots.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(ProductLotDAO)}.{nameof(GetByListKeysAsync)}: {ex.Message}", ex);
            }
        }
    }
}
