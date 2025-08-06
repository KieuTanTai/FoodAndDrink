using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class BankDAO : BaseDAO<BankModel>, IGetRelativeAsync<BankModel>, IGetDataByEnumAsync<BankModel>
    {
        public BankDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "banks", "bank_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $"INSERT INTO {TableName} (bank_name, status) VALUES(@BankName, @Status); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            return $@"UPDATE {TableName}
                            SET bank_name = @BankName, status = @Status     
                            WHERE {ColumnIdName} = @{Converter.SnakeCaseToPascalCase(ColumnIdName)}";
        }

        protected override string DeleteByIdQuery(string colIdName)
        {
            return ""; // Soft delete is handled in DeleteAsync
        }
        public string GetQueryDataString(string colName)
        {
            if (!ColService.IsValidColumn(TableName, colName))
                return "";
            return $"SELECT * FROM {TableName} WHERE {colName} LIKE @Input";
        }

        public async override Task<int> DeleteAsync(string id)
        {
            BankModel bank = await GetByIdAsync(id);
            if (bank == null)
                return -1;
            bank.SetStatus(EActiveStatus.INACTIVE);
            return await UpdateAsync(bank);
        }

        public override async Task<int> DeleteManyAsync(IEnumerable<string> ids)
        {
            if (ids == null || !ids.Any())
                return -1;

            List<BankModel> banksToUpdate = new List<BankModel>();

            foreach (string id in ids)
            {
                BankModel bank = await GetByIdAsync(id);
                if (bank == null)
                    return -1;
                bank.SetStatus(EActiveStatus.INACTIVE);
                banksToUpdate.Add(bank);
            }
            return await UpdateManyAsync(banksToUpdate);
        }

        public async Task<List<BankModel>> GetRelativeAsync(string input, string colName)
        {
            try
            {
                string query = GetQueryDataString(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%";
                IEnumerable<BankModel> result = await connection.QueryAsync<BankModel>(query, new { Input = input });
                return result.AsList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new();
            }
        }

        // search by enum
        public async Task<List<BankModel>> GetAllByEnumAsync<TEnum>(TEnum value, string colName) where TEnum : Enum
        {
            if (value is EActiveStatus)
            {
                try
                {
                    string query = GetByIdQuery(colName);
                    using IDbConnection connection = ConnectionFactory.CreateConnection();
                    IEnumerable<BankModel> result = await connection.QueryAsync<BankModel>(query, new { Id = value });
                    return result.AsList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return new();
                }
            }
            return new();
        }
    }
}
