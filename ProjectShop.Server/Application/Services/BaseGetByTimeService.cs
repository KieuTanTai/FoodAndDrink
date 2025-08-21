using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services
{
    public abstract class BaseGetByTimeService<TEntity, TOptions> : BaseHelperService<TEntity> 
        where TEntity : class 
        where TOptions : class
    {
        //DRY
        protected async Task<IEnumerable<TEntity>> GetByDateTimeGenericAsync<TCompareType>(Func<TCompareType, int?, Task<IEnumerable<TEntity>>> daoFunc, TCompareType compareType, TOptions? options, string errorMsg, int? maxGetCount = null) where TCompareType : Enum
        {
            try
            {
                IEnumerable<TEntity> results = await daoFunc(compareType, maxGetCount);
                if (options != null)
                    results = await GetNavigationPropertyByOptionsAsync(results, options);
                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByDateTimeRangeGenericAsync(Func<int?, Task<IEnumerable<TEntity>>> daoFunc, TOptions? options, string errorMsg, int? maxGetCount = null)
        {
            try
            {
                IEnumerable<TEntity> results = await daoFunc(maxGetCount);
                if (options != null)
                    results = await GetNavigationPropertyByOptionsAsync(results, options);
                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByMonthAndYearGenericAsync(Func<int, int, int?, Task< IEnumerable<TEntity>>> daoFunc, int year, int month, TOptions? options, string errorMsg, int? maxGetCount = null)
        {
            try
            {
                IEnumerable<TEntity> results = await daoFunc(month, year, maxGetCount);
                if (options != null)
                    results = await GetNavigationPropertyByOptionsAsync(results, options);
                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }

        protected abstract Task<TEntity> GetNavigationPropertyByOptionsAsync(TEntity entity, TOptions? options);
        protected abstract Task<IEnumerable<TEntity>> GetNavigationPropertyByOptionsAsync(IEnumerable<TEntity> entities, TOptions? options);
    }
}
