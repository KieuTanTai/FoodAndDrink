using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services._BaseServices
{
    public class BaseGetResultsServices<TEntity, TServiceCall> : IGetMultipleServices<TEntity, TServiceCall>
        where TEntity : class, new()
        where TServiceCall : class
    {
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<TServiceCall> _serviceResultFactory;

        public BaseGetResultsServices(ILogService logger, IServiceResultFactory<TServiceCall> serviceResultFactory)
        {
            _logger = logger;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResults<TEntity>> GetByRangeAsync(decimal minValue, decimal maxValue, Func<decimal, decimal, CancellationToken, Task<IEnumerable<TEntity>>> queryFunc,
            CancellationToken cancellationToken, [CallerMemberName] string? methodCall = null)
        {
            ServiceResults<TEntity> results = new([], [], true);
            try
            {
                IEnumerable<TEntity> entities = await queryFunc(minValue, maxValue, cancellationToken);
                if (entities == null || !entities.Any())
                {
                    entities ??= [];
                    results.LogEntries = results.LogEntries!.Append(_logger.JsonLogWarning<TEntity, TServiceCall>($"No entities found in range: {minValue} - {maxValue}.", methodCall: methodCall));
                    results.IsSuccess = false;
                    return results;
                }
                return results;
            }
            catch (TaskCanceledException ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>
                    ($"The operation was canceled while retrieving entities in range: {minValue} - {maxValue}.", [], false, ex, methodCall: methodCall);
            }
            catch (OperationCanceledException ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>
                    ($"The operation was canceled while retrieving entities in range: {minValue} - {maxValue}.", [], false, ex, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>
                    ($"An error occurred while retrieving entities in range: {minValue} - {maxValue}.", [], false, ex, methodCall: methodCall);
            }
        }

        public async Task<ServiceResults<TEntity>> GetByValueAsync(decimal value, Func<decimal, CancellationToken, Task<IEnumerable<TEntity>>> queryFunc,
            CancellationToken cancellationToken, [CallerMemberName] string? methodCall = null)
        {
            ServiceResults<TEntity> results = new([], [], true);
            try
            {
                IEnumerable<TEntity> entities = await queryFunc(value, cancellationToken);
                if (entities == null || !entities.Any())
                {
                    entities ??= [];
                    results.LogEntries = results.LogEntries!.Append(_logger.JsonLogWarning<TEntity, TServiceCall>($"No entities found with value = {value}.", methodCall: methodCall));
                    results.IsSuccess = false;
                    return results;
                }
                return results;
            }
            catch (TaskCanceledException ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>
                    ($"The operation was canceled while retrieving entities by value = {value}.", [], false, ex, methodCall: methodCall);
            }
            catch (OperationCanceledException ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>
                    ($"The operation was canceled while retrieving entities by value = {value}.", [], false, ex, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>
                    ($"An error occurred while retrieving entities by value = {value}.", [], false, ex, methodCall: methodCall);
            }
        }

        public async Task<ServiceResults<TEntity>> GetManyAsync<TParam>(TParam param, Func<TParam, CancellationToken, Task<IEnumerable<TEntity>>> queryFunc,
            CancellationToken cancellationToken, [CallerMemberName] string? methodCall = null)
        {
            ServiceResults<TEntity> results = new([], [], true);
            try
            {
                IEnumerable<TEntity> accounts = await queryFunc(param, cancellationToken);
                if (accounts == null || !accounts.Any())
                {
                    accounts ??= [];
                    results.LogEntries = results.LogEntries!.Append(_logger.JsonLogWarning<TEntity, TServiceCall>($"No entities found by param = {param}.", methodCall: methodCall));
                    results.IsSuccess = false;
                    return results;
                }
                return results;
            }
            catch (TaskCanceledException ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>
                    ($"The operation was canceled while retrieving entities by param = {param}.", [], false, ex, methodCall: methodCall);
            }
            catch (OperationCanceledException ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>
                    ($"The operation was canceled while retrieving entities by param = {param}.", [], false, ex, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>
                    ($"An error occurred while retrieving entities by param = {param}.", [], false, ex, methodCall: methodCall);
            }
        }

        public async Task<ServiceResults<TEntity>> GetManyAsync(Func<CancellationToken, Task<IEnumerable<TEntity>>> queryFunc,
            CancellationToken cancellationToken, [CallerMemberName] string? methodCall = null)
        {
            ServiceResults<TEntity> results = new([], [], true);
            try
            {
                IEnumerable<TEntity> entities = await queryFunc(cancellationToken);
                if (entities == null || !entities.Any())
                {
                    entities ??= [];
                    results.LogEntries = results.LogEntries!.Append(_logger.JsonLogWarning<TEntity, TServiceCall>
                        ("No entities found.", methodCall: methodCall));
                    results.IsSuccess = false;
                    return results;
                }
                return results;
            }
            catch (TaskCanceledException ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>
                    ("The operation was canceled while retrieving all entities.", [], false, ex, methodCall: methodCall);
            }
            catch (OperationCanceledException ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>
                    ("The operation was canceled while retrieving all entities.", [], false, ex, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<TEntity>
                    ("An error occurred while retrieving all entities.", [], false, ex, methodCall: methodCall);
            }
        }
    }
}
