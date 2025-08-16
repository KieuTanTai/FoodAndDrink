using Dapper;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Persistence
{
    public abstract class BaseGetDataDAO<T>(
        IDbConnectionFactory connectionFactory,
        string tableName,
        string columnIdName) where T : class
    {
        protected IDbConnectionFactory ConnectionFactory { get; } = connectionFactory;
        protected string TableName { get; } = tableName;
        protected string ColumnIdName { get; } = columnIdName;

        // GET ALL
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                string query = GetAllQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<T> results = await connection.QueryAsync<T>(query);
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all data from {TableName}: {ex.Message}", ex);
            }
        }

        // GET SINGLE BY ID
        public virtual async Task<T?> GetSingleDataAsync(string input) => await GetSingleDataAsync(input, ColumnIdName);
        public virtual async Task<IEnumerable<T>> GetByInputsAsync(IEnumerable<string> inputs) => await GetByInputsAsync(inputs, ColumnIdName);

        // DRY
        protected async Task<T?> GetSingleDataAsync(string input, string colName)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentException("Input cannot be null or empty.", nameof(input));
                string query = GetDataQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                T? result = await connection.QueryFirstOrDefaultAsync<T>(query, new { Input = input });
                return result ?? throw new KeyNotFoundException($"No data found in {TableName} for {colName} = {input}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data from {TableName} by {ColumnIdName}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<T>> GetByInputAsync(string input, string colName)
        {
            try
            {
                string query = GetDataQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<T> results = await connection.QueryAsync<T>(query, new { Input = input });
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

        protected async Task<IEnumerable<T>> GetByInputAsync(string input, ECompareType compareType, string colName)
        {
            try
            {
                string query = GetDataQuery(colName, compareType);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<T> results = await connection.QueryAsync<T>(query, new { Input = input });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {colName} with comparison type {compareType} and input {input}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by {input} with comparison type {compareType}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<T>> GetByInputsAsync(IEnumerable<string> inputs, string colName)
        {
            try
            {
                if (inputs == null || !inputs.Any())
                    throw new ArgumentException("Inputs cannot be null or empty.", nameof(inputs));
                string query = $"SELECT * FROM {TableName} WHERE {colName} IN @Inputs";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<T> results = await connection.QueryAsync<T>(query, new { Inputs = inputs });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for inputs in column {colName}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data from {TableName} by inputs in column {ColumnIdName}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<T>> GetByLikeStringAsync(string input, string colName)
        {
            try
            {
                string query = RelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%";
                IEnumerable<T> results = await connection.QueryAsync<T>(query, new { Input = input });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {colName} like {input}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by relative search: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<T>> GetByDateTimeAsync(string colName, EQueryTimeType queryType, object param)
        {
            try
            {
                string query = queryType switch
                {
                    EQueryTimeType.MONTH_AND_YEAR => GetByMonthAndYear(colName),
                    EQueryTimeType.DATE_TIME_RANGE => GetByDateTimeRange(colName),
                    _ => throw new ArgumentException("Invalid query type")
                };

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

                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<T> results = await connection.QueryAsync<T>(query, param);
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {colName} with the specified parameters");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by {colName}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<T>> GetByDateTimeAsync(string colName, EQueryTimeType queryType, ECompareType compareType, object param)
        {
            try
            {
                string query = queryType switch
                {
                    EQueryTimeType.YEAR => GetByYear(colName, compareType),
                    EQueryTimeType.DATE_TIME => GetByDateTime(colName, compareType),
                    _ => throw new ArgumentException("Invalid query type")
                };

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

                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<T> results = await connection.QueryAsync<T>(query, DParam);
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {colName} with the specified parameters");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by {colName}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<T>> GetByRangeDecimalAsync(decimal minDecimal, decimal maxDecimal, string colName)
        {
            try
            {
                string query = GetRangeDecimalQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<T> results = await connection.QueryAsync<T>(query, new { MinDecimal = minDecimal, MaxDecimal = maxDecimal });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for decimal range {minDecimal} - {maxDecimal} in column {colName}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving data by total decimal range", ex);
            }
        }

        protected async Task<IEnumerable<T>> GetByDecimalAsync<TEnum>(decimal decimalValue, TEnum compareType, string colName) where TEnum : Enum
        {
            if (compareType is ECompareType type)
            {
                try
                {
                    string query = GetCompareDecimalQuery(colName, type);
                    using IDbConnection connection = ConnectionFactory.CreateConnection();
                    IEnumerable<T> results = await connection.QueryAsync<T>(query, new { Decimal = decimalValue });
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

        protected async Task<T> GetSingleByTwoIdAsync(string firstColName, string secondColName, object firstParam, object secondParam)
        {
            try
            {
                string query = GetByTwoIdQuery(firstColName, secondColName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                T? result = await connection.QueryFirstOrDefaultAsync<T>(query, new { FirstInput = firstParam, SecondInput = secondParam }) ?? 
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
