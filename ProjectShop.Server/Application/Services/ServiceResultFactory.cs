using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services
{
    public class ServiceResultFactory<TEntityCall> : IServiceResultFactory<TEntityCall> where TEntityCall : class
    {
        private readonly ILogService _logger;
        public ServiceResultFactory(ILogService logger)
        {
            _logger = logger;
        }

        public ServiceResult<TEntity> CreateServiceResult<TEntity>(string message, TEntity entity, bool isSuccess, Exception? ex, int? affectedRows, [CallerMemberName] string? methodCall = null) where TEntity : class
        {
            List<JsonLogEntry> logEntries = new List<JsonLogEntry>();
            if (isSuccess)
            {
                if (affectedRows == null)
                    logEntries.Add(_logger.JsonLogInfo<TEntity, TEntityCall>(message, methodCall: methodCall));
                else
                    logEntries.Add(_logger.JsonLogInfo<TEntity, TEntityCall>(message, affectedRows: affectedRows, methodCall: methodCall));
            }
            else if (ex == null)
            {
                if (affectedRows == null)
                    logEntries.Add(_logger.JsonLogWarning<TEntity, TEntityCall>(message, methodCall: methodCall));
                else
                    logEntries.Add(_logger.JsonLogWarning<TEntity, TEntityCall>(message, null, affectedRows: affectedRows, methodCall: methodCall   ));
            }
            else
            {
                if (affectedRows == null)
                    logEntries.Add(_logger.JsonLogError<TEntity, TEntityCall>(message, ex, methodCall: methodCall));
                else
                    logEntries.Add(_logger.JsonLogError<TEntity, TEntityCall>(message, ex, affectedRows: affectedRows, methodCall: methodCall));
            }
            return new ServiceResult<TEntity>
            {
                Data = entity,
                LogEntries = logEntries
            };
        }

        public ServiceResults<TEntity> CreateServiceResults<TEntity>(string message, IEnumerable<TEntity> entities, bool isSuccess, Exception? ex, int? affectedRows, [CallerMemberName] string? methodCall = null) where TEntity : class
        {
            List<JsonLogEntry> logEntries = new List<JsonLogEntry>();
            if (isSuccess)
            {
                if (affectedRows == null)
                    logEntries.Add(_logger.JsonLogInfo<TEntity, TEntityCall>(message, methodCall: methodCall));
                else
                    logEntries.Add(_logger.JsonLogInfo<TEntity, TEntityCall>(message, affectedRows: affectedRows, methodCall: methodCall));
            }
            else if (ex == null)
            {
                if (affectedRows == null)
                    logEntries.Add(_logger.JsonLogWarning<TEntity, TEntityCall>(message, methodCall: methodCall));
                else
                    logEntries.Add(_logger.JsonLogWarning<TEntity, TEntityCall>(message, null, affectedRows: affectedRows, methodCall: methodCall));
            }
            else
            {
                if (affectedRows == null)
                    logEntries.Add(_logger.JsonLogError<TEntity, TEntityCall>(message, ex, methodCall: methodCall));
                else
                    logEntries.Add(_logger.JsonLogError<TEntity, TEntityCall>(message, ex, affectedRows: affectedRows, methodCall: methodCall));
            }
            return new ServiceResults<TEntity>
            {
                Data = entities?.ToList(),
                LogEntries = logEntries
            };
        }

        public ServiceResult<TEntity> CreateServiceResult<TEntity>(TEntity data, IEnumerable<JsonLogEntry> logEntries) where TEntity : class
        {
            return new ServiceResult<TEntity>
            {
                Data = data,
                LogEntries = logEntries
            };
        }

        public ServiceResults<TEntity> CreateServiceResults<TEntity>(IEnumerable<TEntity> data, IEnumerable<JsonLogEntry> logEntries) where TEntity : class
        {
            return new ServiceResults<TEntity>
            {
                Data = data?.ToList(),
                LogEntries = logEntries
            };
        }
    }
}
