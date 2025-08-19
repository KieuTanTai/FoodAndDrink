using Dapper;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Persistence
{
    public abstract class BaseGetDataDAO<TEntity>(
        IDbConnectionFactory connectionFactory,
        IColumnService colService,
        IStringConverter converter,
        IStringChecker checker,
        string tableName,
        string columnIdName,
        string secondColumnIdName = "") where TEntity : class
    {
        protected IDbConnectionFactory ConnectionFactory { get; } = connectionFactory;
        protected string TableName { get; } = tableName;
        protected string ColumnIdName { get; } = columnIdName;

        protected IColumnService ColService { get; } = colService;
        protected IStringConverter Converter { get; } = converter;
        protected IStringChecker Checker { get; } = checker;
        protected string SecondColumnIdName { get; } = secondColumnIdName ?? string.Empty;

        // GET ALL
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                string query = GetAllQuery();
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query);
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all data from {TableName}: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int maxGetCount)
        {
            if (maxGetCount <= 0)
                throw new ArgumentException("Max get count must be greater than zero.", nameof(maxGetCount));
            try
            {
                string query = $"{GetAllQuery()} LIMIT @MaxGetCount";
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { MaxGetCount = maxGetCount });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} with limit {maxGetCount}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all data from {TableName} with limit: {ex.Message}", ex);
            }
        }

        // GET SINGLE BY ID
        public virtual async Task<TEntity?> GetSingleDataAsync(string input)
            => await GetSingleDataAsync(input, ColumnIdName);
        public virtual async Task<IEnumerable<TEntity>> GetByInputsAsync(IEnumerable<string> inputs)
            => await GetByInputsAsync(inputs, ColumnIdName);
        public virtual async Task<IEnumerable<TEntity>> GetByInputAsync(string input, int maxGetCount)
            => await GetByInputAsync(input, ColumnIdName, maxGetCount);
        public virtual async Task<IEnumerable<TEntity>> GetByInputsAsync(IEnumerable<string> inputs, int maxGetCount)
            => await GetByInputsAsync(inputs, ColumnIdName, maxGetCount);
        // DRY
        protected async Task<TEntity?> GetSingleDataAsync(string input, string colName)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input cannot be null or empty.", nameof(input));
            try
            {
                string query = GetDataQuery(colName);
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                TEntity? result = await connection.QueryFirstOrDefaultAsync<TEntity>(query, new { Input = input });
                return result ?? throw new KeyNotFoundException($"No data found in {TableName} for {colName} = {input}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data from {TableName} by {ColumnIdName}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByInputAsync(string input, string colName)
        {
            try
            {
                string query = GetDataQuery(colName);
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { Input = input });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {colName} = {input}");
                return results;
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving data by {input}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByInputAsync(string input, string colName, int maxGetCount)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input cannot be null or empty.", nameof(input));
            if (maxGetCount <= 0)
                throw new ArgumentException("Max get count must be greater than zero.", nameof(maxGetCount));
            try
            {
                string query = $"{GetDataQuery(colName)} LIMIT @MaxGetCount";
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { Input = input, MaxGetCount = maxGetCount });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {colName} = {input} with limit {maxGetCount}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by {input} with limit: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByInputAsync(string input, ECompareType compareType, string colName)
        {
            try
            {
                string query = GetDataQuery(colName, compareType);
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { Input = input });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {colName} with comparison type {compareType} and input {input}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by {input} with comparison type {compareType}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByInputsAsync(IEnumerable<string> inputs, string colName)
        {
            if (inputs == null || !inputs.Any())
                throw new ArgumentException("Inputs cannot be null or empty.", nameof(inputs));
            try
            {
                string query = $"SELECT * FROM {TableName} WHERE {colName} IN @Inputs";
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { Inputs = inputs });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for inputs in column {colName}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data from {TableName} by inputs in column {ColumnIdName}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByInputsAsync(IEnumerable<string> inputs, string colName, int maxGetCount)
        {
            if (inputs == null || !inputs.Any())
                throw new ArgumentException("Inputs cannot be null or empty.", nameof(inputs));
            if (maxGetCount <= 0)
                throw new ArgumentException("Max get count must be greater than zero.", nameof(maxGetCount));
            try
            {
                string query = $"SELECT * FROM {TableName} WHERE {colName} IN @Inputs LIMIT @MaxGetCount";
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { Inputs = inputs, MaxGetCount = maxGetCount });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for inputs in column {colName} with limit {maxGetCount}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data from {TableName} by inputs in column {ColumnIdName} with limit: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByLikeStringAsync(string input, string colName)
        {
            try
            {
                string query = RelativeQuery(colName);
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%";
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { Input = input });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {colName} like {input}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by relative search: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByLikeStringAsync(string input, string colName, int maxGetCount)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input cannot be null or empty.", nameof(input));
            if (maxGetCount <= 0)
                throw new ArgumentException("Max get count must be greater than zero.", nameof(maxGetCount));
            try
            {
                string query = $"{RelativeQuery(colName)} LIMIT @MaxGetCount";
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { Input = input, MaxGetCount = maxGetCount });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {colName} = {input} with limit {maxGetCount}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by like string: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByDateTimeAsync(string colName, EQueryTimeType queryType, object param)
        {
            string query = queryType switch
            {
                EQueryTimeType.MONTH_AND_YEAR => GetByMonthAndYear(colName),
                EQueryTimeType.DATE_TIME_RANGE => GetByDateTimeRange(colName),
                _ => throw new ArgumentException("Invalid query type")
            };
            try
            {

                DynamicParameters DParam = new DynamicParameters();
                if (queryType == EQueryTimeType.MONTH_AND_YEAR)
                {
                    if (param is { } tuple && tuple is ValueTuple<int, int> time)
                    {
                        DParam.Add("FirstTime", time.Item1);
                        DParam.Add("SecondTime", time.Item2);
                    }
                    else
                        throw new ArgumentException("Invalid parameters for MONTH_AND_YEAR query type");
                }
                else if (queryType == EQueryTimeType.DATE_TIME_RANGE)
                {
                    if (param is { } tuple && tuple is ValueTuple<DateTime, DateTime> range)
                    {
                        DParam.Add("FirstTime", range.Item1);
                        DParam.Add("SecondTime", range.Item2);
                    }
                    else
                        throw new ArgumentException("Invalid parameters for DATE_TIME_RANGE query type");
                }

                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, param);
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {colName} with the specified parameters");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by {colName}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByDateTimeAsync(string colName, EQueryTimeType queryType, ECompareType compareType, object param)
        {
            string query = queryType switch
            {
                EQueryTimeType.YEAR => GetByYear(colName, compareType),
                EQueryTimeType.DATE_TIME => GetByDateTime(colName, compareType),
                _ => throw new ArgumentException("Invalid query type")
            };
            try
            {

                DynamicParameters DParam = new DynamicParameters();
                if (queryType == EQueryTimeType.YEAR)
                {
                    if (param is int year)
                        DParam.Add("Input", year);
                    else
                        throw new ArgumentException("Invalid parameters for YEAR query type");
                }
                else if (queryType == EQueryTimeType.DATE_TIME)
                {
                    if (param is DateTime dateTime)
                        DParam.Add("Input", dateTime);
                    else
                        throw new ArgumentException("Invalid parameters for DATE_TIME query type");
                }

                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, DParam);
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {colName} with the specified parameters");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by {colName}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByRangeDecimalAsync(decimal minDecimal, decimal maxDecimal, string colName)
        {
            try
            {
                string query = GetRangeDecimalQuery(colName);
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { MinDecimal = minDecimal, MaxDecimal = maxDecimal });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for decimal range {minDecimal} - {maxDecimal} in column {colName}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving data by total decimal range", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByDecimalAsync<TEnum>(decimal decimalValue, TEnum compareType, string colName) where TEnum : Enum
        {
            if (compareType is ECompareType type)
            {
                try
                {
                    string query = GetCompareDecimalQuery(colName, type);
                    using IDbConnection connection = await ConnectionFactory.CreateConnection();
                    IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { Decimal = decimalValue });
                    if (results == null || !results.Any())
                        throw new KeyNotFoundException($"No data found in {TableName} for {colName} with decimal {decimalValue} and comparison type {type}");
                    return results;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving data by total decimal with comparison type", ex);
                }
            }
            else
                throw new ArgumentException("Invalid comparison type", nameof(compareType));
        }

        protected async Task<TEntity> GetSingleByTwoIdAsync(string firstColName, string secondColName, object firstParam, object secondParam)
        {
            try
            {
                string query = GetByTwoIdQuery(firstColName, secondColName);
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                TEntity? result = await connection.QueryFirstOrDefaultAsync<TEntity>(query, new { FirstInput = firstParam, SecondInput = secondParam }) ??
                    throw new KeyNotFoundException($"No data found in {TableName} for {firstColName} and {secondColName} with parameters {firstParam} + {secondParam}");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by keys {firstParam} + {secondParam}: {ex.Message}", ex);
            }
        }

        protected virtual string GetAllQuery()
        {
            return $"SELECT * FROM {TableName}";
        }

        protected virtual string GetDataQuery(string colName)
        {
            return $"SELECT * FROM {TableName} WHERE {colName} = @Input";
        }

        protected virtual string GetDataQuery(string colName, ECompareType compareType)
        {
            string type = GetStringType(compareType);
            return $"SELECT * FROM {TableName} WHERE {colName} {type} @Input";
        }

        protected virtual string GetByTwoIdQuery(string firstColName, string secondColName)
        {
            return $"SELECT * FROM {TableName} WHERE {firstColName} = @FirstInput AND {secondColName} = @SecondInput";
        }

        protected virtual string RelativeQuery(string colName)
        {
            return $"SELECT * FROM {TableName} WHERE {colName} LIKE @input";
        }

        protected virtual string GetByMonthAndYear(string colName)
        {
            return $"SELECT * FROM {TableName} WHERE YEAR({colName}) = @FirstTime AND MONTH({colName}) = @SecondTime";
        }

        protected virtual string GetByYear(string colName, ECompareType compareType)
        {
            string type = GetStringType(compareType);
            return $"SELECT * FROM {TableName} WHERE Year({colName}) {type} @Input";
        }

        protected virtual string GetByDateTimeRange(string colName)
        {
            return $"SELECT * FROM {TableName} WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        protected virtual string GetByDateTime(string colName, ECompareType compareType)
        {
            string type = GetStringType(compareType);
            return $"SELECT * FROM {TableName} WHERE {colName} {type} DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        protected virtual string GetCompareDecimalQuery(string colName, ECompareType compareType)
        {
            string compareOperator = GetStringType(compareType);
            return $@"
                SELECT * FROM {TableName}
                WHERE {colName} {compareOperator} @Decimal";
        }

        protected virtual string GetRangeDecimalQuery(string colName)
        {
            return $@"
                SELECT * FROM {TableName}
                WHERE {colName} BETWEEN @MinDecimal AND @MaxDecimal";
        }

        protected string GetStringType(ECompareType compareType)
        {
            return compareType switch
            {
                ECompareType.EQUAL => "=",
                ECompareType.GREATER_THAN => ">",
                ECompareType.LESS_THAN => "<",
                ECompareType.GREATER_THAN_OR_EQUAL => ">=",
                ECompareType.LESS_THAN_OR_EQUAL => "<=",
                _ => throw new ArgumentException("Invalid compare type", nameof(compareType))
            };
        }

        protected string GetTinyIntString(bool value)
        {
            return value ? "1" : "0";
        }
    }
}
