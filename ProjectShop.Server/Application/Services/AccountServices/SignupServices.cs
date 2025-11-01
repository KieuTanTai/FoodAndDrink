using System.Security.Claims;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Infrastructure.Services;

namespace ProjectShop.Server.Application.Services.AccountServices
{
    public class SignupServices(
        IUnitOfWork unit,
        IBaseHelperServices<Account> helper,
        IHashPassword hashPassword, ILogService logger, IServiceResultFactory<SignupServices> serviceResultFactory) : ISignupServices
    {
        private readonly IUnitOfWork _unit = unit;
        private readonly IBaseHelperServices<Account> _helper = helper;
        private readonly IHashPassword _hashPassword = hashPassword;
        private readonly ILogService _logger = logger;
        private readonly IServiceResultFactory<SignupServices> _serviceResultFactory = serviceResultFactory;

        //NOTE: SIGNUP FUNCTIONALITY
        public async Task<ServiceResult<Account>> AddAccountAsync(Account entity, CancellationToken cancellationToken)
        {
            List<JsonLogEntry> logEntries = [];
            await _unit.BeginTransactionAsync(cancellationToken);
            try
            {
                if (await _helper.IsExistObject(entity.UserName, _unit.Accounts.GetByUserNameAsync, cancellationToken))
                    return _serviceResultFactory.CreateServiceResult("Account with the same username already exists.", entity, false);
                if (!await _hashPassword.IsPasswordValidAsync(entity.Password))
                    return _serviceResultFactory.CreateServiceResult("Password does not meet the required criteria.", entity, false);

                entity.Password = await _hashPassword.HashPasswordAsync(entity.Password, cancellationToken);
                entity = await _unit.Accounts.AddAsync(entity, cancellationToken);
                if (entity.AccountId <= 0)
                {
                    await _unit.RollbackTransactionAsync(cancellationToken);
                    logEntries.Add(_logger.JsonLogWarning<Account, SignupServices>($"Failed to insert the account for username: {entity.UserName}."));
                    return _serviceResultFactory.CreateServiceResult(entity, logEntries, false);
                }

                logEntries.Add(_logger.JsonLogInfo<Account, SignupServices>($"Account inserted successfully for username: {entity.UserName}.", affectedRows: 1));
                await _unit.CommitTransactionAsync(cancellationToken);
                return _serviceResultFactory.CreateServiceResult(entity, logEntries, true);
            }
            catch (TaskCanceledException ex)
            {
                await _unit.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, SignupServices>("Operation canceled while inserting the account.", ex));
                return _serviceResultFactory.CreateServiceResult(entity, logEntries, false);
            }
            catch (OperationCanceledException ex)
            {
                await _unit.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, SignupServices>("Operation canceled while inserting the account.", ex));
                return _serviceResultFactory.CreateServiceResult(entity, logEntries, false);
            }
            catch (Exception ex)
            {
                await _unit.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, SignupServices>("An error occurred while inserting the account.", ex));
                return _serviceResultFactory.CreateServiceResult(entity, logEntries, false);
            }
        }

        public async Task<ServiceResults<Account>> AddAccountsAsync(IEnumerable<Account> entities, HttpContext httpContext, CancellationToken cancellationToken = default)
        {
            List<JsonLogEntry> logEntries = [];
            await _unit.BeginTransactionAsync(cancellationToken);
            try
            {
                IEnumerable<string> userNames = entities.Select(account => account.UserName);
                if (await _helper.DoNoneOfIdsExistAsync(userNames, (userNames, token) => _unit.Accounts.GetByUserNamesAsync(userNames, cancellationToken: token), cancellationToken))
                {
                    logEntries.Add(_logger.JsonLogWarning<Account, SignupServices>("One or more accounts with the same usernames already exist."));
                    return _serviceResultFactory.CreateServiceResults<Account>([], logEntries, false);
                }
                entities = await HashPasswordAsync(entities);
                entities = await _unit.Accounts.AddRangeAsync(entities, cancellationToken);
                if (!entities.Any())
                {
                    await _unit.RollbackTransactionAsync(cancellationToken);
                    logEntries.Add(_logger.JsonLogWarning<Account, SignupServices>("Failed to insert the accounts."));
                    return _serviceResultFactory.CreateServiceResults<Account>([], logEntries, false);
                }
                logEntries.Add(_logger.JsonLogInfo<Account, SignupServices>($"Accounts inserted successfully.", affectedRows: entities.Count()));
                await _unit.CommitTransactionAsync(cancellationToken);
                return _serviceResultFactory.CreateServiceResults(entities, logEntries, true);
            }
            catch (TaskCanceledException ex)
            {
                await _unit.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, SignupServices>("Operation canceled while inserting accounts.", ex));
                return _serviceResultFactory.CreateServiceResults<Account>([], logEntries, false);
            }
            catch (OperationCanceledException ex)
            {
                await _unit.RollbackTransactionAsync(cancellationToken);
                logEntries.Add(_logger.JsonLogError<Account, SignupServices>("Operation canceled while inserting accounts.", ex));
                return _serviceResultFactory.CreateServiceResults<Account>([], logEntries, false);
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<Account, SignupServices>("An error occurred while inserting accounts.", ex));
                return _serviceResultFactory.CreateServiceResults<Account>([], logEntries, false);
            }
        }

        private static bool IsHavePermissionAsync(HttpContext httpContext)
        {
            ClaimsPrincipal principal = BaseAuthorizationService.GetCurrentUser(httpContext);
            if (principal == null)
                return false;
            return principal.IsInRole("Admin");
        }
        
        private async Task<IEnumerable<Account>> HashPasswordAsync(IEnumerable<Account> entities)
        {
            foreach (Account entity in entities)
            {
                if (!await _hashPassword.IsPasswordValidAsync(entity.Password))
                    throw new ArgumentException(nameof(entity.Password), $"Password for user {entity.UserName} does not meet the required criteria.");
                entity.Password = await _hashPassword.HashPasswordAsync(entity.Password);
            }
            return entities;
        }
    }
}
