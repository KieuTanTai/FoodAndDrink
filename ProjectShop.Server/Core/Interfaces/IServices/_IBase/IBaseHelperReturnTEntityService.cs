using ProjectShop.Server.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IServices._IBase
{
    public interface IBaseHelperReturnTEntityService<TCurrentCall> where TCurrentCall : class
    {
        Task<ServiceResult<TEntity>> TryLoadEntityAsync<TKey, TEntity>(
            TKey id,
            Func<TKey, Task<TEntity?>> daoFunc,
            Func<TEntity> constructorDelegate,
            [CallerMemberName] string? methodCall = null
        ) where TEntity : class;

        Task<IDictionary<TKey, ServiceResult<TEntity>>> TryLoadEntitiesAsync<TKey, TEntity>(
            IEnumerable<TKey> ids,
            Func<IEnumerable<TKey>, Task<IEnumerable<TEntity>>> daoFunc,
            Func<TEntity> constructorDelegate,
            Func<TEntity, TKey> fieldSelector,
            [CallerMemberName] string? methodCall = null
        ) where TEntity : class where TKey : notnull;

        Task<ServiceResults<TEntity>> TryLoadICollectionEntitiesAsync<TKey, TEntity>(
            TKey id,
            Func<TKey, Task<IEnumerable<TEntity>>> daoFunc,
            [CallerMemberName] string? methodCall = null
        ) where TEntity : class;

        Task<IDictionary<TKey, ServiceResults<TEntity>>> TryLoadICollectionEntitiesAsync<TKey, TEntity>(
            IEnumerable<TKey> ids,
            Func<IEnumerable<TKey>, Task<IEnumerable<TEntity>>> daoFunc,
            Func<TEntity, TKey> fieldSelector,
            [CallerMemberName] string? methodCall = null
        ) where TEntity : class where TKey : notnull;

        Task LoadEntityAsync<TKey, TEntity>(
            Action<TEntity> assignResult,
            Func<TKey> selectorField,
            Func<TKey, Task<ServiceResult<TEntity>>> tryLoadFunc,
            List<JsonLogEntry> logEntries,
            [CallerMemberName] string? methodCall = null
        ) where TEntity : class;

        Task LoadICollectionEntitiesAsync<TKey, TEntity>(
            Action<IEnumerable<TEntity>> assignResult,
            Func<TKey> selectorField,
            Func<TKey, Task<ServiceResults<TEntity>>> tryLoadFunc,
            List<JsonLogEntry> logEntries,
            [CallerMemberName] string? methodCall = null
        ) where TEntity : class;

        Task LoadICollectionEntitiesAsync<TKey, TSource, TEntity>(
            IEnumerable<TSource> sources,
            Action<TSource, IEnumerable<TEntity>> assignResult,
            Func<TSource, TKey> selectorField,
            Func<IEnumerable<TKey>, Task<IDictionary<TKey, ServiceResults<TEntity>>>> tryLoadFunc,
            List<JsonLogEntry> logEntries,
            [CallerMemberName] string? methodCall = null
        ) where TEntity : class where TSource : class;

        Task LoadEntitiesAsync<TKey, TSource, TEntity>(
            IEnumerable<TSource> sources,
            Action<TSource, TEntity> assignResult,
            Func<TSource, TKey> selectorField,
            Func<IEnumerable<TKey>, Task<IDictionary<TKey, ServiceResult<TEntity>>>> tryLoadFunc,
            List<JsonLogEntry> logEntries,
            [CallerMemberName] string? methodCall = null
        ) where TEntity : class;
    }
}
