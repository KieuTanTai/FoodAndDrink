using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

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

        public async Task<IEnumerable<BankModel>> GetByLikeStringAsync(string input) => await GetByLikeStringAsync(input, "bank_name");

        public async Task<IEnumerable<BankModel>> GetByStatusAsync(bool status) => await GetByInputAsync(GetTinyIntString(status), "bank_status");
    }
}
