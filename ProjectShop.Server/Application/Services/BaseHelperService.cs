using ProjectShop.Server.Application.Services;
using ProjectShop.Server.Core.Entities.EntitiesRequest;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Services;

namespace TLGames.Application.Services
{
    public abstract class BaseHelperService<TEntity> : BaseAuthorizationService
        where TEntity : class
    {
        protected readonly IHashPassword hashPassword = GetProviderService.SystemServices.GetRequiredService<IHashPassword>();
        protected readonly IColumnService colService = GetProviderService.SystemServices.GetRequiredService<IColumnService>();
        protected readonly IStringConverter converter = GetProviderService.SystemServices.GetRequiredService<IStringConverter>();
        protected readonly IClock clock = GetProviderService.SystemServices.GetRequiredService<IClock>();
        protected async Task<bool> IsExistObject(string input, Func<string, Task<TEntity?>> daoFunc)
        {
            try
            {
                TEntity? existingObject = await daoFunc(input);
                return existingObject != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> IsExistObject(uint input, Func<uint, Task<TEntity?>> daoFunc)
        {
            try
            {
                TEntity? existingObject = await daoFunc(input);
                return existingObject != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> IsExistObject<TKey>(TKey keys, Func<TKey, Task<TEntity>> daoFunc) where TKey : struct
        {
            try
            {
                TEntity existingObject = await daoFunc(keys);
                return existingObject != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> DoAllIdsExistAsync(IEnumerable<string> ids, Func<IEnumerable<string>, Task<IEnumerable<TEntity>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids);
                return existingObjects.Count() == ids.Count();
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> DoAllIdsExistAsync(IEnumerable<uint> ids, Func<IEnumerable<uint>, Task<IEnumerable<TEntity>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids);
                return existingObjects.Count() == ids.Count();
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> DoAllKeysExistAsync<TKey>(IEnumerable<TKey> keys, Func<IEnumerable<TKey>, Task<IEnumerable<TEntity>>> daoFunc) where TKey : struct
        {
            if (keys == null || !keys.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(keys);
                return existingObjects.Count() == keys.Count();
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> DoNoneOfIdsExistAsync(IEnumerable<string> ids, Func<IEnumerable<string>, Task<IEnumerable<TEntity>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids);
                return !existingObjects.Any();
            }
            catch (Exception)
            {
                return true;
            }
        }

        protected async Task<bool> DoNoneOfIdsExistAsync(IEnumerable<uint> ids, Func<IEnumerable<uint>, Task<IEnumerable<TEntity>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids);
                return !existingObjects.Any();
            }
            catch (Exception)
            {
                return true;
            }
        }

        protected async Task<bool> DoNoneOfKeysExistAsync<TKey>(IEnumerable<TKey> keys, Func<IEnumerable<TKey>, Task<IEnumerable<TEntity>>> daoFunc) where TKey : struct
        {
            if (keys == null || !keys.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(keys);
                return !existingObjects.Any();
            }
            catch (Exception)
            {
                return true;
            }
        }

        // NOTE: This method filters valid entities based on a field selector and a DAO function.
        protected async Task<Dictionary<uint, BatchObjectResult<TEntity>>> FilterValidEntities(
            IEnumerable<TEntity> entities,
            Func<TEntity, string> fieldSelector,
            Func<IEnumerable<string>, Task<IEnumerable<TEntity>>> daoFunc)
        {
            try
            {
                var entityList = entities.ToList();
                var fieldValues = entityList.Select(fieldSelector).ToList();
                var existingEntities = (await daoFunc(fieldValues)).ToList();

                // Tạo HashSet cho hiệu suất so sánh
                var existingFieldSet = new HashSet<string>(existingEntities.Select(fieldSelector), StringComparer.OrdinalIgnoreCase);

                // Lọc entity hợp lệ
                var validEntities = entityList
                    .Where(entity => !existingFieldSet.Contains(fieldSelector(entity)))
                    .ToList();

                // Tạo batch result cho tất cả input, đúng thứ tự
                var batchItemResults = entityList.Select(entity => new BatchItemResult<TEntity>
                {
                    Input = entity,
                    IsSuccess = !existingFieldSet.Contains(fieldSelector(entity)),
                    ErrorMessage = existingFieldSet.Contains(fieldSelector(entity)) ? "already exists!" : ""
                }).ToList();

                return new Dictionary<uint, BatchObjectResult<TEntity>>
                {
                    { 0, new BatchObjectResult<TEntity> {ValidEntities = validEntities.ToList(), BatchResults = batchItemResults} }
                };
            }
            catch (Exception)
            {
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

        // BUG: IT may not work correctly if TKey is not a struct or does not have a proper equality comparer.
        protected async Task<Dictionary<uint, BatchObjectResult<TEntity>>> FilterValidEntities<TKey>(
            IEnumerable<TEntity> entities,
            Func<TEntity, TKey> fieldSelector,
            Func<IEnumerable<TKey>, Task<IEnumerable<TEntity>>> daoFunc) where TKey : struct
        {
            try
            {
                var entityList = entities.ToList();
                var fieldValues = entityList.Select(fieldSelector).ToList();
                var existingEntities = (await daoFunc(fieldValues)).ToList();
                // Tạo HashSet cho hiệu suất so sánh
                var existingFieldSet = new HashSet<TKey>(existingEntities.Select(fieldSelector), EqualityComparer<TKey>.Default);
                // Lọc entity hợp lệ
                var validEntities = entityList
                    .Where(entity => !existingFieldSet.Contains(fieldSelector(entity)))
                    .ToList();
                // Tạo batch result cho tất cả input, đúng thứ tự
                var batchItemResults = entityList.Select(entity => new BatchItemResult<TEntity>
                {
                    Input = entity,
                    IsSuccess = !existingFieldSet.Contains(fieldSelector(entity)),
                    ErrorMessage = existingFieldSet.Contains(fieldSelector(entity)) ? "already exists!" : ""
                }).ToList();
                return new Dictionary<uint, BatchObjectResult<TEntity>>
                {
                    { 0, new BatchObjectResult<TEntity> {ValidEntities = validEntities.ToList(), BatchResults = batchItemResults} }
                };
            }
            catch (Exception)
            {
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
