using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class CategoryDAO : BaseDAO<CategoryModel>, IGetByStatusAsync<CategoryModel>, IGetRelativeAsync<CategoryModel>
    {
        public CategoryDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "category", "category_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"
                INSERT INTO {TableName} (category_name, category_description, category_status)
                VALUES (@CategoryName, @CategoryDescription, @CategoryStatus); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"
                UPDATE {TableName}
                SET category_name = @CategoryName,
                    category_description = @CategoryDescription,
                    category_status = @CategoryStatus
                WHERE {ColumnIdName} = @{colIdName}";
        }

        private string RelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $@"
                SELECT * FROM {TableName}
                WHERE {colName} LIKE @Input";
        }

        public async Task<List<CategoryModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = $@"
                    SELECT * FROM {TableName}
                    WHERE category_status = @Status";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<CategoryModel> categories = await connection.QueryAsync<CategoryModel>(query, new { Status = status });
                return categories.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving categories by status", ex);
            }
        }

        public async Task<List<CategoryModel>> GetRelativeAsync(string input, string colName)
        {
            try
            {
                string query = RelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%";
                IEnumerable<CategoryModel> categories = await connection.QueryAsync<CategoryModel>(query, new { Input = input });
                return categories.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving relative categories", ex);
            }
        }
    }
}
