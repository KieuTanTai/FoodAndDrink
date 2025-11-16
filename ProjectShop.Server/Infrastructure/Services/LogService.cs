using ProjectShop.Server.Core.Constants;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class LogService : ILogService
    {
        private readonly JsonSerializerOptions _jsonOptions;
        public LogService()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            // Ensure log directories exist on initialization
            // LogPathConstants.EnsureDirectoriesExist();
        }

        public JsonLogEntry JsonLogError<TEntity, TCurrentEntityCall>(string message, Exception? ex, int? affectedRows, [CallerMemberName] string? methodCall = null)
            => CreateLogEntry<TEntity, TCurrentEntityCall>(message, null, ex, affectedRows, methodCall);

        public JsonLogEntry JsonLogInfo<TEntity, TCurrentEntityCall>(string message, Exception? ex, int? affectedRows, [CallerMemberName] string? methodCall = null)
            => CreateLogEntry<TEntity, TCurrentEntityCall>(message, "Info", ex, affectedRows, methodCall);

        public JsonLogEntry JsonLogWarning<TEntity, TCurrentEntityCall>(string message, Exception? ex, int? affectedRows, [CallerMemberName] string? methodCall = null)
            => CreateLogEntry<TEntity, TCurrentEntityCall>(message, "Warning", ex, affectedRows, methodCall);

        public JsonLogEntry JsonLogDebug<TEntity, TCurrentEntityCall>(string message, Exception? ex, int? affectedRows, [CallerMemberName] string? methodCall = null)
            => CreateLogEntry<TEntity, TCurrentEntityCall>(message, "Debug", ex, affectedRows, methodCall);

        public string LogError<TEntity, TCurrentEntityCall>(string message, Exception? ex, [CallerMemberName] string? methodCall = null)
        {
            string logs = "";
            logs += $"----- ERROR in {typeof(TCurrentEntityCall).FullName} -----\n";
            logs += $"Method Call: {methodCall}\n";
            logs += $"Query Time: {DateTime.UtcNow:dd/MM/yyyy HH:mm:ss}\n";
            logs += $"Entity: {typeof(TEntity).FullName}\n";
            logs += $"Message: {message}\n";
            if (ex != null)
            {
                logs += $"Error Name: {ex.GetType().FullName}\n";
                logs += $"Error Message: {ex.Message}\n";
            }
            logs += $"----- END Log Error in {typeof(TCurrentEntityCall).FullName} -----\n\n";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(logs);
            Console.ResetColor();
            return logs;
        }

        public string LogInfo<TEntity, TCurrentEntityCall>(string message, [CallerMemberName] string? methodCall = null)
        {
            string logs = "";
            logs += $"----- INFO in {typeof(TCurrentEntityCall).FullName} -----\n";
            logs += $"Method Call: {methodCall}\n";
            logs += $"Query Time: {DateTime.UtcNow:dd/MM/yyyy HH:mm:ss}\n";
            logs += $"Entity: {typeof(TEntity).FullName}\n";
            logs += $"Message: {message}\n";
            logs += $"----- END Log Info in {typeof(TCurrentEntityCall).FullName} -----\n\n";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(logs);
            Console.ResetColor();
            return logs;
        }

        public string LogWarning<TEntity, TCurrentEntityCall>(string message, [CallerMemberName] string? methodCall = null)
        {
            string logs = "";
            logs += $"----- WARNING in {typeof(TCurrentEntityCall).FullName} -----\n";
            logs += $"Method Call: {methodCall}\n";
            logs += $"Query Time: {DateTime.UtcNow:dd/MM/yyyy HH:mm:ss}\n";
            logs += $"Entity: {typeof(TEntity).FullName}\n";
            logs += $"Message: {message}\n";
            logs += $"----- END Log Warning in {typeof(TCurrentEntityCall).FullName} -----\n\n";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(logs);
            Console.ResetColor();
            return logs;
        }

        public string LogDebug<TEntity, TCurrentEntityCall>(string message, [CallerMemberName] string? methodCall = null)
        {
            string logs = "";
            logs += $"----- DEBUG in {typeof(TCurrentEntityCall).FullName} -----\n";
            logs += $"Method Call: {methodCall}\n";
            logs += $"Query Time: {DateTime.UtcNow:dd/MM/yyyy HH:mm:ss}\n";
            logs += $"Entity: {typeof(TEntity).FullName}\n";
            logs += $"Message: {message}\n";
            logs += $"----- END Log Debug in {typeof(TCurrentEntityCall).FullName} -----\n\n";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(logs);
            Console.ResetColor();
            return logs;
        }

        private static JsonLogEntry CreateLogEntry<TEntity, TCurrentEntityCall>(string message, string? name, Exception? ex = null, int? affectedRows = null, [CallerMemberName] string? methodCall = null)
        {
            bool isHaveAffectedRows = affectedRows.HasValue;
            if (isHaveAffectedRows && affectedRows < 0)
                affectedRows = null;
            JsonLogEntry log = new()
            {
                EntityCall = typeof(TCurrentEntityCall).FullName,
                MethodCall = methodCall,
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

        public async Task SaveJsonLogToFileAsync(JsonLogEntry log, string? logPath = null, CancellationToken cancellationToken = default)
        {
            try
            {
                logPath = ValidateLogPath([log], logPath);

                if (!Directory.Exists(logPath))
                    Directory.CreateDirectory(logPath);
                string logFileName = $"log_{DateTime.UtcNow:yyyyMMdd}.json";
                string logFilePath = Path.Combine(logPath, logFileName);

                using FileStream stream = new(logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
                await JsonSerializer.SerializeAsync(stream, log, _jsonOptions, cancellationToken);
                await stream.WriteAsync(Encoding.UTF8.GetBytes(Environment.NewLine), cancellationToken);
            }
            catch (TaskCanceledException)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Log saving was canceled.");
                Console.ResetColor();
            }
            catch (OperationCanceledException)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Log saving was canceled.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed to save log to file. Exception: {ex.Message}");
                Console.ResetColor();
            }
        }

        public async Task SaveJsonLogsToFileAsync(IEnumerable<JsonLogEntry> logs, string? logPath = null, CancellationToken cancellationToken = default)
        {
            try
            {
                logPath = ValidateLogPath(logs, logPath);

                if (!Directory.Exists(logPath))
                    Directory.CreateDirectory(logPath);
                string logFileName = $"log_{DateTime.UtcNow:yyyyMMdd}.json";
                string logFilePath = Path.Combine(logPath, logFileName);

                using FileStream stream = new(logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
                foreach (var log in logs)
                {
                    await JsonSerializer.SerializeAsync(stream, log, _jsonOptions, cancellationToken);
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(Environment.NewLine), cancellationToken);
                }
            }
            catch (TaskCanceledException)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Log saving was canceled.");
                Console.ResetColor();
            }
            catch (OperationCanceledException)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Log saving was canceled.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed to save logs to file. Exception: {ex.Message}");
                Console.ResetColor();
            }
        }

        public async Task SaveLogStringToFileAsync(string logMessage, string? logPath = null, CancellationToken cancellationToken = default)
        {
            try
            {
                // Use base log directory if not provided
                if (string.IsNullOrEmpty(logPath))
                    logPath = LogPathConstants.BaseLogDirectory;

                if (!Directory.Exists(logPath))
                    Directory.CreateDirectory(logPath);

                string logFileName = $"console_log_{DateTime.UtcNow:yyyyMMdd}.txt";
                string logFilePath = Path.Combine(logPath, logFileName);

                using FileStream stream = new(logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
                await stream.WriteAsync(Encoding.UTF8.GetBytes(logMessage), cancellationToken);
                await stream.WriteAsync(Encoding.UTF8.GetBytes(Environment.NewLine), cancellationToken);
            }
            catch (TaskCanceledException)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Console log saving was canceled.");
                Console.ResetColor();
            }
            catch (OperationCanceledException)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Console log saving was canceled.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed to save console log to file. Exception: {ex.Message}");
                Console.ResetColor();
            }
        }

        #region Overload methods with auto-detect layer from generic type

        public async Task SaveJsonLogToFileAsync<TCurrentEntityCall>(JsonLogEntry log, CancellationToken cancellationToken = default)
        {
            string logPath = GetLogPathByLayer<TCurrentEntityCall>();
            await SaveJsonLogToFileAsync(log, logPath, cancellationToken);
        }

        public async Task SaveJsonLogsToFileAsync<TCurrentEntityCall>(IEnumerable<JsonLogEntry> logs, CancellationToken cancellationToken = default)
        {
            string logPath = GetLogPathByLayer<TCurrentEntityCall>();
            await SaveJsonLogsToFileAsync(logs, logPath, cancellationToken);
        }

        public async Task SaveLogStringToFileAsync<TCurrentEntityCall>(string logMessage, CancellationToken cancellationToken = default)
        {
            string logPath = GetLogPathByLayer<TCurrentEntityCall>();
            await SaveLogStringToFileAsync(logMessage, logPath, cancellationToken);
        }

        #endregion

        #region Helper methods for log path detection

        private static string GetLogPathByLayer<TCurrentEntityCall>()
        {
            string? fullName = typeof(TCurrentEntityCall).FullName;

            if (string.IsNullOrEmpty(fullName))
                return LogPathConstants.BaseLogDirectory;

            // Check namespace to determine layer
            if (fullName.Contains(".Infrastructure.Repositories", StringComparison.OrdinalIgnoreCase))
                return LogPathConstants.RepositoryLogPath;

            if (fullName.Contains(".Infrastructure.Persistence", StringComparison.OrdinalIgnoreCase))
                return LogPathConstants.DatabaseLogPath;

            if (fullName.Contains(".Infrastructure", StringComparison.OrdinalIgnoreCase))
                return LogPathConstants.InfrastructureLogPath;

            if (fullName.Contains(".Application.Services", StringComparison.OrdinalIgnoreCase))
                return LogPathConstants.ServiceLogPath;

            if (fullName.Contains(".Application.Validations", StringComparison.OrdinalIgnoreCase))
                return LogPathConstants.ValidationLogPath;

            if (fullName.Contains(".Application", StringComparison.OrdinalIgnoreCase))
                return LogPathConstants.ApplicationLogPath;

            if (fullName.Contains(".WebAPI.Controllers", StringComparison.OrdinalIgnoreCase))
                return LogPathConstants.ControllerLogPath;

            if (fullName.Contains(".WebAPI.Middlewares", StringComparison.OrdinalIgnoreCase))
                return LogPathConstants.MiddlewareLogPath;

            if (fullName.Contains(".WebAPI", StringComparison.OrdinalIgnoreCase))
                return LogPathConstants.WebAPILogPath;

            // Default fallback
            return LogPathConstants.BaseLogDirectory;
        }

        private static string ValidateLogPath(IEnumerable<JsonLogEntry> logs, string? logPath)
        {
            // Auto-detect log path from first log entry if not provided
            if (string.IsNullOrEmpty(logPath))
            {
                var firstLog = logs.FirstOrDefault();
                if (firstLog != null)
                {
                    string layerPath = LogPathConstants.BaseLogDirectory;
                    string? entityCallStr = firstLog.EntityCall?.ToString();

                    if (!string.IsNullOrEmpty(entityCallStr))
                    {
                        if (entityCallStr.Contains(".Infrastructure.Repositories", StringComparison.OrdinalIgnoreCase))
                            layerPath = LogPathConstants.RepositoryLogPath;
                        else if (entityCallStr.Contains(".Infrastructure.Persistence", StringComparison.OrdinalIgnoreCase))
                            layerPath = LogPathConstants.DatabaseLogPath;
                        else if (entityCallStr.Contains(".Infrastructure", StringComparison.OrdinalIgnoreCase))
                            layerPath = LogPathConstants.InfrastructureLogPath;
                        else if (entityCallStr.Contains(".Application.Services", StringComparison.OrdinalIgnoreCase))
                            layerPath = LogPathConstants.ServiceLogPath;
                        else if (entityCallStr.Contains(".Application.Validations", StringComparison.OrdinalIgnoreCase))
                            layerPath = LogPathConstants.ValidationLogPath;
                        else if (entityCallStr.Contains(".Application", StringComparison.OrdinalIgnoreCase))
                            layerPath = LogPathConstants.ApplicationLogPath;
                        else if (entityCallStr.Contains(".WebAPI.Controllers", StringComparison.OrdinalIgnoreCase))
                            layerPath = LogPathConstants.ControllerLogPath;
                        else if (entityCallStr.Contains(".WebAPI.Middlewares", StringComparison.OrdinalIgnoreCase))
                            layerPath = LogPathConstants.MiddlewareLogPath;
                        else if (entityCallStr.Contains(".WebAPI", StringComparison.OrdinalIgnoreCase))
                            layerPath = LogPathConstants.WebAPILogPath;
                    }
                    logPath = layerPath;
                }
                else
                {
                    logPath = LogPathConstants.BaseLogDirectory;
                }
            }
            return logPath;
        }

        #endregion
    }
}
