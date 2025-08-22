using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;

namespace TLGames.Application.Services
{
    public class BaseHelperService<TEntity> : IBaseHelperService<TEntity>
        where TEntity : class
    {
        private readonly IHashPassword hashPassword;
        private readonly IStringConverter converter;
        private readonly IClock clock;
        private readonly IMaxGetRecord maxGetRecord;
        private readonly ILogService logger;

        public BaseHelperService(
            IHashPassword hashPassword,
            IStringConverter converter,
            IClock clock,
            IMaxGetRecord maxGetRecord,
            ILogService logger)
        {
            this.hashPassword = hashPassword;
            this.converter = converter;
            this.clock = clock;
            this.maxGetRecord = maxGetRecord;
            this.logger = logger;
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
                logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object with input: {input}", ex);
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
                logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object with input: {input}", ex);
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
                logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object with key: {keys}", ex);
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
                logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object!", ex);
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
                logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object!", ex);
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
                logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object!", ex);
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
                logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object!", ex);
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
                logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object!", ex);
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
                logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object!", ex);
                return true;
            }
        }

        public int? GetValidMaxRecord(int? maxGetCount)
        {
            if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                return 200;
            return maxGetCount > maxGetRecord.MaxGetRecord ? maxGetRecord.MaxGetRecord : maxGetCount;
        }

        public async Task<Dictionary<uint, BatchObjectResult<TEntity>>> FilterValidEntities(
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

                var validEntities = entityList
                    .Where(entity => !existingFieldSet.Contains(fieldSelector(entity)))
                    .ToList();

                var batchItemResults = entityList.Select(entity => new BatchItemResult<TEntity>
                {
                    Input = entity,
                    IsSuccess = !existingFieldSet.Contains(fieldSelector(entity)),
                    ErrorMessage = existingFieldSet.Contains(fieldSelector(entity)) ? "already exists!" : ""
                }).ToList();

                return new Dictionary<uint, BatchObjectResult<TEntity>>
                {
                    { 0, new BatchObjectResult<TEntity> { ValidEntities = validEntities.ToList(), BatchResults = batchItemResults } }
                };
            }
            catch (Exception ex)
            {
                logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object for filter!", ex);

                var batchItemResults = entities.Select(entity => new BatchItemResult<TEntity>
                {
                    Input = entity,
                    IsSuccess = false,
                    ErrorMessage = "An error occurred while filtering valid entities."
                }).ToList();

                return new Dictionary<uint, BatchObjectResult<TEntity>>
                {
                    { 0, new BatchObjectResult<TEntity> { ValidEntities = new List<TEntity>(), BatchResults = batchItemResults } }
                };
            }
        }

        public async Task<Dictionary<uint, BatchObjectResult<TEntity>>> FilterValidEntities<TKey>(
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

                var validEntities = entityList
                    .Where(entity => !existingFieldSet.Contains(fieldSelector(entity)))
                    .ToList();

                var batchItemResults = entityList.Select(entity => new BatchItemResult<TEntity>
                {
                    Input = entity,
                    IsSuccess = !existingFieldSet.Contains(fieldSelector(entity)),
                    ErrorMessage = existingFieldSet.Contains(fieldSelector(entity)) ? "already exists!" : ""
                }).ToList();

                return new Dictionary<uint, BatchObjectResult<TEntity>>
                {
                    { 0, new BatchObjectResult<TEntity> { ValidEntities = validEntities.ToList(), BatchResults = batchItemResults } }
                };
            }
            catch (Exception ex)
            {
                logger.LogError<TEntity, BaseHelperService<TEntity>>($"Error checking existence of object for filter!", ex);

                var batchItemResults = entities.Select(entity => new BatchItemResult<TEntity>
                {
                    Input = entity,
                    IsSuccess = false,
                    ErrorMessage = "An error occurred while filtering valid entities."
                }).ToList();

                return new Dictionary<uint, BatchObjectResult<TEntity>>
                {
                    { 0, new BatchObjectResult<TEntity> { ValidEntities = new List<TEntity>(), BatchResults = batchItemResults } }
                };
            }
        }
    }
}
