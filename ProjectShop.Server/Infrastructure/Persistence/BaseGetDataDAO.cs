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
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int? maxGetCount)
        {
            string query = "";
            if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentOutOfRangeException("Max get count must be greater than zero.", nameof(maxGetCount));
            else if (maxGetCount.HasValue && maxGetCount.Value > 0)
                query = $"{GetAllQuery()} LIMIT @MaxGetCount";
            else
                query = GetAllQuery();

            try
            {
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { MaxGetCount = maxGetCount });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($@"No data found in {TableName} with 
                            {(maxGetCount.HasValue ? $"limit {maxGetCount.Value}" : "no limit")}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($@"Error retrieving all data from {TableName}: {ex.Message}", ex);
            }
        }

        // GET SINGLE BY ID
        public virtual async Task<TEntity?> GetSingleDataAsync(string input)
            => await GetSingleDataAsync(input, ColumnIdName);
        public virtual async Task<IEnumerable<TEntity>> GetByInputsAsync(IEnumerable<string> inputs)
            => await GetByInputsAsync(inputs, ColumnIdName);
        public virtual async Task<IEnumerable<TEntity>> GetByInputAsync(string input, int? maxGetCount)
            => await GetByInputAsync(input, ColumnIdName, maxGetCount);
        public virtual async Task<IEnumerable<TEntity>> GetByInputsAsync(IEnumerable<string> inputs, int? maxGetCount)
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

        protected async Task<IEnumerable<TEntity>> GetByInputAsync(string input, string colName, int? maxGetCount)
        {
            string query = "";
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input cannot be null or empty.", nameof(input));
            if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentOutOfRangeException("Max get count must be greater than zero.", nameof(maxGetCount));
            else if (maxGetCount.HasValue && maxGetCount.Value > 0)
                query = $"{GetDataQuery(colName)} LIMIT @MaxGetCount";
            else
                query = GetDataQuery(colName);

            try
            {
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { Input = input, MaxGetCount = maxGetCount });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($@"No data found in {TableName} for {colName} = {input} with 
                            {(maxGetCount.HasValue ? $"limit {maxGetCount.Value}" : "no limit")}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by {input}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByInputAsync(string input, ECompareType compareType, string colName, int? maxGetCount = null)
        {
            string query = "";
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input cannot be null or empty.", nameof(input));
            if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentOutOfRangeException("Max get count must be greater than zero.", nameof(maxGetCount));
            else if (maxGetCount.HasValue && maxGetCount.Value > 0)
                query = $"{GetDataQuery(colName, compareType)} LIMIT @MaxGetCount";
            else
                query = GetDataQuery(colName, compareType);
            try
            {
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { Input = input, MaxGetCount = maxGetCount });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for {colName} with comparison type {compareType} and " +
                        $"input {input} with {(maxGetCount.HasValue ? $"limit {maxGetCount.Value}" : "no limit")}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by {input} with comparison type {compareType}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByInputsAsync(IEnumerable<string> inputs, string colName, int? maxGetCount = null)
        {
            string query = $"";
            if (inputs == null || !inputs.Any())
                throw new ArgumentException("Inputs cannot be null or empty.", nameof(inputs));
            if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentOutOfRangeException("Max get count must be greater than zero.", nameof(maxGetCount));
            else if (maxGetCount.HasValue && maxGetCount.Value > 0)
                query = $"SELECT * FROM {TableName} WHERE {colName} IN @Inputs LIMIT @MaxGetCount";
            else
                query = $"SELECT * FROM {TableName} WHERE {colName} IN @Inputs";

            try
                {
                    using IDbConnection connection = await ConnectionFactory.CreateConnection();
                    IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { Inputs = inputs, MaxGetCount = maxGetCount });
                    if (results == null || !results.Any())
                        throw new KeyNotFoundException($"No data found in {TableName} for inputs in column {colName} with " +
                            $"{(maxGetCount.HasValue ? $"limit {maxGetCount.Value}" : "no limit")}");
                    return results;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving data from {TableName} by inputs in column {ColumnIdName} with limit: {ex.Message}", ex);
                }
        }

        protected async Task<IEnumerable<TEntity>> GetByLikeStringAsync(string input, string colName, int? maxGetCount = null)
        {
            string query = "";
            if (maxGetCount.HasValue && maxGetCount.Value > 0)
                query = $"{RelativeQuery(colName)} LIMIT @MaxGetCount";
            else if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentOutOfRangeException("Max get count must be greater than zero.", nameof(maxGetCount));
            else
                query = RelativeQuery(colName);

            try
            {
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%";

                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { Input = input, MaxGetCount = maxGetCount });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($@"No data found in {TableName} for {colName} like {input} with 
                        {(maxGetCount.HasValue ? $"limit {maxGetCount.Value}" : "no limit")}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving data by relative search: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByDateTimeAsync(string colName, EQueryTimeType queryType, object param, int? maxGetCount = null)
        {
            string subQuery = "";
            if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentOutOfRangeException("Max get count must be greater than zero.", nameof(maxGetCount));
            else if (maxGetCount.HasValue && maxGetCount.Value > 0)
                subQuery = $" LIMIT @MaxGetCount";

                string query = queryType switch
            {
                EQueryTimeType.MONTH_AND_YEAR => $"{GetByMonthAndYear(colName)} {subQuery}",
                EQueryTimeType.DATE_TIME_RANGE => $"{GetByDateTimeRange(colName)} {subQuery}",
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
                DParam.Add("MaxGetCount", maxGetCount);

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

        protected async Task<IEnumerable<TEntity>> GetByDateTimeAsync(string colName, EQueryTimeType queryType, ECompareType compareType, object param, int? maxGetCount = null)
        {
            string subQuery = "";
            if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentOutOfRangeException("Max get count must be greater than zero.", nameof(maxGetCount));
            else if (maxGetCount.HasValue && maxGetCount.Value > 0)
                subQuery = $" LIMIT @MaxGetCount";  

            string query = queryType switch
            {
                EQueryTimeType.YEAR => $"{GetByYear(colName, compareType)} {subQuery}",
                EQueryTimeType.DATE_TIME => $"{GetByDateTime(colName, compareType)} {subQuery}",
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
                DParam.Add("MaxGetCount", maxGetCount);

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

        protected async Task<IEnumerable<TEntity>> GetByRangeDecimalAsync(decimal minDecimal, decimal maxDecimal, string colName, int? maxGetCount = null)
        {
            string query = "";
            if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentOutOfRangeException("Max get count must be greater than zero.", nameof(maxGetCount));
            else if (maxGetCount.HasValue && maxGetCount.Value > 0)
                query = $"{GetRangeDecimalQuery(colName)} LIMIT @MaxGetCount";
            else
                query = GetRangeDecimalQuery(colName);

            try
            {
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { MinDecimal = minDecimal, MaxDecimal = maxDecimal, MaxGetCount = maxGetCount });
                if (results == null || !results.Any())
                    throw new KeyNotFoundException($"No data found in {TableName} for decimal range {minDecimal} - {maxDecimal} in column {colName}");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving data by total decimal range", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByDecimalAsync<TEnum>(decimal decimalValue, TEnum compareType, string colName, int? maxGetCount = null) where TEnum : Enum
        {
            if (compareType is ECompareType type)
            {
                string query = "";
                if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                    throw new ArgumentOutOfRangeException("Max get count must be greater than zero.", nameof(maxGetCount));
                else if (maxGetCount.HasValue && maxGetCount.Value > 0)
                    query = $"{GetCompareDecimalQuery(colName, type)} LIMIT @MaxGetCount";
                else
                    query = GetCompareDecimalQuery(colName, type);

                try
                {
                    using IDbConnection connection = await ConnectionFactory.CreateConnection();
                    IEnumerable<TEntity> results = await connection.QueryAsync<TEntity>(query, new { Decimal = decimalValue, MaxGetCount = maxGetCount });
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
                TEntity result = await connection.QueryFirstOrDefaultAsync<TEntity>(query, new { FirstInput = firstParam, SecondInput = secondParam })
                    ?? throw new KeyNotFoundException($"No data found in {TableName} for {firstColName} = {firstParam} and {secondColName} = {secondParam}");
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
