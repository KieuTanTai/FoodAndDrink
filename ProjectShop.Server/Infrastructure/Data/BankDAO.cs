using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class BankDAO : BaseDAO<BankModel>, IGetRelativeAsync<BankModel>, IGetByStatusAsync<BankModel>
    {
        public BankDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "bank", "bank_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (bank_name, bank_status) 
                      VALUES (@BankName, @BankStatus); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET bank_name = @BankName, bank_status = @BankStatus 
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        private string RelativeQuery(string colName)
        {
            CheckColumnName(colName); // It'll throw an exception if the column name is invalid
            return $"SELECT * FROM {TableName} WHERE {colName} LIKE @input";
        }

        public async Task<List<BankModel>> GetRelativeAsync(string input, string colName)
        {
            try
            {
                string query = RelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%";
                IEnumerable<BankModel> result = await connection.QueryAsync<BankModel>(query, new { Input = input });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving banks by relative search: {ex.Message}", ex);
            }
        }

        public async Task<List<BankModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("bank_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<BankModel> result = await connection.QueryAsync<BankModel>(query, new { Input = status });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving banks by status: {ex.Message}", ex);
            }
        }
    }
}
