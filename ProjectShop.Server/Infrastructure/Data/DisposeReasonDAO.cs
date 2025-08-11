using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

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

        private string GetRelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} LIKE @Input;";
        }

        public async Task<List<DisposeReasonModel>> GetRelativeAsync(string input, string colName)
        {
            try
            {
                string query = GetRelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%"; // Ensure input is wrapped in wildcards
                IEnumerable<DisposeReasonModel> disposeReasons = await connection.QueryAsync<DisposeReasonModel>(query, new { Input = input });
                return disposeReasons.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(DisposeReasonDAO)}.{nameof(GetRelativeAsync)}: {ex.Message}", ex);
            }
        }
    }
}
