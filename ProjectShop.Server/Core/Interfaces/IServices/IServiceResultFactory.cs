using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IServiceResultFactory<TEntityCall> where TEntityCall : class
    {
        ServiceResult<TEntity> CreateServiceResult<TEntity>(string message, TEntity entity, bool isSuccess, Exception? ex = null, int? affectedRows = null) where TEntity : class;
        ServiceResult<TEntity> CreateServiceResult<TEntity>(TEntity data, IEnumerable<JsonLogEntry> logEntries) where TEntity : class;
        ServiceResults<TEntity> CreateServiceResults<TEntity>(string message, IEnumerable<TEntity> entities, bool isSuccess, Exception? ex = null, int? affectedRows = null) where TEntity : class;
        ServiceResults<TEntity> CreateServiceResults<TEntity>(IEnumerable<TEntity> data, IEnumerable<JsonLogEntry> logEntries) where TEntity : class;
    }
}
