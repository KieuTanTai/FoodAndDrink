using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DisposeReasonDAO : BaseDAO<DisposeReasonModel>, IGetRelativeAsync<DisposeReasonModel>
    {
        public DisposeReasonDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "dispose_reason", "dispose_reason_id", string.Empty)
        {
        }
        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (dispose_reason_name) 
                      VALUES (@DisposeReasonName); SELECT LAST_INSERT_ID();";
        }
        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET dispose_reason_name = @DisposeReasonName 
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        public async Task<IEnumerable<DisposeReasonModel>> GetByLikeStringAsync(string input) => await GetByLikeStringAsync(input, "dispose_reason_name");
    }
}
