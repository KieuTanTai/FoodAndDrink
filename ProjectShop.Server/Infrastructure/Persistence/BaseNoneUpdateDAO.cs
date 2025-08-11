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
        string secondColumnIdName = "") : INoneUpdateDAO<T> where T : class
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
        public virtual async Task<T?> GetSingleDataAsync(string input, string colName)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentException("Input cannot be null or empty.", nameof(input));
                string query = GetDataQuery(colName);
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

        public virtual async Task<List<T>> GetByInputsAsync(IEnumerable<string> inputs, string colName)
        {
            try
            {
                if (string.IsNullOrEmpty(colName))
                    throw new ArgumentException("Column name cannot be null or empty.", nameof(colName));
                if (inputs == null || !inputs.Any())
                    return new List<T>();
                CheckColumnName(colName);
                string query = $"SELECT * FROM {TableName} WHERE {colName} IN @Inputs";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<T> result = await connection.QueryAsync<T>(query, new { Inputs = inputs });
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

        protected virtual string GetAllQuery()
        {
            return $"SELECT * FROM {TableName}";
        }

        protected virtual string GetDataQuery(string colIdName)
        {
            CheckColumnName(colIdName);
            return $"SELECT * FROM {TableName} WHERE {colIdName} = @Input";
        }

        protected void CheckColumnName(string colName)
        {
            if (!ColService.IsValidColumn(TableName, colName))
                throw new ArgumentException($"Invalid column name: {colName}");
        }

#nullable disable
        protected abstract string GetInsertQuery();
    }
}

