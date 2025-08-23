using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class LogService : ILogService
    {
        public JsonLogEntry JsonLogError<TEntity, TCurrentEntityCall>(string message, Exception? ex, int? affectedRows)
            => CreateLogEntry<TEntity, TCurrentEntityCall>(message, null, ex, affectedRows);

        public JsonLogEntry JsonLogInfo<TEntity, TCurrentEntityCall>(string message, Exception? ex, int? affectedRows)
            => CreateLogEntry<TEntity, TCurrentEntityCall>(message, "Info", ex, affectedRows);

        public JsonLogEntry JsonLogWarning<TEntity, TCurrentEntityCall>(string message, Exception? ex, int? affectedRows)
            => CreateLogEntry<TEntity, TCurrentEntityCall>(message, "Warning", ex, affectedRows);

        public JsonLogEntry JsonLogDebug<TEntity, TCurrentEntityCall>(string message, Exception? ex, int? affectedRows)
            => CreateLogEntry<TEntity, TCurrentEntityCall>(message, "Debug", ex, affectedRows);

        public void LogError<TEntity, TCurrentEntityCall>(string message, Exception? ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"----- ERROR in {typeof(TCurrentEntityCall).FullName} -----");
            Console.WriteLine($"Query Time: {DateTime.UtcNow}");
            Console.WriteLine($"Error Name: {ex?.GetType().FullName ?? "Unknown"}");
            Console.WriteLine($"Entity: {typeof(TEntity).FullName}");
            Console.WriteLine($"Message: {message}");
            Console.WriteLine($"Error Message: {ex?.Message}");
            Console.WriteLine($"----- END Log Error in {typeof(TCurrentEntityCall).FullName} -----\n");
            Console.ResetColor();
        }

        public void LogInfo<TEntity, TCurrentEntityCall>(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"----- INFO in {typeof(TCurrentEntityCall).FullName} -----");
            Console.WriteLine($"Query Time: {DateTime.UtcNow}");
            Console.WriteLine($"Entity: {typeof(TEntity).FullName}");
            Console.WriteLine($"Message: {message}");
            Console.WriteLine($"----- END Log Info in {typeof(TCurrentEntityCall).FullName} -----\n");
            Console.ResetColor();
        }

        public void LogWarning<TEntity, TCurrentEntityCall>(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"----- WARNING in {typeof(TCurrentEntityCall).FullName} -----");
            Console.WriteLine($"Query Time: {DateTime.UtcNow}");
            Console.WriteLine($"Entity: {typeof(TEntity).FullName}");
            Console.WriteLine($"Message: {message}");
            Console.WriteLine($"----- END Log Warning in {typeof(TCurrentEntityCall).FullName} -----\n");
            Console.ResetColor();
        }

        public void LogDebug<TEntity, TCurrentEntityCall>(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"----- DEBUG in {typeof(TCurrentEntityCall).FullName} -----");
            Console.WriteLine($"Query Time: {DateTime.UtcNow}");
            Console.WriteLine($"Entity: {typeof(TEntity).FullName}");
            Console.WriteLine($"Message: {message}");
            Console.WriteLine($"----- END Log Debug in {typeof(TCurrentEntityCall).FullName} -----\n");
            Console.ResetColor();
        }

        private JsonLogEntry CreateLogEntry<TEntity, TCurrentEntityCall>(string message, string? name, Exception? ex = null, int? affectedRows = null)
        {
            bool isHaveAffectedRows = affectedRows.HasValue;
            if (isHaveAffectedRows && affectedRows < 0)
                affectedRows = null;
            JsonLogEntry log = new JsonLogEntry
            {
                QueryTime = DateTime.UtcNow,
                EntityCall = typeof(TCurrentEntityCall).FullName,
                Entity = typeof(TEntity).FullName,
                Message = message
            };

            if (ex != null)
            {
                log.ErrorName = ex.GetType().FullName;
                log.ErrorMessage = ex.Message;
                if (isHaveAffectedRows)
                    log.AffectedRows = affectedRows;
            }
            else if (isHaveAffectedRows)
                log.AffectedRows = affectedRows;

            return log;
        }
    }
}
