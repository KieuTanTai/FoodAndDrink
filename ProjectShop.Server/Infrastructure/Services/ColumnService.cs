using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Infrastructure.Configuration;
using System.Data;
using System.Data.Common;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class ColumnService(IDbConnectionFactory connectionFactory) : IColumnService
    {
        private readonly IDbConnectionFactory _connectionFactory = connectionFactory;
        private readonly Dictionary<string, HashSet<string>> _columnCache = new(StringComparer.OrdinalIgnoreCase);

        public async Task<List<string>> GetValidColumns(string tableName)
        {
            try
            {
                if (_columnCache.TryGetValue(tableName, out var cached))
                {
                    return cached.ToList();
                }

                using IDbConnection connection = await _connectionFactory.CreateConnection();

                if (connection is not DbConnection dbConnection)
                    throw new InvalidOperationException("Connection must inherit from DbConnection to use GetSchema.");

                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                var restrictions = new string[] { "", "", tableName, "" };
                DataTable schema = dbConnection.GetSchema("Columns", restrictions);

                List<string> columns = new();
                foreach (DataRow row in schema.Rows)
                {

                    object dataRow = row["COLUMN_NAME"];
                    if (dataRow is not null)
                    {
                        string columnName = dataRow.ToString()?.Trim() ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(columnName))
                            columns.Add(columnName);
                    }
                }

                var columnSet = new HashSet<string>(columns, StringComparer.OrdinalIgnoreCase);
                _columnCache[tableName] = columnSet;

                return columns;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetValidColumns] Error for table '{tableName}: \n{ex.Message}");
                return new();
            }
        }

        public async Task<bool> IsValidColumn(string tableName, string columnName)
        {
            if (!_columnCache.TryGetValue(tableName, out var cols))
            {
                var colList = await GetValidColumns(tableName);
                cols = new HashSet<string>(colList, StringComparer.OrdinalIgnoreCase);
                _columnCache[tableName] = cols;
            }

            return SqlWhitelist.IsSafeColumn(columnName) && cols.Contains(columnName);
        }

    }
}
