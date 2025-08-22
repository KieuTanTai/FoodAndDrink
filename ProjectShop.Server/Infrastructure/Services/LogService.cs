using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class LogService : ILogService
    {
        public JsonLogEntry LogJsonError<TEntity, TCurrentEntityCall>(string message, Exception? ex)
            => CreateLogEntry<TEntity, TCurrentEntityCall>(message, null, ex);

        public JsonLogEntry LogJsonInfo<TEntity, TCurrentEntityCall>(string message)
            => CreateLogEntry<TEntity, TCurrentEntityCall>(message, "Info");

        public JsonLogEntry LogJsonWarning<TEntity, TCurrentEntityCall>(string message)
            => CreateLogEntry<TEntity, TCurrentEntityCall>(message, "Warning");

        public JsonLogEntry LogJsonDebug<TEntity, TCurrentEntityCall>(string message)
            => CreateLogEntry<TEntity, TCurrentEntityCall>(message, "Debug");

        public void LogError<TEntity, TCurrentEntityCall>(string message, Exception? ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"----- ERROR in {typeof(TCurrentEntityCall).Name} -----");
            Console.WriteLine($"Query Time: {DateTime.UtcNow}");
            Console.WriteLine($"Error Name: {ex?.GetType().Name ?? "Unknown"}");
            Console.WriteLine($"Entity: {typeof(TEntity).Name}");
            Console.WriteLine($"Message: {message}");
            Console.WriteLine($"Error Message: {ex?.Message}");
            Console.WriteLine($"----- END Log Error in {typeof(TCurrentEntityCall).Name} -----\n");
            Console.ResetColor();
        }

        public void LogInfo<TEntity, TCurrentEntityCall>(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"----- INFO in {typeof(TCurrentEntityCall).Name} -----");
            Console.WriteLine($"Query Time: {DateTime.UtcNow}");
            Console.WriteLine($"Entity: {typeof(TEntity).Name}");
            Console.WriteLine($"Message: {message}");
            Console.WriteLine($"----- END Log Info in {typeof(TCurrentEntityCall).Name} -----\n");
            Console.ResetColor();
        }

        public void LogWarning<TEntity, TCurrentEntityCall>(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"----- WARNING in {typeof(TCurrentEntityCall).Name} -----");
            Console.WriteLine($"Query Time: {DateTime.UtcNow}");
            Console.WriteLine($"Entity: {typeof(TEntity).Name}");
            Console.WriteLine($"Message: {message}");
            Console.WriteLine($"----- END Log Warning in {typeof(TCurrentEntityCall).Name} -----\n");
            Console.ResetColor();
        }

        public void LogDebug<TEntity, TCurrentEntityCall>(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"----- DEBUG in {typeof(TCurrentEntityCall).Name} -----");
            Console.WriteLine($"Query Time: {DateTime.UtcNow}");
            Console.WriteLine($"Entity: {typeof(TEntity).Name}");
            Console.WriteLine($"Message: {message}");
            Console.WriteLine($"----- END Log Debug in {typeof(TCurrentEntityCall).Name} -----\n");
            Console.ResetColor();
        }

        private JsonLogEntry CreateLogEntry<TEntity, TCurrentEntityCall>(string message, string? name, Exception? ex = null)
        {
            return new JsonLogEntry
            {
                QueryTime = DateTime.UtcNow,
                EntityCall = typeof(TCurrentEntityCall).Name,
                Entity = typeof(TEntity).Name,
                ErrorName = name ?? ex?.GetType().Name,
                Message = message,
                ErrorMessage = ex?.ToString()
            };
        }
    }
}
