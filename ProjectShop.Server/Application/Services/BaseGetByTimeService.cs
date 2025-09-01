using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services
{
    public class BaseGetByTimeService<TEntity, TOptions> : IBaseGetByTimeServices<TEntity, TOptions>
        where TEntity : class
        where TOptions : class
    {
        private readonly ILogService _logger;
        private readonly IBaseGetNavigationPropertyServices<TEntity, TOptions> _navigationPropertyService;
        private readonly IServiceResultFactory<BaseGetByTimeService<TEntity, TOptions>> _serviceResultFactory;

        public BaseGetByTimeService(IBaseGetNavigationPropertyServices<TEntity, TOptions> navigationPropertyService, ILogService logger, IServiceResultFactory<BaseGetByTimeService<TEntity, TOptions>> serviceResultFactory)
        {
            _logger = logger;
            _navigationPropertyService = navigationPropertyService;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResults<TEntity>> GetByDateTimeGenericAsync<TCompareType>(Func<TCompareType, int?, Task<IEnumerable<TEntity>>> daoFunc, TCompareType compareType,
            TOptions? options, string errorMsg, int? maxGetCount = null, [CallerMemberName] string? methodCall = null) where TCompareType : Enum
        {
            ServiceResults<TEntity> results = new();
            try
            {
                IEnumerable<TEntity> entities = await daoFunc(compareType, maxGetCount);
                if (entities == null || !entities.Any())
                {
                    JsonLogEntry log = _logger.JsonLogInfo<TEntity, BaseGetByTimeService<TEntity, TOptions>>($"No entities found with {typeof(TCompareType).Name}: {compareType}.", methodCall: methodCall);
                    results.LogEntries = results.LogEntries!.Append(log);
                    return results;
                }

                if (options != null)
                    results = await _navigationPropertyService.GetNavigationPropertyByOptionsAsync(entities, options);
                JsonLogEntry logEntry = _logger.JsonLogInfo<TEntity, BaseGetByTimeService<TEntity, TOptions>>($"Successfully retrieved entities with {typeof(TCompareType).Name}: {compareType}.", methodCall: methodCall);
                results.LogEntries = results.LogEntries!.Append(logEntry);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseGetByTimeService<TEntity, TOptions>>(errorMsg, ex, methodCall);
                return _serviceResultFactory.CreateServiceResults<TEntity>($"{errorMsg} (exception)", [], false, ex, methodCall: methodCall);
            }
        }

        public async Task<ServiceResults<TEntity>> GetByDateTimeRangeGenericAsync(Func<int?, Task<IEnumerable<TEntity>>> daoFunc, TOptions? options, string errorMsg, 
            int? maxGetCount = null, [CallerMemberName] string? methodCall = null)
        {
            ServiceResults<TEntity> results = new();
            try
            {
                IEnumerable<TEntity> entities = await daoFunc(maxGetCount);
                if (entities == null || !entities.Any())
                {
                    JsonLogEntry log = _logger.JsonLogInfo<TEntity, BaseGetByTimeService<TEntity, TOptions>>("No entities found within the specified date range.", methodCall: methodCall);
                    results.LogEntries = results.LogEntries!.Append(log);
                    return results;
                }

                if (options != null)
                    results = await _navigationPropertyService.GetNavigationPropertyByOptionsAsync(entities, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<TEntity, BaseGetByTimeService<TEntity, TOptions>>("Successfully retrieved entities within the specified date range.", methodCall: methodCall));
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseGetByTimeService<TEntity, TOptions>>(errorMsg, ex, methodCall);
                return _serviceResultFactory.CreateServiceResults<TEntity>($"{errorMsg} (exception)", [], false, ex, methodCall: methodCall);
            }
        }

        public async Task<ServiceResults<TEntity>> GetByMonthAndYearGenericAsync(Func<int, int, int?, Task<IEnumerable<TEntity>>> daoFunc, int year, int month, TOptions? options, string errorMsg, 
            int? maxGetCount = null, [CallerMemberName] string? methodCall = null)
        {
            ServiceResults<TEntity> results = new();
            try
            {
                IEnumerable<TEntity> entities = await daoFunc(month, year, maxGetCount);
                if (entities == null || !entities.Any())
                {
                    JsonLogEntry log = _logger.JsonLogInfo<TEntity, BaseGetByTimeService<TEntity, TOptions>>($"No entities found for {month}/{year}.", methodCall: methodCall);
                    results.LogEntries = results.LogEntries!.Append(log);
                    return results;
                }

                if (options != null)
                    results = await _navigationPropertyService.GetNavigationPropertyByOptionsAsync(entities, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<TEntity, BaseGetByTimeService<TEntity, TOptions>>($"Successfully retrieved entities for {month}/{year}.", methodCall: methodCall));
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseGetByTimeService<TEntity, TOptions>>(errorMsg, ex, methodCall);
                return _serviceResultFactory.CreateServiceResults<TEntity>($"{errorMsg} (exception)", [], false, ex, methodCall: methodCall);
            }
        }
    }
}
