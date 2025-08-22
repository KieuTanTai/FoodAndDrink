using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class CategoryDAO : BaseDAO<CategoryModel>, ICategoryDAO<CategoryModel>
    {
        public CategoryDAO(
            IDbConnectionFactory connectionFactory,
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "category", "category_id", string.Empty)
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

        public async Task<IEnumerable<CategoryModel>> GetByLikeStringAsync(string input, int? maxGetCount)
            => await GetByLikeStringAsync(input, "category_name", maxGetCount);

        public async Task<IEnumerable<CategoryModel>> GetByStatusAsync(bool status, int? maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "category_status", maxGetCount);
    }
}
