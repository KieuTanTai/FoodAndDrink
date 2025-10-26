using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services._BaseServices
{
    public class BaseGetResultServices<TEntity, TServiceCall> : IGetSingleServices<TEntity, TServiceCall>
        where TEntity : class, new()
        where TServiceCall : class
    {
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<TServiceCall> _serviceResultFactory;

        public BaseGetResultServices(ILogService logger,
            IServiceResultFactory<TServiceCall> serviceResultFactory)
        {
            _logger = logger;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResult<TEntity>> GetAsync<TParam>(TParam param, Func<TParam, CancellationToken, Task<TEntity?>> queryFunc,
            CancellationToken cancellationToken, [CallerMemberName] string? methodCall = null)
        {
            ServiceResult<TEntity> result = new(true);
            try
            {
                TEntity? entity = await queryFunc(param, cancellationToken);
                if (entity == null)
                    return _serviceResultFactory.CreateServiceResult($"No Entity found with param: {param}.", new TEntity(), false, methodCall: methodCall);
                return result;
            }
            catch (TaskCanceledException ex)
            {
                return _serviceResultFactory.CreateServiceResult($"The operation was canceled while retrieving entity by param: {param}.", new TEntity(), false, ex, methodCall: methodCall);
            }
            catch (OperationCanceledException ex)
            {
                return _serviceResultFactory.CreateServiceResult($"The operation was canceled while retrieving entity by param: {param}.", new TEntity(), false, ex, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult($"An error occurred while retrieving entity by param: {param}.", new TEntity(), false, ex, methodCall: methodCall);
            }
        }
    }
}
