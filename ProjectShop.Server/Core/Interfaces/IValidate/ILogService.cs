using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Core.Interfaces.IValidate
{
    public interface ILogService
    {
        void LogError<TEntity, TCurrentEntityCall>(string message, Exception? ex);
        void LogInfo<TEntity, TCurrentEntityCall>(string message);
        void LogWarning<TEntity, TCurrentEntityCall>(string message);
        void LogDebug<TEntity, TCurrentEntityCall>(string message);

        JsonLogEntry LogJsonError<TEntity, TCurrentEntityCall>(string message, Exception? ex);
        JsonLogEntry LogJsonInfo<TEntity, TCurrentEntityCall>(string message);
        JsonLogEntry LogJsonWarning<TEntity, TCurrentEntityCall>(string message);
        JsonLogEntry LogJsonDebug<TEntity, TCurrentEntityCall>(string message);
    }
}
