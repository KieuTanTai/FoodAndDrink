using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Core.Interfaces.IValidate
{
    public interface ILogService
    {
        void LogError<TEntity, TCurrentEntityCall>(string message, Exception? ex);
        void LogInfo<TEntity, TCurrentEntityCall>(string message);
        void LogWarning<TEntity, TCurrentEntityCall>(string message);
        void LogDebug<TEntity, TCurrentEntityCall>(string message);

        JsonLogEntry JsonLogError<TEntity, TCurrentEntityCall>(string message, Exception? ex = null, int? affectedRows = null);
        JsonLogEntry JsonLogInfo<TEntity, TCurrentEntityCall>(string message, Exception? ex = null, int? affectedRows = null);
        JsonLogEntry JsonLogWarning<TEntity, TCurrentEntityCall>(string message, Exception? ex = null, int? affectedRows = null);
        JsonLogEntry JsonLogDebug<TEntity, TCurrentEntityCall>(string message, Exception?ex = null, int? affectedRows = null);
    }
}
