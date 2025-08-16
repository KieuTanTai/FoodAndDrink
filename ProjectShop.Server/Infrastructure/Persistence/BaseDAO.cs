using Dapper;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Persistence
{
    public abstract class BaseDAO<T>(
        IDbConnectionFactory connectionFactory,
        IColumnService colService,
        IStringConverter converter,
        IStringChecker checker,
        string tableName,
        string columnIdName,
        string secondColumnIdName = "") : BaseGetDataDAO<T>(connectionFactory, tableName, columnIdName), IDAO<T> where T : class
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
                throw new Exception($"Error creating connection or transaction for {TableName}: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> InsertManyAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null || !entities.Any())
                    return 0; // Trả về 0 nếu không có đối tượng nào.

                using IDbConnection connection = ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string insertQuery = GetInsertQuery();
                    int affectedRows = await connection.ExecuteAsync(insertQuery, entities, transaction);

                    transaction.Commit();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(TableName + $" Error inserting multiple entities: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating connection or transaction for {TableName}: {ex.Message}", ex);
            }
        }

        // 2. Usage in UpdateAsync
        public virtual async Task<int> UpdateAsync(T entity)
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    int affectedRows = await connection.ExecuteAsync(GetUpdateQuery(), entity, transaction);
                    transaction.Commit();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Error updating data in {TableName}: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating connection or transaction for {TableName}: {ex.Message}", ex);
            }
        }

        public virtual async Task<int> UpdateManyAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null || !entities.Any())
                    return 0; // Không có gì để cập nhật.

                using IDbConnection connection = ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string updateQuery = GetUpdateQuery();
                    int affectedRows = await connection.ExecuteAsync(updateQuery, entities, transaction);
                    transaction.Commit();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Error updating multiple entities in {TableName}: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating connection or transaction for {TableName}: {ex.Message}", ex);
            }
        }

#nullable enable
        public virtual async Task<IEnumerable<TResult>?> QueryAsync<TResult>(string query, object? parameters = null)
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<TResult> result = await connection.QueryAsync<TResult>(query, parameters);
                return result.AsList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return default;
            }
        }

        public virtual async Task<TResult?> QueryFirstOrDefaultAsync<TResult>(string query, object? parameters = null, IDbTransaction? transaction = null)
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                try
                {
                    TResult? result = await connection.QueryFirstOrDefaultAsync(query, parameters, transaction);
                    transaction?.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Commit!\n{ex.StackTrace}");
                    transaction?.Rollback();
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return default;
            }
        }

        public virtual async Task<bool> ExecuteAsync(string query, object? parameters = null, IDbTransaction? transaction = null)
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                try
                {
                    int affectedRows = await connection.ExecuteAsync(query, parameters, transaction);
                    transaction?.Commit();
                    return affectedRows > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Commit!\n{ex.StackTrace}");
                    transaction?.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

#nullable disable
        protected abstract string GetInsertQuery();
        protected abstract string GetUpdateQuery();
    }
}
