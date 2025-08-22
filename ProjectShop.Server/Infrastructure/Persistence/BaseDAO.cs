using Dapper;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Persistence
{
    public abstract class BaseDAO<TEntity>(
        IDbConnectionFactory connectionFactory,
        IStringConverter converter,
        ILogService logger,
        string tableName,
        string columnIdName,
        string secondColumnIdName = "") : BaseGetDataDAO<TEntity>(connectionFactory, converter, logger, tableName, columnIdName, secondColumnIdName),
        IDAO<TEntity> where TEntity : class
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
                    int result = await connection.ExecuteScalarAsync<int>(GetInsertQuery(), entity, transaction);
                    transaction.Commit();
                    Logger.LogInfo<TEntity, BaseDAO<TEntity>>($"Successfully inserted data into {TableName}");
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Logger.LogError<TEntity, BaseDAO<TEntity>>($"Error inserting data into {TableName}", ex);
                    throw new Exception($"Error inserting data into {TableName}: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError<TEntity, BaseDAO<TEntity>>($"Error creating connection or transaction for {TableName}", ex);
                throw new Exception($"Error creating connection or transaction for {TableName}: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> InsertAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null || !entities.Any())
                    return 0; // Trả về 0 nếu không có đối tượng nào.

                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string insertQuery = GetInsertQuery();
                    int affectedRows = await connection.ExecuteAsync(insertQuery, entities, transaction);
                    transaction.Commit();
                    Logger.LogInfo<IEnumerable<TEntity>, BaseDAO<TEntity>>($"Successfully inserted multiple entities into {TableName}");
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Logger.LogError<IEnumerable<TEntity>, BaseDAO<TEntity>>($"Error inserting multiple entities into {TableName}", ex);
                    throw new Exception(TableName + $" Error inserting multiple entities: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError<IEnumerable<TEntity>, BaseDAO<TEntity>>($"Error creating connection or transaction for {TableName}", ex);
                throw new Exception($"Error creating connection or transaction for {TableName}: {ex.Message}", ex);
            }
        }

        // 2. Usage in UpdateAsync
        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            try
            {
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    int affectedRows = await connection.ExecuteAsync(GetUpdateQuery(), entity, transaction);
                    transaction.Commit();
                    Logger.LogInfo<TEntity, BaseDAO<TEntity>>($"Successfully updated data in {TableName}");
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Logger.LogError<TEntity, BaseDAO<TEntity>>($"Error updating data in {TableName}", ex);
                    throw new Exception($"Error updating data in {TableName}: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError<TEntity, BaseDAO<TEntity>>($"Error creating connection or transaction for {TableName}", ex);
                throw new Exception($"Error creating connection or transaction for {TableName}: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> UpdateAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null || !entities.Any())
                    return 0; // Không có gì để cập nhật.

                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string updateQuery = GetUpdateQuery();
                    int affectedRows = await connection.ExecuteAsync(updateQuery, entities, transaction);
                    transaction.Commit();
                    Logger.LogInfo<IEnumerable<TEntity>, BaseDAO<TEntity>>($"Successfully updated multiple entities in {TableName}");
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Logger.LogError<IEnumerable<TEntity>, BaseDAO<TEntity>>($"Error updating multiple entities in {TableName}", ex);
                    throw new Exception($"Error updating multiple entities in {TableName}: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError<IEnumerable<TEntity>, BaseDAO<TEntity>>($"Error creating connection or transaction for {TableName}", ex);
                throw new Exception($"Error creating connection or transaction for {TableName}: {ex.Message}", ex);
            }
        }

        protected abstract string GetInsertQuery();
        protected abstract string GetUpdateQuery();
    }
}
