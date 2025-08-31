using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Core.Interfaces.IValidate
{
    public interface ILogService
    {
        void LogError<TEntity, TCurrentEntityCall>(string message, Exception? ex = null, [CallerMemberName] string? methodCall = null);
        void LogInfo<TEntity, TCurrentEntityCall>(string message, [CallerMemberName] string? methodCall = null);
        void LogWarning<TEntity, TCurrentEntityCall>(string message, [CallerMemberName] string? methodCall = null);
        void LogDebug<TEntity, TCurrentEntityCall>(string message, [CallerMemberName] string? methodCall = null);

        JsonLogEntry JsonLogError<TEntity, TCurrentEntityCall>(string message, Exception? ex = null, int? affectedRows = null, [CallerMemberName] string? methodCall = null);
        JsonLogEntry JsonLogInfo<TEntity, TCurrentEntityCall>(string message, Exception? ex = null, int? affectedRows = null, [CallerMemberName] string? methodCall = null);
        JsonLogEntry JsonLogWarning<TEntity, TCurrentEntityCall>(string message, Exception? ex = null, int? affectedRows = null, [CallerMemberName] string? methodCall = null);
        JsonLogEntry JsonLogDebug<TEntity, TCurrentEntityCall>(string message, Exception?ex = null, int? affectedRows = null, [CallerMemberName] string? methodCall = null);
    }
}
