using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services
{
    public class BaseGetByTimeService<TEntity, TOptions> : IBaseGetByTimeService<TEntity, TOptions>
        where TEntity : class
        where TOptions : class
    {
        private readonly IBaseGetNavigationPropertyService<TEntity, TOptions> _navigationPropertyService;
        private readonly ILogService _logger;

        public BaseGetByTimeService(IBaseGetNavigationPropertyService<TEntity, TOptions> navigationPropertyService, ILogService logger)
        {
            _navigationPropertyService = navigationPropertyService;
            _logger = logger;
        }

        public async Task<IEnumerable<TEntity>> GetByDateTimeGenericAsync<TCompareType>(Func<TCompareType, int?, Task<IEnumerable<TEntity>>> daoFunc, TCompareType compareType, 
            TOptions? options, string errorMsg, int? maxGetCount = null) where TCompareType : Enum
        {
            try
            {
                IEnumerable<TEntity> results = await daoFunc(compareType, maxGetCount);
                if (options != null)
                    results = await _navigationPropertyService.GetNavigationPropertyByOptionsAsync(results, options);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseGetByTimeService<TEntity, TOptions>>(errorMsg, ex);
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }

        public async Task<IEnumerable<TEntity>> GetByDateTimeRangeGenericAsync(Func<int?, Task<IEnumerable<TEntity>>> daoFunc, TOptions? options, string errorMsg, int? maxGetCount = null)
        {
            try
            {
                IEnumerable<TEntity> results = await daoFunc(maxGetCount);
                if (options != null)
                    results = await _navigationPropertyService.GetNavigationPropertyByOptionsAsync(results, options);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseGetByTimeService<TEntity, TOptions>>(errorMsg, ex);
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }

        public async Task<IEnumerable<TEntity>> GetByMonthAndYearGenericAsync(Func<int, int, int?, Task<IEnumerable<TEntity>>> daoFunc, int year, int month, TOptions? options, string errorMsg, int? maxGetCount = null)
        {
            try
            {
                IEnumerable<TEntity> results = await daoFunc(month, year, maxGetCount);
                if (options != null)
                    results = await _navigationPropertyService.GetNavigationPropertyByOptionsAsync(results, options);

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseGetByTimeService<TEntity, TOptions>>(errorMsg, ex);
                throw new InvalidOperationException($"{errorMsg} (exception)", ex);
            }
        }
    }
}
