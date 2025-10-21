using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Core.Interfaces.IValidate
{
    public interface ILogService
    {
        string LogError<TEntity, TCurrentEntityCall>(string message, Exception? ex = null, [CallerMemberName] string? methodCall = null);
        string LogInfo<TEntity, TCurrentEntityCall>(string message, [CallerMemberName] string? methodCall = null);
        string LogWarning<TEntity, TCurrentEntityCall>(string message, [CallerMemberName] string? methodCall = null);
        string LogDebug<TEntity, TCurrentEntityCall>(string message, [CallerMemberName] string? methodCall = null);

        JsonLogEntry JsonLogError<TEntity, TCurrentEntityCall>(string message, Exception? ex = null, int? affectedRows = null, [CallerMemberName] string? methodCall = null);
        JsonLogEntry JsonLogInfo<TEntity, TCurrentEntityCall>(string message, Exception? ex = null, int? affectedRows = null, [CallerMemberName] string? methodCall = null);
        JsonLogEntry JsonLogWarning<TEntity, TCurrentEntityCall>(string message, Exception? ex = null, int? affectedRows = null, [CallerMemberName] string? methodCall = null);
        JsonLogEntry JsonLogDebug<TEntity, TCurrentEntityCall>(string message, Exception? ex = null, int? affectedRows = null, [CallerMemberName] string? methodCall = null);

        #region Save log to files (with optional manual path)
        Task SaveJsonLogToFileAsync(JsonLogEntry log, string? logPath = null, CancellationToken cancellationToken = default);
        Task SaveJsonLogsToFileAsync(IEnumerable<JsonLogEntry> logs, string? logPath = null, CancellationToken cancellationToken = default);
        Task SaveLogStringToFileAsync(string logString, string? logPath = null, CancellationToken cancellationToken = default);

        #endregion

        #region Save log to files (with auto-detect layer from generic type)
        Task SaveJsonLogToFileAsync<TCurrentEntityCall>(JsonLogEntry log, CancellationToken cancellationToken = default);
        Task SaveJsonLogsToFileAsync<TCurrentEntityCall>(IEnumerable<JsonLogEntry> logs, CancellationToken cancellationToken = default);
        Task SaveLogStringToFileAsync<TCurrentEntityCall>(string logMessage, CancellationToken cancellationToken = default);

        #endregion
    }
}
