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
        protected async Task<IEnumerable<TEntity>> GetByDateTimeGenericAsync<TCompareType>(Func<TCompareType, Task<IEnumerable<TEntity>>> daoFunc, TCompareType compareType, TOptions? options, string errorMsg) where TCompareType : Enum
        {
            try
            {
                IEnumerable<TEntity> results = await daoFunc(compareType);
                if (options != null)
                    results = await GetNavigationPropertyByOptionsAsync(results, options);
                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByDateTimeRangeGenericAsync(Func<Task<IEnumerable<TEntity>>> daoFunc, TOptions? options, string errorMsg)
        {
            try
            {
                IEnumerable<TEntity> results = await daoFunc();
                if (options != null)
                    results = await GetNavigationPropertyByOptionsAsync(results, options);
                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }

        protected async Task<IEnumerable<TEntity>> GetByMonthAndYearGenericAsync(Func<int, int, Task<IEnumerable<TEntity>>> daoFunc, int year, int month, TOptions? options, string errorMsg)
        {
            try
            {
                IEnumerable<TEntity> results = await daoFunc(month, year);
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
