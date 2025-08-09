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
        string secondColumnIdName = "") : IDAO<T>, IDbOperationAsync<T>, IQueryOperationsAsync, IExecuteOperationsAsync where T : class
    {
        protected IDbConnectionFactory ConnectionFactory { get; } = connectionFactory;
        protected IColumnService ColService { get; } = colService;
        protected IStringConverter Converter { get; } = converter;
        protected IStringChecker Checker { get; } = checker;

        protected string TableName { get; } = tableName;
        protected string ColumnIdName { get; } = columnIdName;
        protected string SecondColumnIdName { get; } = secondColumnIdName ?? string.Empty;

        // GET ALL
        public virtual async Task<List<T>> GetAllAsync()
        {
            try
            {
                string query = GetAllQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<T> result = await connection.QueryAsync<T>(query);
                return result.AsList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new();
            }
        }

        // GET SINGLE BY ID
        public virtual async Task<T?> GetByIdAsync(string id)
        {
            try
            {
                string query = GetByIdQuery(ColumnIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                T? result = await connection.QueryFirstOrDefaultAsync(query);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public virtual async Task<List<T>> GetByIdsAsync(IEnumerable<string> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    return new List<T>();
                string query = $"SELECT * FROM {TableName} WHERE {ColumnIdName} IN @Ids";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<T> result = await connection.QueryAsync<T>(query, new { Ids = ids });
                return result.AsList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new List<T>();
            }
        }

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
                    Console.WriteLine($"Error Commit!\n{ex.StackTrace}");
                    transaction.Rollback();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return -1;
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
                    Console.WriteLine($"Error Commit!\n{ex.StackTrace}");
                    transaction.Rollback();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return -1;
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
                    Console.WriteLine($"Error Commit!\n{ex.StackTrace}");
                    transaction.Rollback();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return -1;
            }
        }

        public virtual async Task<int> UpdateManyAsync(IEnumerable<T> entities)
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    int affectedRows = 0;
                    foreach (T entity in entities)
                        affectedRows += await connection.ExecuteAsync(GetUpdateQuery(), entity, transaction);
                    transaction.Commit();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Commit!\n{ex.StackTrace}");
                    transaction.Rollback();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return -1;
            }
        }

        //DELETE ENTITY
        //public virtual async Task<int> DeleteAsync(string id)
        //{
        //    try
        //    {
        //        string query = DeleteByIdQuery(ColumnIdName);
        //        using IDbConnection connection = ConnectionFactory.CreateConnection();
        //        using IDbTransaction transaction = connection.BeginTransaction();
        //        try
        //        {
        //            int affectedRows = await connection.ExecuteAsync(query, new { Id = id }, transaction);
        //            transaction.Commit();
        //            return affectedRows;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error Commit!\n{ex.StackTrace}");
        //            transaction.Rollback();
        //            return -1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //        return -1;
        //    }
        //}

        //public virtual async Task<int> DeleteManyAsync(IEnumerable<string> ids)
        //{
        //    try
        //    {
        //        string query = DeleteByIdQuery(ColumnIdName);
        //        using IDbConnection connection = ConnectionFactory.CreateConnection();
        //        using IDbTransaction transaction = connection.BeginTransaction();
        //        try
        //        {
        //            int affectedRows = 0;
        //            foreach (string id in ids)
        //                affectedRows += await connection.ExecuteAsync(query, new { Id = id }, transaction);
        //            transaction.Commit();
        //            return affectedRows;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error Commit!\n{ex.StackTrace}");
        //            transaction.Rollback();
        //            return -1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //        return -1;
        //    }
        //}

#nullable enable
        public virtual async Task<List<TResult>?> QueryAsync<TResult>(string query, object? parameters = null)
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

        protected virtual string GetAllQuery()
        {
            return $"SELECT * FROM {TableName}";
        }

        protected virtual string GetByIdQuery(string colIdName)
        {   
            CheckColumnName(colIdName);
            return $"SELECT * FROM {TableName} WHERE {colIdName} = @Id";
        }

        protected virtual string DeleteByIdQuery(string colIdName)
        {
            CheckColumnName(colIdName);
            return $"DELETE FROM {TableName} WHERE {colIdName} = @Id";
        }

        protected void CheckColumnName(string colName)
        {
            if (!ColService.IsValidColumn(TableName, colName))
                throw new ArgumentException($"Invalid column name: {colName}");
        }

#nullable disable
        protected abstract string GetInsertQuery();
        protected abstract string GetUpdateQuery();
    }
}
