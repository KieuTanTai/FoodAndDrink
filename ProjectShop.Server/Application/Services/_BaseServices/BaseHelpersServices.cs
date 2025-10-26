using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services._BaseServices
{
    public class BaseHelperServices<TEntity> : IBaseHelperServices<TEntity>
        where TEntity : class
    {
        private readonly IHashPassword _hashPassword;
        private readonly IClock _clock;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<BaseHelperServices<TEntity>> _serviceResultFactory;

        public BaseHelperServices(
            IHashPassword hashPassword,
            IClock clock,
            ILogService logger,
            IServiceResultFactory<BaseHelperServices<TEntity>> serviceResultFactory)
        {
            _clock = clock;
            _logger = logger;
            _hashPassword = hashPassword;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<bool> IsExistObject(string input, Func<string, CancellationToken, Task<TEntity?>> daoFunc, 
            CancellationToken cancellationToken)
        {
            try
            {
                TEntity? existingObject = await daoFunc(input, cancellationToken);
                return existingObject != null;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Operation canceled while checking existence of object with input: {input}", ex);
                return false;
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Operation canceled while checking existence of object with input: {input}", ex);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object with input: {input}", ex);
                return false;
            }
        }

        public async Task<bool> IsExistObject(uint input, Func<uint, CancellationToken, Task<TEntity?>> daoFunc, 
            CancellationToken cancellationToken)
        {
            try
            {
                TEntity? existingObject = await daoFunc(input, cancellationToken);
                return existingObject != null;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Operation canceled while checking existence of object with input: {input}", ex);
                return false;
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Operation canceled while checking existence of object with input: {input}", ex);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object with input: {input}", ex);
                return false;
            }
        }

        public async Task<bool> DoAllIdsExistAsync(IEnumerable<string> ids,
            Func<IEnumerable<string>, CancellationToken, Task<IEnumerable<TEntity>>> daoFunc, CancellationToken cancellationToken)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids, cancellationToken);
                return existingObjects.Count() == ids.Count();
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object!", ex);
                return false;
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object!", ex);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object!", ex);
                return false;
            }
        }

        public async Task<bool> DoAllIdsExistAsync(IEnumerable<uint> ids, 
            Func<IEnumerable<uint>, CancellationToken, Task<IEnumerable<TEntity>>> daoFunc, CancellationToken cancellationToken)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids, cancellationToken);
                return existingObjects.Count() == ids.Count();
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object!", ex);
                return false;
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object!", ex);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object!", ex);
                return false;
            }
        }

        public async Task<bool> DoNoneOfIdsExistAsync(IEnumerable<string> ids,
            Func<IEnumerable<string>, CancellationToken, Task<IEnumerable<TEntity>>> daoFunc, CancellationToken cancellationToken)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids, cancellationToken);
                return !existingObjects.Any();
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object!", ex);
                return true;
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object!", ex);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object!", ex);
                return true;
            }
        }

        public async Task<bool> DoNoneOfIdsExistAsync(IEnumerable<uint> ids, Func<IEnumerable<uint>, CancellationToken,
            Task<IEnumerable<TEntity>>> daoFunc, CancellationToken cancellationToken)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids, cancellationToken);
                return !existingObjects.Any();
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object!", ex);
                return true;
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object!", ex);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError<TEntity, BaseHelperServices<TEntity>>($"Error checking existence of object!", ex);
                return true;
            }
        }
    }
}
