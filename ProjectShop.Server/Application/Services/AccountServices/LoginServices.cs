using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using System.Security.Claims;

namespace ProjectShop.Server.Application.Services.AccountServices
{
    public class LoginServices : ILoginServices
    {
        private readonly IUnitOfWork _unit;
        private readonly IHashPassword _hashPassword;
        private readonly IServiceResultFactory<LoginServices> _serviceResultFactory;
        private readonly ILogService _logger;

        public LoginServices(IUnitOfWork unit, IHashPassword hashPassword,
            IServiceResultFactory<LoginServices> serviceResultFactory,
            ILogService logger)
        {
            _unit = unit;
            _hashPassword = hashPassword;
            _serviceResultFactory = serviceResultFactory;
            _logger = logger;
        }

        public async Task<ServiceResult<Account>> HandleLoginAsync(string userName, string password,
            AccountNavigationOptions? options, CancellationToken cancellationToken)
        {
            ServiceResult<Account> result = new(true);
            try
            {
                Account? account = await _unit.Accounts.GetByUserNameAsync(userName, cancellationToken);
                if (account == null)
                    return _serviceResultFactory.CreateServiceResult($"Account with username '{userName}' not found.", new Account(), false);
                // validate account status and password
                if (!account.AccountStatus)
                    return _serviceResultFactory.CreateServiceResult($"Account is inactive.", new Account(), false);

                string accountPassword = account.Password;
                if (!await _hashPassword.ComparePasswordsAsync(accountPassword, password, cancellationToken))
                    return _serviceResultFactory.CreateServiceResult($"Invalid password.", new Account(), false);

                if (options != null)
                    account = await _unit.Accounts.ExplicitLoadAsync(account, options, cancellationToken);
                result.LogEntries = result.LogEntries!.Append(_logger.JsonLogInfo<Account, LoginServices>($"Login successful for user: {userName}"));
                return result;
            }
            catch (TaskCanceledException ex)
            {
                return _serviceResultFactory.CreateServiceResult("Login operation was canceled.", new Account(), false, ex);
            }
            catch (OperationCanceledException ex)
            {
                return _serviceResultFactory.CreateServiceResult("Login operation was canceled.", new Account(), false, ex);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult("An error occurred during login.", new Account(), false, ex);
            }
        }
    }
}
