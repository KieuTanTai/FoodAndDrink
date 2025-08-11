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
        string secondColumnIdName = "") : IDAO<T> where T : class
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
                    Console.WriteLine($"Error during transaction: {ex.StackTrace}");
                    transaction.Rollback();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating connection or transaction: {ex.StackTrace}");
                return -1;
            }
        }

        // 2. Usage in UpdateAsync
        public virtual async Task<int> UpdateAsync(T entity, string? oldId = "")
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    int affectedRows = 0;
                    if (!string.IsNullOrEmpty(oldId))
                    {
                        if (string.IsNullOrEmpty(SecondColumnIdName))
                            throw new ArgumentException("Second column ID name cannot be null or empty when oldId is provided.");
                        // Update with oldId
                        var parameters = new DynamicParameters(entity);
                        parameters.Add("OldId", oldId);
                        affectedRows = await connection.ExecuteAsync(GetUpdateQuery(), parameters, transaction);
                        return affectedRows;
                    }
                    else
                    {
                        // Update without oldId
                        affectedRows = await connection.ExecuteAsync(GetUpdateQuery(), entity, transaction);

                        affectedRows = await connection.ExecuteAsync(GetUpdateQuery(), entity, transaction);
                        transaction.Commit();
                        return affectedRows;
                    }

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

        public virtual async Task<int> UpdateManyAsync(IEnumerable<T> entities, IEnumerable<string>? oldIds)
        {
            try
            {
                // Kiểm tra tính hợp lệ của dữ liệu đầu vào.
                // Số lượng entities phải khớp với số lượng oldIds.
                if (entities == null || !entities.Any() || oldIds == null || entities.Count() != oldIds.Count())
                    return 0; // Trả về 0 nếu không có gì để cập nhật hoặc dữ liệu không hợp lệ.

                using IDbConnection connection = ConnectionFactory.CreateConnection();
                using IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Sử dụng Enumerable.Zip để kết hợp entities và oldIds thành một tập hợp duy nhất.
                    // Sau đó, tạo một đối tượng ẩn danh cho mỗi cặp, chứa tất cả các thuộc tính.
                    var parameters = entities.Zip(oldIds, (entity, oldId) =>
                    {
                        var entityAsDynamic = (dynamic)entity; // Ép kiểu entity thành dynamic
                        var combinedParams = new Dapper.DynamicParameters();
                        combinedParams.AddDynamicParams(entityAsDynamic);
                        combinedParams.Add("OldId", oldId); // Thêm oldId vào làm tham số mới
                        return combinedParams;
                    });

                    // Chuỗi truy vấn cập nhật phải sử dụng cả ID và OldId
                    string updateQuery = GetUpdateQuery();

                    // Gọi Dapper ExecuteAsync một lần duy nhất với toàn bộ tập hợp tham số.
                    // Dapper sẽ tự động lặp qua và thực thi truy vấn cho mỗi phần tử.
                    int affectedRows = await connection.ExecuteAsync(updateQuery, parameters, transaction);

                    transaction.Commit();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during transaction: {ex.StackTrace}");
                    transaction.Rollback();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating connection or transaction: {ex.StackTrace}");
                return -1;
            }
        }

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
        protected abstract string GetUpdateQuery();
    }
}
