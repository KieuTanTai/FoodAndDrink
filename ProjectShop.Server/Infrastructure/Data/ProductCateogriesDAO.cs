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
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "product_categories", "category_id", "product_barcode")
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

        public async Task<IEnumerable<ProductCategoriesModel>> GetByCategoryIdAsync(uint categoryId) => await GetByInputAsync(categoryId.ToString(), "category_id");

        public async Task<IEnumerable<ProductCategoriesModel>> GetByProductBarcodeAsync(string productBarcode) => await GetByInputAsync(productBarcode, "product_barcode");

        public async Task<ProductCategoriesModel> GetByKeysAsync(ProductCategoriesKey keys) => await GetSingleByTwoIdAsync(ColumnIdName, SecondColumnIdName, keys.CategoryId, keys.ProductBarcode);

        public async Task<IEnumerable<ProductCategoriesModel>> GetByListKeysAsync(IEnumerable<ProductCategoriesKey> keys)
        {
            try
            {
                string query = $@"SELECT * FROM {TableName} WHERE ({ColumnIdName}, {SecondColumnIdName}) IN @Keys";
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<ProductCategoriesModel> results = await connection.QueryAsync<ProductCategoriesModel>(query, new { Keys = keys.Select(k => new { k.CategoryId, k.ProductBarcode }) });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No records found in {TableName} for the provided keys.");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data from {TableName} with keys: {ex.Message}", ex);
            }
        }

        public async Task<int> DeleteByKeysAsync(ProductCategoriesKey keys)
        {
            try
            {
                string query = GetDeleteByKeysQuery();
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                return await connection.ExecuteAsync(query, keys);
            }
            catch (Exception ex)
            {
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
                return await connection.ExecuteAsync(query, new { Input = key });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting data from {TableName} with key {key}: {ex.Message}", ex);
            }
        }
    }
}
