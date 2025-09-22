using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services
{
    public class BaseHelperReturnTEntityService<TCurrentCall> : IBaseHelperReturnTEntityService<TCurrentCall>
        where TCurrentCall : class
    {
        private readonly IServiceResultFactory<TCurrentCall> _serviceResultFactory;
        private readonly ILogService _logger;

        public BaseHelperReturnTEntityService(IServiceResultFactory<TCurrentCall> serviceResultFactory, ILogService logger)
        {
            _serviceResultFactory = serviceResultFactory;
            _logger = logger;
        }

        // DRY:
        // 1. Load một entity theo id generic
        public async Task<ServiceResult<TEntity>> TryLoadEntityAsync<TKey, TEntity>(
            TKey id,
            Func<TKey, Task<TEntity?>> daoFunc,
            Func<TEntity> constructorDelegate,
            [CallerMemberName] string? methodCall = null
        ) where TEntity : class
        {
            ServiceResult<TEntity> serviceResult = new(true);
            try
            {
                TEntity? entity = await daoFunc(id);
                bool isTEntityFound = entity != null;
                entity ??= constructorDelegate();
                string message = isTEntityFound ? $"Successfully retrieved entity : {id}." : $"TEntity not found for id: {id}.";
                return _serviceResultFactory.CreateServiceResult(message, entity, isTEntityFound, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult(
                    $"Error occurred while retrieving entity for id: {id}.", constructorDelegate(), false, ex, methodCall: methodCall);
            }
        }

        // 2. Load nhiều entity theo danh sách id generic
        public async Task<IDictionary<TKey, ServiceResult<TEntity>>> TryLoadEntitiesAsync<TKey, TEntity>(
            IEnumerable<TKey> ids,
            Func<IEnumerable<TKey>, Task<IEnumerable<TEntity>>> daoFunc,
            Func<TEntity> constructorDelegate,
            Func<TEntity, TKey> fieldSelector,
            [CallerMemberName] string? methodCall = null
        ) where TEntity : class where TKey : notnull
        {
            if (!ids.Any() || ids.FirstOrDefault() == null)
                return new Dictionary<TKey, ServiceResult<TEntity>>();
            TKey firstId = ids.FirstOrDefault()!;
            try
            {
                IEnumerable<TEntity> entities = await daoFunc(ids) ?? [];
                if (!entities.Any())
                {
                    return new Dictionary<TKey, ServiceResult<TEntity>>()
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResult(
                            $"No entities found for given ids.", constructorDelegate(), false, methodCall: methodCall)
                    };
                }

                return entities.ToDictionary(fieldSelector,
                    entity => _serviceResultFactory.CreateServiceResult(
                        $"Successfully retrieved entities for given id: {fieldSelector(entity)}.",
                        entity, true, methodCall: methodCall));
            }
            catch (Exception ex)
            {
                return new Dictionary<TKey, ServiceResult<TEntity>>()
                {
                    [firstId] = _serviceResultFactory.CreateServiceResult(
                        $"Error occurred while retrieving entities for given ids.", constructorDelegate(), false, ex, methodCall: methodCall)
                };
            }
        }

        // 3. Load ICollection entity theo id generic
        public async Task<ServiceResults<TEntity>> TryLoadICollectionEntitiesAsync<TKey, TEntity>(
            TKey id,
            Func<TKey, Task<IEnumerable<TEntity>>> daoFunc,
            [CallerMemberName] string? methodCall = null
        ) where TEntity : class
        {
            List<JsonLogEntry> logEntries = [];
            try
            {
                IEnumerable<TEntity> entities = await daoFunc(id) ?? [];
                if (!entities.Any())
                {
                    logEntries.Add(_logger.JsonLogWarning<TEntity, TCurrentCall>(
                        $"No entities found for given id: {id}.", methodCall: methodCall));
                    return _serviceResultFactory.CreateServiceResults(entities, logEntries, false);
                }
                logEntries.Add(_logger.JsonLogInfo<TEntity, TCurrentCall>(
                    $"Successfully retrieved entities for given id: {id}.", methodCall: methodCall));
                return _serviceResultFactory.CreateServiceResults(entities, logEntries, true);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults(
                    $"Error occurred while retrieving entities for given id: {id}.", Array.Empty<TEntity>(), false, ex, methodCall: methodCall);
            }
        }

        // 4. Load nhiều ICollection entity theo danh sách id generic
        public async Task<IDictionary<TKey, ServiceResults<TEntity>>> TryLoadICollectionEntitiesAsync<TKey, TEntity>(
            IEnumerable<TKey> ids,
            Func<IEnumerable<TKey>, Task<IEnumerable<TEntity>>> daoFunc,
            Func<TEntity, TKey> fieldSelector,
            [CallerMemberName] string? methodCall = null
        ) where TEntity : class where TKey : notnull
        {
            if (!ids.Any() || ids.FirstOrDefault() == null)
                return new Dictionary<TKey, ServiceResults<TEntity>>();
            TKey firstId = ids.FirstOrDefault()!;
            List<JsonLogEntry> logEntries = [];
            try
            {
                IEnumerable<TEntity> entities = await daoFunc(ids);
                if (entities == null || !entities.Any())
                {
                    logEntries.Add(_logger.JsonLogWarning<TEntity, TCurrentCall>(
                        $"No entities found for given ids.", methodCall: methodCall));
                    return new Dictionary<TKey, ServiceResults<TEntity>>()
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResults<TEntity>([], logEntries, false)
                    };
                }
                logEntries.Add(_logger.JsonLogInfo<TEntity, TCurrentCall>(
                    $"Successfully retrieved entities for given ids.", methodCall: methodCall));
                return entities.GroupBy(fieldSelector).ToDictionary(group => group.Key,
                    group => _serviceResultFactory.CreateServiceResults(group, logEntries, true));
            }
            catch (Exception ex)
            {
                return new Dictionary<TKey, ServiceResults<TEntity>>()
                {
                    [firstId] = _serviceResultFactory.CreateServiceResults(
                        $"Error occurred while retrieving entities for given ids.", Array.Empty<TEntity>(), false, ex, methodCall: methodCall)
                };
            }
        }

        //DRY helper:
        public async Task LoadEntityAsync<TKey, TEntity>(Action<TEntity> assignResult, Func<TKey> selectorField, Func<TKey, Task<ServiceResult<TEntity>>> tryLoadFunc,
            List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null) where TEntity : class
        {
            var result = (await tryLoadFunc(selectorField())) ?? new ServiceResult<TEntity>(false);
            logEntries.AddRange(result.LogEntries ?? []);
            if (result.Data != null)
                assignResult(result.Data);
            else
                logEntries.Add(_logger.JsonLogWarning<TEntity, TCurrentCall>($"TEntity not found for id {selectorField()}.",
                    methodCall: methodCall));
        }

        public async Task LoadICollectionEntitiesAsync<TKey, TEntity>(Action<IEnumerable<TEntity>> assignResult, Func<TKey> selectorField, Func<TKey, Task<ServiceResults<TEntity>>> tryLoadFunc,
            List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null) where TEntity : class
        {
            var result = await tryLoadFunc(selectorField());
            logEntries.AddRange(result.LogEntries ?? []);
            if (result != null && result.Data != null && result.Data.Any())
                assignResult(result.Data);
            else
                logEntries.Add(_logger.JsonLogWarning<TEntity, TCurrentCall>($"No entities found for id {selectorField()}.",
                    methodCall: methodCall));
        }

        public async Task LoadICollectionEntitiesAsync<TKey, TSource, Entity>(IEnumerable<TSource> sources, Action<TSource, IEnumerable<Entity>> assignResult,
            Func<TSource, TKey> selectorField,
            Func<IEnumerable<TKey>, Task<IDictionary<TKey, ServiceResults<Entity>>>> tryLoadFunc,
            List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null) where Entity : class where TSource : class
        {
            if (sources == null || !sources.Any())
                return;
            var sourceList = sources.ToList();
            IEnumerable<TKey> ids = sourceList.Select(selectorField).Distinct();
            var results = await tryLoadFunc(ids);
            if (results == null || !results.Any())
                return;

            foreach (var source in sourceList)
            {
                if (!results.TryGetValue(selectorField(source), out var serviceResults) || serviceResults == null || serviceResults.Data == null)
                    continue;
                logEntries.AddRange(serviceResults.LogEntries ?? []);
                assignResult(source, serviceResults.Data);
            }
            logEntries.Add(_logger.JsonLogInfo<Entity, TCurrentCall>($"Loaded ICollection entities for sources.", methodCall: methodCall));
        }

        public async Task LoadEntitiesAsync<TKey, TSource, Entity>(IEnumerable<TSource> sources, Action<TSource, Entity> assignResult,
            Func<TSource, TKey> selectorField,
            Func<IEnumerable<TKey>, Task<IDictionary<TKey, ServiceResult<Entity>>>> tryLoadFunc,
            List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null) where Entity : class
        {
            if (sources == null || !sources.Any())
                return;
            var sourceList = sources.ToList();
            IEnumerable<TKey> ids = sourceList.Select(selectorField).Distinct();
            var results = await tryLoadFunc(ids);
            if (results == null || !results.Any())
                return;

            foreach (var source in sourceList)
            {
                if (!results.TryGetValue(selectorField(source), out var serviceResult) || serviceResult == null || serviceResult.Data == null)
                    continue;
                logEntries.AddRange(serviceResult.LogEntries ?? []);
                assignResult(source, serviceResult.Data);
            }
            logEntries.Add(_logger.JsonLogInfo<Entity, TCurrentCall>($"Loaded entities for sources.", methodCall: methodCall));
        }
    }
}
