using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class ProductCateogriesDAO : BaseDAO<ProductCategoriesModel>, IProductCategoriesDAO<ProductCategoriesModel, ProductCategoriesKey>
    {
        public ProductCateogriesDAO(
            IDbConnectionFactory connectionFactory,
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "product_categories", "category_id", "product_barcode")
        {
        }

        protected override string GetInsertQuery()
        {
            return $"INSERT INTO {TableName} (category_id, product_barcode) VALUES (@CategoryId, @ProductBarcode)";
        }

        protected override string GetUpdateQuery()
        {
            return $@"UPDATE {TableName} SET category_id = @CategoryId WHERE id = ";
        }

        private string GetDeleteByKeysQuery()
        {
            return $@"DELETE FROM {TableName} WHERE category_id = @CategoryId AND product_barcode = @ProductBarcode";
        }

        private string GetDeleteQuery(string colName)
        {
            return $@"DELETE FROM {TableName} WHERE {colName} = @Input";
        }

        public async Task<IEnumerable<ProductCategoriesModel>> GetByCategoryIdAsync(uint categoryId, int? getMaxCount) 
            => await GetByInputAsync(categoryId.ToString(), "category_id", getMaxCount);
        public async Task<IEnumerable<ProductCategoriesModel>> GetByCategoryIdsAsync(IEnumerable<uint> categoryIds, int? getMaxCount)
            => await GetByInputsAsync(categoryIds.Select(categoryId => categoryId.ToString()), "category_id", getMaxCount);
        public async Task<IEnumerable<ProductCategoriesModel>> GetByProductBarcodeAsync(string productBarcode, int? maxGetCount) 
            => await GetByInputAsync(productBarcode, "product_barcode", maxGetCount);

        public async Task<IEnumerable<ProductCategoriesModel>> GetByProductBarcodesAsync(IEnumerable<string> productBarcodes, int? maxGetCount)
            => await GetByInputsAsync(productBarcodes, "product_barcode", maxGetCount);

        public async Task<ProductCategoriesModel?> GetByKeysAsync(ProductCategoriesKey keys) => await GetSingleByTwoIdAsync(ColumnIdName, SecondColumnIdName, keys.CategoryId, keys.ProductBarcode);

        public async Task<IEnumerable<ProductCategoriesModel>> GetByListKeysAsync(IEnumerable<ProductCategoriesKey> keys, int? maxGetCount)
        {
            string query = $@"";
            if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxGetCount), "maxGetCount must be greater than 0.");
            else if (maxGetCount.HasValue && maxGetCount.Value > 0)
                query = $@"SELECT * FROM {TableName} WHERE (category_id, product_barcode) IN @Keys LIMIT @MaxGetCount";
            else
                query = $"SELECT * FROM {TableName} WHERE ({ColumnIdName}, {SecondColumnIdName}) IN @Keys";
            try
            {
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<ProductCategoriesModel> results = await connection.QueryAsync<ProductCategoriesModel>(query, new { Keys = keys.Select(k => new { k.CategoryId, k.ProductBarcode }), MaxGetCount = maxGetCount });
                Logger.LogInfo<IEnumerable<ProductCategoriesModel>, ProductCateogriesDAO>($"Retrieved ProductCategories by keys successfully.");
                return results;
            }
            catch (Exception ex)
            {
                Logger.LogError<IEnumerable<ProductCategoriesModel>, ProductCateogriesDAO>($"Error retrieving data from {TableName} with keys: {ex.Message}", ex);
                throw new Exception($"Error retrieving data from {TableName} with keys: {ex.Message}", ex);
            }
        }

        public async Task<int> DeleteByKeysAsync(ProductCategoriesKey keys)
        {
            try
            {
                string query = GetDeleteByKeysQuery();
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                int affectedRows = await connection.ExecuteAsync(query, keys);
                Logger.LogInfo<int, ProductCateogriesDAO>($"Deleted {affectedRows} records from {TableName} with keys: {keys}");
                return affectedRows;
            }
            catch (Exception ex)
            {
                Logger.LogError<int, ProductCateogriesDAO>($"Error deleting data from {TableName} with keys {keys}: {ex.Message}", ex);
                throw new Exception($"Error deleting data from {TableName} with keys {keys}: {ex.Message}", ex);
            }
        }

        public async Task<int> DeleteByCategoryIdAsync(uint categoryId) => await DeleteBySingleKeyAsync(categoryId.ToString(), "category_id");
        public async Task<int> DeleteByProductBarcodeAsync(string productBarcode) => await DeleteBySingleKeyAsync(productBarcode, "product_barcode");

        private async Task<int> DeleteBySingleKeyAsync(string key, string colName)
        {
            try
            {
                string query = GetDeleteQuery(colName);
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                int affectedRows = await connection.ExecuteAsync(query, new { Input = key });
                Logger.LogInfo<int, ProductCateogriesDAO>($"Deleted {affectedRows} records from {TableName} with {colName}: {key}");
                return affectedRows;
            }
            catch (Exception ex)
            {
                Logger.LogError<int, ProductCateogriesDAO>($"Error deleting data from {TableName} with key {key}: {ex.Message}", ex);
                throw new Exception($"Error deleting data from {TableName} with key {key}: {ex.Message}", ex);
            }
        }
    }
}
