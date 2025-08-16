using Dapper;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Persistence
{
    public abstract class BaseNoneUpdateDAO<T>(
        IDbConnectionFactory connectionFactory,
        IColumnService colService,
        IStringConverter converter,
        IStringChecker checker,
        string tableName,
        string columnIdName,
        string secondColumnIdName = "") : BaseGetDataDAO<T>(connectionFactory, tableName, columnIdName), INoneUpdateDAO<T> where T : class
    {
        protected IColumnService ColService { get; } = colService;
        protected IStringConverter Converter { get; } = converter;
        protected IStringChecker Checker { get; } = checker;
        protected string SecondColumnIdName { get; } = secondColumnIdName ?? string.Empty;

        // INSERT ENTITY
        public virtual async Task<int> InsertAsync(T entity)
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    int result = await connection.QueryFirstOrDefaultAsync(GetInsertQuery(), entity, transaction);
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Error inserting data into {TableName}: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating connection for inserting data into {TableName}: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> InsertManyAsync(IEnumerable<T> entities)
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    int result = 0;
                    foreach (T entity in entities)
                        result += await connection.ExecuteAsync(GetInsertQuery(), entity, transaction);
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Error inserting multiple records into {TableName}: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating connection for inserting multiple records into {TableName}: {ex.Message}", ex);
            }
        }

#nullable disable
        protected abstract string GetInsertQuery();
    }
}

