using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class ProductCateogriesDAO : BaseDAO<ProductCategoriesModel>, IGetByKeysAsync<ProductCategoriesModel, ProductCategoriesKey>,
        IDeleteByKeysAsync<ProductCategoriesKey>
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
            return $@"UPDATE {TableName} SET category_id = @CategoryId WHERE product_barcode = @ProductBarcode AND 
                        category_id = @OldCategoryId";
        }

        private string GetSelectByIdsQuery()
        {
            return $@"SELECT * FROM {TableName} WHERE category_id = @CategoryId AND product_barcode = @ProductBarcode";
        }

        private string GetDeleteByKeysQuery()
        {
            return $@"DELETE FROM {TableName} WHERE category_id = @CategoryId AND product_barcode = @ProductBarcode";
        }

        private string GetDeleteQuery(string colName)
        {
            CheckColumnName(colName);
            string column = Converter.SnakeCaseToPascalCase(colName);
            return $@"DELETE FROM {TableName} WHERE {colName} = @{column}";
        }

        public async Task<ProductCategoriesModel> GetByKeysAsync(ProductCategoriesKey keys)
        {
            try
            {
                string query = GetSelectByIdsQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("CategoryId", keys.CategoryId);
                dynamic.Add("ProductBarcode", keys.ProductBarcode);
                ProductCategoriesModel? result = await connection.QueryFirstOrDefaultAsync<ProductCategoriesModel>(query, dynamic);
                if (result == null)
                    throw new KeyNotFoundException($"No product category found with CategoryId: {keys.CategoryId} and ProductBarcode: {keys.ProductBarcode}");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(GetByKeysAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductCategoriesModel>> GetByListKeysAsync(IEnumerable<ProductCategoriesKey> keys)
        {
            try
            {
                string query = GetSelectByIdsQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductCategoriesModel> result = await connection.QueryAsync<ProductCategoriesModel>(query, keys);
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(GetByListKeysAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<int> DeleteBySingleKeyAsync(string key, string colName)
        {
            try
            {
                string query = GetDeleteQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add(colName, key);
                int result = await connection.ExecuteAsync(query, dynamic);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(DeleteBySingleKeyAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<int> DeleteByKeysAsync(ProductCategoriesKey keys)
        {
            try
            {
                string query = GetDeleteByKeysQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                int result = await connection.ExecuteAsync(query, keys);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(DeleteByKeysAsync)}: {ex.Message}", ex);
            }
        }
    }
}
