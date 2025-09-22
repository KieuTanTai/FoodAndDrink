using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services._BaseServices
{
    public class BaseGetResultService<TEntity, TOptions, TServiceCall> : IGetSingleServices<TEntity, TOptions, TServiceCall>
        where TEntity : class, new()
        where TOptions : class
        where TServiceCall : class
    {
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<TServiceCall> _serviceResultFactory;
        private readonly IBaseGetNavigationPropertyServices<TEntity, TOptions> _navigationService;

        public BaseGetResultService(ILogService logger,
            IServiceResultFactory<TServiceCall> serviceResultFactory,
            IBaseGetNavigationPropertyServices<TEntity, TOptions> navigationService)
        {
            _logger = logger;
            _serviceResultFactory = serviceResultFactory;
            _navigationService = navigationService;
        }

        public async Task<ServiceResult<TEntity>> GetAsync<TParam>(TParam param, Func<TParam, Task<TEntity?>> queryFunc, TOptions? options = null, [CallerMemberName] string? methodCall = null)
        {
            ServiceResult<TEntity> result = new(true);
            try
            {
                TEntity? entity = await queryFunc(param);
                if (entity == null)
                    return _serviceResultFactory.CreateServiceResult($"No Entity found with param: {param}.", new TEntity(), false, methodCall: methodCall);

                if (options != null)
                    result = await _navigationService.GetNavigationPropertyByOptionsAsync(entity, options, methodCall);
                result.LogEntries = result.LogEntries!.Append(_logger.JsonLogInfo<TEntity, TServiceCall>($"Retrieved entity by param = {param} with options={options}.", methodCall: methodCall));
                return result;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult($"An error occurred while retrieving entity by param: {param}.", new TEntity(), false, ex, methodCall: methodCall);
            }
        }
    }
}
