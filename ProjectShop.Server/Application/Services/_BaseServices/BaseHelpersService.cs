using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services._BaseServices
{
    public class BaseHelperService<TEntity> : IBaseHelperServices<TEntity>
        where TEntity : class
    {
        private readonly IHashPassword _hashPassword;
        private readonly IStringConverter _converter;
        private readonly IClock _clock;
        private readonly IMaxGetRecord _maxGetRecord;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<BaseHelperService<TEntity>> _serviceResultFactory;

        public BaseHelperService(
            IHashPassword hashPassword,
            IStringConverter converter,
            IClock clock,
            IMaxGetRecord maxGetRecord,
            ILogService logger,
            IServiceResultFactory<BaseHelperService<TEntity>> serviceResultFactory)
        {
            _clock = clock;
            _logger = logger;
            _converter = converter;
            _hashPassword = hashPassword;
            _maxGetRecord = maxGetRecord;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<bool> IsExistObject(string input, Func<string, Task<TEntity?>> daoFunc)
        {
            try
            {
                TEntity? existingObject = await daoFunc(input);
                return existingObject != null;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object with input: {input}", ex);
                return false;
            }
        }

        public async Task<bool> IsExistObject(uint input, Func<uint, Task<TEntity?>> daoFunc)
        {
            try
            {
                TEntity? existingObject = await daoFunc(input);
                return existingObject != null;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object with input: {input}", ex);
                return false;
            }
        }

        public async Task<bool> IsExistObject<TKey>(TKey keys, Func<TKey, Task<TEntity?>> daoFunc) where TKey : struct
        {
            try
            {
                TEntity? existingObject = await daoFunc(keys);
                return existingObject != null;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object with key: {keys}", ex);
                return false;
            }
        }

        public async Task<bool> DoAllIdsExistAsync(IEnumerable<string> ids, Func<IEnumerable<string>, Task<IEnumerable<TEntity>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids);
                return existingObjects.Count() == ids.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object!", ex);
                return false;
            }
        }

        public async Task<bool> DoAllIdsExistAsync(IEnumerable<uint> ids, Func<IEnumerable<uint>, Task<IEnumerable<TEntity>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids);
                return existingObjects.Count() == ids.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object!", ex);
                return false;
            }
        }

        public async Task<bool> DoAllKeysExistAsync<TKey>(IEnumerable<TKey> keys, Func<IEnumerable<TKey>, int?, Task<IEnumerable<TEntity>>> daoFunc) where TKey : struct
        {
            if (keys == null || !keys.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(keys, null);
                return existingObjects.Count() == keys.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object!", ex);
                return false;
            }
        }

        public async Task<bool> DoNoneOfIdsExistAsync(IEnumerable<string> ids, Func<IEnumerable<string>, Task<IEnumerable<TEntity>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids);
                return !existingObjects.Any();
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object!", ex);
                return true;
            }
        }

        public async Task<bool> DoNoneOfIdsExistAsync(IEnumerable<uint> ids, Func<IEnumerable<uint>, Task<IEnumerable<TEntity>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids);
                return !existingObjects.Any();
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object!", ex);
                return true;
            }
        }

        public async Task<bool> DoNoneOfKeysExistAsync<TKey>(IEnumerable<TKey> keys, Func<IEnumerable<TKey>, int?, Task<IEnumerable<TEntity>>> daoFunc) where TKey : struct
        {
            if (keys == null || !keys.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(keys, null);
                return !existingObjects.Any();
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object!", ex);
                return true;
            }
        }

        public int? GetValidMaxRecord(int? maxGetCount)
        {
            if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                return _maxGetRecord.MaxGetRecord;
            return maxGetCount > _maxGetRecord.MaxGetRecord ? _maxGetRecord.MaxGetRecord : maxGetCount;
        }

        public async Task<Dictionary<uint, ServiceResults<TEntity>>> FilterValidEntities(IEnumerable<TEntity> entities,
            Func<TEntity, string> fieldSelector,
            Func<IEnumerable<string>, int?, Task<IEnumerable<TEntity>>> daoFunc, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                var entityList = entities.ToList();
                var fieldValues = entityList.Select(fieldSelector).ToList();
                var existingEntities = await daoFunc(fieldValues, null);

                if (existingEntities == null || !existingEntities.Any())
                    return new Dictionary<uint, ServiceResults<TEntity>>
                    {
                        { 0, _serviceResultFactory.CreateServiceResults<TEntity>(entityList, [.. entityList.Select(_
                            => _logger.JsonLogInfo<TEntity, BaseHelperService<TEntity>>("Success", methodCall: methodCall))], true) }
                    };
                existingEntities = [.. existingEntities];

                var existingFieldSet = new HashSet<string>(existingEntities.Select(fieldSelector), StringComparer.OrdinalIgnoreCase);
                var data = entityList
                    .Where(entity => !existingFieldSet.Contains(fieldSelector(entity)))
                    .ToList();

                var logEntries = CreateLogsByFilterEntities<TEntity, BaseHelperService<TEntity>>(entityList, existingFieldSet, fieldSelector).ToList();
                return new Dictionary<uint, ServiceResults<TEntity>>
                {
                    { 0, _serviceResultFactory.CreateServiceResults<TEntity>(data, logEntries, true) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object for filter!", ex, methodCall);
                List<JsonLogEntry> logEntries = [_logger.JsonLogError<TEntity, BaseHelperService<TEntity>>($"An error occurred while filtering valid entities.", ex, methodCall: methodCall)];

                return new Dictionary<uint, ServiceResults<TEntity>>
                {
                    { 0, _serviceResultFactory.CreateServiceResults<TEntity>([], logEntries, false) }
                };
            }
        }

        public async Task<Dictionary<uint, ServiceResults<TEntity>>> FilterValidEntities<TKey>(
            IEnumerable<TEntity> entities,
            Func<TEntity, TKey> fieldSelector,
            Func<IEnumerable<TKey>, int?, Task<IEnumerable<TEntity>>> daoFunc, [CallerMemberName] string? methodCall = null) where TKey : struct
        {
            try
            {
                var entityList = entities.ToList();
                var fieldValues = entityList.Select(fieldSelector).ToList();
                var existingEntities = await daoFunc(fieldValues, null);

                if (existingEntities == null || !existingEntities.Any())
                    return new Dictionary<uint, ServiceResults<TEntity>>
                        {
                            { 0, _serviceResultFactory.CreateServiceResults<TEntity>(entityList, [.. entityList.Select(_
                                => _logger.JsonLogInfo<TEntity, BaseHelperService<TEntity>>("Success", methodCall: methodCall))], true) }
                        };
                existingEntities = [.. existingEntities];

                var existingFieldSet = new HashSet<TKey>(existingEntities.Select(fieldSelector), EqualityComparer<TKey>.Default);
                var data = entityList
                    .Where(entity => !existingFieldSet.Contains(fieldSelector(entity)))
                    .ToList();

                var logEntries = CreateLogsByFilterEntities<TEntity, BaseHelperService<TEntity>, TKey>(entityList, existingFieldSet, fieldSelector).ToList();
                return new Dictionary<uint, ServiceResults<TEntity>>
                {
                    { 0, _serviceResultFactory.CreateServiceResults<TEntity>(data, logEntries, true) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object for filter!", ex, methodCall);
                List<JsonLogEntry> logEntries = [_logger.JsonLogError<TEntity, BaseHelperService<TEntity>>($"An error occurred while filtering valid entities.", ex, methodCall: methodCall)];

                return new Dictionary<uint, ServiceResults<TEntity>>
                {
                    { 0, _serviceResultFactory.CreateServiceResults<TEntity>([], logEntries, false) }
                };
            }
        }

        private IEnumerable<JsonLogEntry> CreateLogsByFilterEntities<TEntityModel, TServiceCall>
            (IEnumerable<TEntityModel> entities, HashSet<string> existingFieldSet, Func<TEntityModel, string> fieldSelector, [CallerMemberName] string? methodCall = null)
        {
            return entities.Select(entity =>
            {
                bool isContains = existingFieldSet.Contains(fieldSelector(entity));
                if (isContains)
                    return _logger.JsonLogWarning<TEntityModel, TServiceCall>("Failure", new Exception("already exists!"), methodCall: methodCall);
                return _logger.JsonLogInfo<TEntityModel, TServiceCall>("Success", methodCall: methodCall);
            });
        }

        private IEnumerable<JsonLogEntry> CreateLogsByFilterEntities<TEntityModel, TServiceCall, TKey>
            (IEnumerable<TEntityModel> entities, HashSet<TKey> existingFieldSet, Func<TEntityModel, TKey> fieldSelector, [CallerMemberName] string? methodCall = null) where TKey : struct
        {
            return entities.Select(entity =>
            {
                bool isContains = existingFieldSet.Contains(fieldSelector(entity));
                if (isContains)
                    return _logger.JsonLogWarning<TEntityModel, TServiceCall>("Failure", new Exception("already exists!"), methodCall: methodCall);
                return _logger.JsonLogInfo<TEntityModel, TServiceCall>("Success", methodCall: methodCall);
            });
        }
    }
}
