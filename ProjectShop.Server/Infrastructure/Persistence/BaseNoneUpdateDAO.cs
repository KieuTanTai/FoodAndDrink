using Dapper;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Persistence
{
    public abstract class BaseNoneUpdateDAO<TEntity>(
        IDbConnectionFactory connectionFactory,
        IColumnService colService,
        IStringConverter converter,
        IStringChecker checker,
        string tableName,
        string columnIdName,
        string secondColumnIdName = "") : BaseGetDataDAO<TEntity>(connectionFactory, colService, converter, checker, tableName, columnIdName, secondColumnIdName), INoneUpdateDAO<TEntity> where TEntity : class
    {

        // INSERT ENTITY
        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            try
            {
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    int result = await connection.ExecuteAsync(GetInsertQuery(), entity, transaction);
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

        public virtual async Task<int> InsertManyAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    int result = 0;
                    foreach (TEntity entity in entities)
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

