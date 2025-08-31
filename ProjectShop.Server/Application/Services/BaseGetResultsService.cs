using ProjectShop.Server.Application.Services.Account;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services
{
    public class BaseGetResultsService<TEntity, TOptions, TServiceCall> : IServiceGetMultiple<TEntity, TOptions, TServiceCall>
        where TEntity : class, new()
        where TOptions : class
        where TServiceCall : class
    {
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<TServiceCall> _serviceResultFactory;
        private readonly IBaseGetNavigationPropertyService<TEntity, TOptions> _navigationService;

        public BaseGetResultsService(ILogService logger, IServiceResultFactory<TServiceCall> serviceResultFactory, IBaseGetNavigationPropertyService<TEntity, TOptions> navigationService)
        {
            _logger = logger;
            _serviceResultFactory = serviceResultFactory;
            _navigationService = navigationService;
        }

        public async Task<ServiceResults<TEntity>> GetByRangeAsync(decimal minValue, decimal maxValue, Func<decimal, decimal, int?, Task<IEnumerable<TEntity>>> queryFunc,
            TOptions? options = null, int? maxGetCount = null, [CallerMemberName] string? methodCall = null)
        {
            ServiceResults<TEntity> results = new();
            try
            {
                IEnumerable<TEntity> entities = await queryFunc(minValue, maxValue, maxGetCount);
                if (entities == null || !entities.Any())
                    return _serviceResultFactory.CreateServiceResults<TEntity>($"No entities found in range: {minValue} - {maxValue}.", [], false, methodCall: methodCall);

                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(entities, options, methodCall);

                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<TEntity, SearchAccountService>($"Retrieved entities by range: {minValue} - {maxValue} with maxGetCount={maxGetCount}, options={options}.", methodCall: methodCall));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>($"An error occurred while retrieving entities in range: {minValue} - {maxValue}.", [], false, ex, methodCall: methodCall);
            }
        }

        public async Task<ServiceResults<TEntity>> GetByValueAsync(decimal value, Func<decimal, int?, Task<IEnumerable<TEntity>>> queryFunc,
            TOptions? options = null, int? maxGetCount = null, [CallerMemberName] string? methodCall = null)
        {
            ServiceResults<TEntity> results = new();
            try
            {
                IEnumerable<TEntity> entities = await queryFunc(value, maxGetCount);
                if (entities == null || !entities.Any())
                    return _serviceResultFactory.CreateServiceResults<TEntity>($"No entities found with value = {value}.", [], false, methodCall: methodCall);

                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(entities, options, methodCall);

                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<TEntity, SearchAccountService>($"Retrieved entities by value = {value} with maxGetCount={maxGetCount}, options={options}.", methodCall: methodCall));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>($"An error occurred while retrieving entities by value = {value}.", [], false, ex, methodCall: methodCall);
            }
        }

        public async Task<ServiceResults<TEntity>> GetManyAsync<TParam>(TParam param, Func<TParam, int?, Task<IEnumerable<TEntity>>> queryFunc, TOptions? options = null, int? maxGetCount = null, [CallerMemberName] string? methodCall = null)
        {
            ServiceResults<TEntity> results = new();
            try
            {
                IEnumerable<TEntity> accounts = await queryFunc(param, maxGetCount);
                if (accounts == null || !accounts.Any())
                    return _serviceResultFactory.CreateServiceResults<TEntity>($"No entities found with param = {param}.", [], false, methodCall: methodCall);

                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(accounts, options, methodCall);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<TEntity, SearchAccountService>($"Retrieved entities by param = {param} with maxGetCount={maxGetCount}, options={options}.", methodCall: methodCall));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>($"An error occurred while retrieving entities by param = {param}.", [], false, ex, methodCall: methodCall);
            }
        }

        public async Task<ServiceResults<TEntity>> GetManyAsync(Func<int?, Task<IEnumerable<TEntity>>> queryFunc, TOptions? options = null, int? maxGetCount = null, [CallerMemberName] string? methodCall = null)
        {
            ServiceResults<TEntity> results = new();
            try
            {
                IEnumerable<TEntity> entities = await queryFunc(maxGetCount);
                if (entities == null || !entities.Any())
                    return _serviceResultFactory.CreateServiceResults<TEntity>("No entities found.", [], false, methodCall: methodCall);

                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(entities, options, methodCall);

                results.LogEntries = results.LogEntries!.Append(
                    _logger.JsonLogInfo<TEntity, SearchAccountService>($"Retrieved all entities with maxGetCount={maxGetCount}, options={options}.", methodCall: methodCall));

                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>("An error occurred while retrieving all entities.", [], false, ex, methodCall: methodCall);
            }
        }
    }
}
