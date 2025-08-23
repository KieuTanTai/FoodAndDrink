using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;

namespace TLGames.Application.Services
{
    public class BaseHelperService<TEntity> : IBaseHelperService<TEntity>
        where TEntity : class
    {
        private readonly IHashPassword _hashPassword;
        private readonly IStringConverter _converter;
        private readonly IClock _clock;
        private readonly IMaxGetRecord _maxGetRecord;
        private readonly ILogService _logger;

        public BaseHelperService(
            IHashPassword hashPassword,
            IStringConverter converter,
            IClock clock,
            IMaxGetRecord maxGetRecord,
            ILogService logger)
        {
            _clock = clock;
            _logger = logger;
            _converter = converter;
            _hashPassword = hashPassword;
            _maxGetRecord = maxGetRecord;
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

        public async Task<bool> IsExistObject<TKey>(TKey keys, Func<TKey, Task<TEntity>> daoFunc) where TKey : struct
        {
            try
            {
                TEntity existingObject = await daoFunc(keys);
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
                return 200;
            return maxGetCount > _maxGetRecord.MaxGetRecord ? _maxGetRecord.MaxGetRecord : maxGetCount;
        }

        public async Task<Dictionary<uint, ServiceResults<TEntity>>> FilterValidEntities(
            IEnumerable<TEntity> entities,
            Func<TEntity, string> fieldSelector,
            Func<IEnumerable<string>, int?, Task<IEnumerable<TEntity>>> daoFunc)
        {
            try
            {
                var entityList = entities.ToList();
                var fieldValues = entityList.Select(fieldSelector).ToList();
                var existingEntities = (await daoFunc(fieldValues, null)).ToList();

                var existingFieldSet = new HashSet<string>(existingEntities.Select(fieldSelector), StringComparer.OrdinalIgnoreCase);

                var data = entityList
                    .Where(entity => !existingFieldSet.Contains(fieldSelector(entity)))
                    .ToList();

                var logEntries = entityList.Select(entity =>
                {
                    bool isContains = existingFieldSet.Contains(fieldSelector(entity));
                    if (isContains)
                        return _logger.JsonLogWarning<TEntity, BaseHelperService<TEntity>>("Failure", new Exception("already exists!"));
                    return _logger.JsonLogInfo<TEntity, BaseHelperService<TEntity>>("Success");
                }).ToList();

                return new Dictionary<uint, ServiceResults<TEntity>>
                {
                    { 0, new ServiceResults<TEntity> { LogEntries = logEntries, Data = data } }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object for filter!", ex);
                List<JsonLogEntry> logEntries = new List<JsonLogEntry> { _logger.JsonLogError<TEntity, BaseHelperService<TEntity>>($"An error occurred while filtering valid entities.", ex) };

                return new Dictionary<uint, ServiceResults<TEntity>>
                {
                    { 0, new ServiceResults<TEntity> { LogEntries = logEntries, Data = new List<TEntity>() } }
                };
            }
        }

        public async Task<Dictionary<uint, ServiceResults<TEntity>>> FilterValidEntities<TKey>(
            IEnumerable<TEntity> entities,
            Func<TEntity, TKey> fieldSelector,
            Func<IEnumerable<TKey>, int?, Task<IEnumerable<TEntity>>> daoFunc) where TKey : struct
        {
            try
            {
                var entityList = entities.ToList();
                var fieldValues = entityList.Select(fieldSelector).ToList();
                var existingEntities = (await daoFunc(fieldValues, null)).ToList();

                var existingFieldSet = new HashSet<TKey>(existingEntities.Select(fieldSelector), EqualityComparer<TKey>.Default);

                var data = entityList
                    .Where(entity => !existingFieldSet.Contains(fieldSelector(entity)))
                    .ToList();

                var logEntries = entityList.Select(entity =>
                {
                    bool isContains = existingFieldSet.Contains(fieldSelector(entity));
                    if (isContains)
                        return _logger.JsonLogWarning<TEntity, BaseHelperService<TEntity>>("Failure", new Exception("already exists!"));
                    return _logger.JsonLogInfo<TEntity, BaseHelperService<TEntity>>("Success");
                }).ToList();

                return new Dictionary<uint, ServiceResults<TEntity>>
                {
                    { 0, new ServiceResults<TEntity> { LogEntries = logEntries, Data = data } }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object for filter!", ex);

                List<JsonLogEntry> logEntries = new List<JsonLogEntry> { _logger.JsonLogError<TEntity, BaseHelperService<TEntity>>($"An error occurred while filtering valid entities.", ex) };

                return new Dictionary<uint, ServiceResults<TEntity>>
                {
                    { 0, new ServiceResults<TEntity> { LogEntries = logEntries, Data = new List<TEntity>() } }
                };
            }
        }
    }
}
