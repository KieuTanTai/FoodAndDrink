using ProjectShop.Server.Application.Services.Account;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services
{
    public class BaseGetResultsService<TEntity, TOptions, TRootEntityCall> : IServiceGetMultiple<TEntity, TOptions>
        where TEntity : class, new()
        where TOptions : class
        where TRootEntityCall : class
    {
        private readonly ILogService _logger;
        private readonly IBaseHelperService<TEntity> _baseHelperService;
        private readonly IServiceResultFactory<TRootEntityCall> _serviceResultFactory;
        private readonly IBaseHelperService<TEntity> _helper;
        private readonly IBaseGetNavigationPropertyService<TEntity, TOptions> _navigationService;

        public BaseGetResultsService(ILogService logger, IBaseHelperService<TEntity> baseHelperService,
            IBaseHelperService<TEntity> helper,
            IServiceResultFactory<TRootEntityCall> serviceResultFactory, IBaseGetNavigationPropertyService<TEntity, TOptions> navigationService)
        {
            _logger = logger;
            _helper = helper;
            _baseHelperService = baseHelperService;
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
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(entities, options);

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
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(entities, options);

                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<TEntity, SearchAccountService>($"Retrieved entities by value = {value} with maxGetCount={maxGetCount}, options={options}.", methodCall: methodCall));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>($"An error occurred while retrieving entities by value = {value}.", [], false, ex, methodCall: methodCall);
            }
        }

        public async Task<ServiceResults<TEntity>> QueryManyAsync<TParam>(TParam param, Func<TParam, int?, Task<IEnumerable<TEntity>>> queryFunc, TOptions? options = null, int? maxGetCount = null, [CallerMemberName] string? methodCall = null)
        {
            ServiceResults<TEntity> results = new();
            try
            {
                IEnumerable<TEntity> accounts = await queryFunc(param, maxGetCount);
                if (accounts == null || !accounts.Any())
                    return _serviceResultFactory.CreateServiceResults<TEntity>($"No entities found with param = {param}.", [], false, methodCall: methodCall);

                if (options != null)
                    results = await _navigationService.GetNavigationPropertyByOptionsAsync(accounts, options);
                results.LogEntries = results.LogEntries!.Append(_logger.JsonLogInfo<TEntity, SearchAccountService>($"Retrieved entities by param = {param} with maxGetCount={maxGetCount}, options={options}.", methodCall: methodCall));
                return results;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>($"An error occurred while retrieving entities by param = {param}.", [], false, ex, methodCall: methodCall);
            }
        }
    }
}
