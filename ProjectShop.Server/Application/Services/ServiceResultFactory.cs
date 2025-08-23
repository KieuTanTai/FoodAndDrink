using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Application.Services
{
    public class ServiceResultFactory<TEntityCall> : IServiceResultFactory<TEntityCall> where TEntityCall : class
    {
        private readonly ILogService _logger;
        public ServiceResultFactory(ILogService logger)
        {
            _logger = logger;
        }

        public ServiceResult<TEntity> CreateServiceResult<TEntity>(string message, TEntity entity, bool isSuccess, Exception? ex, int? affectedRows) where TEntity : class
        {
            List<JsonLogEntry> logEntries = new List<JsonLogEntry>();
            if (isSuccess)
            {
                if (affectedRows == null)
                    logEntries.Add(_logger.JsonLogInfo<TEntity, TEntityCall>(message));
                else
                    logEntries.Add(_logger.JsonLogInfo<TEntity, TEntityCall>(message, affectedRows: affectedRows));
            }
            else if (ex == null)
            {
                if (affectedRows == null)
                    logEntries.Add(_logger.JsonLogWarning<TEntity, TEntityCall>(message));
                else
                    logEntries.Add(_logger.JsonLogWarning<TEntity, TEntityCall>(message, null, affectedRows: affectedRows));
            }
            else
            {
                if (affectedRows == null)
                    logEntries.Add(_logger.JsonLogError<TEntity, TEntityCall>(message, ex));
                else
                    logEntries.Add(_logger.JsonLogError<TEntity, TEntityCall>(message, ex, affectedRows: affectedRows));
            }
            return new ServiceResult<TEntity>
            {
                Data = entity,
                LogEntries = logEntries
            };
        }

        public ServiceResults<TEntity> CreateServiceResults<TEntity>(string message, IEnumerable<TEntity> entities, bool isSuccess, Exception? ex, int? affectedRows) where TEntity : class
        {
            List<JsonLogEntry> logEntries = new List<JsonLogEntry>();
            if (isSuccess)
            {
                if (affectedRows == null)
                    logEntries.Add(_logger.JsonLogInfo<TEntity, TEntityCall>(message));
                else
                    logEntries.Add(_logger.JsonLogInfo<TEntity, TEntityCall>(message, affectedRows: affectedRows));
            }
            else if (ex == null)
            {
                if (affectedRows == null)
                    logEntries.Add(_logger.JsonLogWarning<TEntity, TEntityCall>(message));
                else
                    logEntries.Add(_logger.JsonLogWarning<TEntity, TEntityCall>(message, null, affectedRows: affectedRows));
            }
            else
            {
                if (affectedRows == null)
                    logEntries.Add(_logger.JsonLogError<TEntity, TEntityCall>(message, ex));
                else
                    logEntries.Add(_logger.JsonLogError<TEntity, TEntityCall>(message, ex, affectedRows: affectedRows));
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
