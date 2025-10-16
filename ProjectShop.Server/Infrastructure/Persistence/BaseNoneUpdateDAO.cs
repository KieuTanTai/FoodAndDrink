using Dapper;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IValidate;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Persistence
{
    public abstract class BaseNoneUpdateDAO<TEntity>(
        IDbConnectionFactory connectionFactory,
        IStringConverter converter,
        ILogService logger,
        string tableName,
        string columnIdName,
        string secondColumnIdName = "") : BaseGetDataDAO<TEntity>(connectionFactory, converter, logger, tableName, columnIdName, secondColumnIdName), INoneUpdateDAO<TEntity> where TEntity : class
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
                    Logger.LogInfo<TEntity, BaseNoneUpdateDAO<TEntity>>($"Successfully inserted data into {TableName}");
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Logger.LogError<TEntity, BaseNoneUpdateDAO<TEntity>>($"Error inserting data into {TableName}", ex);
                    throw new Exception($"Error inserting data into {TableName}: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError<TEntity, BaseNoneUpdateDAO<TEntity>>($"Error creating connection for inserting data into {TableName}", ex);
                throw new Exception($"Error creating connection for inserting data into {TableName}: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> InsertAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    int result = await connection.ExecuteAsync(GetInsertQuery(), entities, transaction);
                    transaction.Commit();
                    Logger.LogInfo<TEntity, BaseNoneUpdateDAO<TEntity>>($"Successfully inserted multiple records into {TableName}");
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Logger.LogError<TEntity, BaseNoneUpdateDAO<TEntity>>($"Error inserting multiple records into {TableName}", ex);
                    throw new Exception($"Error inserting multiple records into {TableName}: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError<TEntity, BaseNoneUpdateDAO<TEntity>>($"Error creating connection for inserting multiple records into {TableName}", ex);
                throw new Exception($"Error creating connection for inserting multiple records into {TableName}: {ex.Message}", ex);
            }
        }

#nullable disable
        protected abstract string GetInsertQuery();
    }
}

