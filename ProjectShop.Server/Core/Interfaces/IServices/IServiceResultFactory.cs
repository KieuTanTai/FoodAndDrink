using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IServiceResultFactory<TEntityCall> where TEntityCall : class
    {
        ServiceResult<TEntity> CreateServiceResult<TEntity>(string message, TEntity entity, bool isSuccess, Exception? ex = null, int? affectedRows = null, [CallerMemberName] string? methodCall = null) where TEntity : class;
        ServiceResult<TEntity> CreateServiceResult<TEntity>(TEntity data, IEnumerable<JsonLogEntry> logEntries, bool isSuccess = true) where TEntity : class;
        ServiceResults<TEntity> CreateServiceResults<TEntity>(string message, IEnumerable<TEntity> entities, bool isSuccess, Exception? ex = null, int? affectedRows = null, [CallerMemberName] string? methodCall = null) where TEntity : class;
        ServiceResults<TEntity> CreateServiceResults<TEntity>(IEnumerable<TEntity> data, IEnumerable<JsonLogEntry> logEntries, bool isSuccess = true) where TEntity : class;
    }
}
