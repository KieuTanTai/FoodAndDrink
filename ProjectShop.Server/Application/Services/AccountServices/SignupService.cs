using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Application.Services.AccountServices
{
    public class SignupService : ISignupServices<AccountModel>
    {
        private readonly IDAO<AccountModel> _baseDAO;
        private readonly IAccountDAO<AccountModel> _accountDAO;
        private readonly IBaseHelperServices<AccountModel> _helper;
        private readonly IHashPassword _hashPassword;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<SignupService> _serviceResultFactory;

        public SignupService(
            IDAO<AccountModel> baseDAO,
            IAccountDAO<AccountModel> accountDAO,
            IBaseHelperServices<AccountModel> helper,
            IHashPassword hashPassword, ILogService logger, IServiceResultFactory<SignupService> serviceResultFactory)
        {
            _logger = logger;
            _baseDAO = baseDAO;
            _accountDAO = accountDAO;
            _helper = helper;
            _hashPassword = hashPassword;
            _serviceResultFactory = serviceResultFactory;
        }

        //NOTE: SIGNUP FUNCTIONALITY
        public async Task<ServiceResult<AccountModel>> AddAccountAsync(AccountModel entity)
        {
            List<JsonLogEntry> logEntries = [];
            try
            {
                if (await _helper.IsExistObject(entity.UserName, _accountDAO.GetByUserNameAsync))
                    return _serviceResultFactory.CreateServiceResult("Account with the same username already exists.", entity, false);
                if (!await _hashPassword.IsPasswordValidAsync(entity.Password))
                    return _serviceResultFactory.CreateServiceResult("Password does not meet the required criteria.", entity, false);

                entity.Password = await _hashPassword.HashPasswordAsync(entity.Password);
                int affectedRows = await _baseDAO.InsertAsync(entity);
                if (affectedRows == 0)
                {
                    logEntries.Add(_logger.JsonLogWarning<AccountModel, SignupService>($"Failed to insert the account for username: {entity.UserName}."));
                    return _serviceResultFactory.CreateServiceResult(entity, logEntries, false);
                }

                logEntries.Add(_logger.JsonLogInfo<AccountModel, SignupService>($"Account inserted successfully for username: {entity.UserName}.", affectedRows: affectedRows));
                return _serviceResultFactory.CreateServiceResult(entity, logEntries, true);
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<AccountModel, SignupService>("An error occurred while inserting the account.", ex));
                return _serviceResultFactory.CreateServiceResult(entity, logEntries, false);
            }
        }

        public async Task<ServiceResults<AccountModel>> AddAccountsAsync(IEnumerable<AccountModel> entities)
        {
            List<JsonLogEntry> logEntries = [];
            try
            {
                var filteredEntities = await _helper.FilterValidEntities(entities, entity => entity.UserName, _accountDAO.GetByUserNameAsync);
                filteredEntities.TryGetValue(filteredEntities.Keys.FirstOrDefault(), out var serviceResults);
                if (serviceResults == null || serviceResults.Data == null || !serviceResults.Data.Any())
                {
                    logEntries = serviceResults?.LogEntries?.ToList() ?? [];
                    logEntries.Add(_logger.JsonLogWarning<AccountModel, SignupService>("No valid accounts to insert after filtering."));
                    return _serviceResultFactory.CreateServiceResults<AccountModel>([], logEntries, false);
                }

                // Hash passwords for valid entities
                var validEntities = serviceResults.Data;
                validEntities = await HashPasswordAsync(validEntities!);
                int affectedRows = await _baseDAO.InsertAsync(validEntities);
                if (affectedRows == 0)
                {
                    logEntries.Add(_logger.JsonLogWarning<AccountModel, SignupService>("Failed to insert the accounts.", affectedRows: affectedRows));
                    return _serviceResultFactory.CreateServiceResults(validEntities, logEntries, false);
                }
                logEntries.Add(_logger.JsonLogInfo<AccountModel, SignupService>($"Accounts inserted successfully.", affectedRows: affectedRows));
                return _serviceResultFactory.CreateServiceResults(validEntities, logEntries, true);
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<AccountModel, SignupService>("An error occurred while inserting accounts.", ex));
                return _serviceResultFactory.CreateServiceResults<AccountModel>([], logEntries, false);
            }
        }

        private async Task<IEnumerable<AccountModel>> HashPasswordAsync(IEnumerable<AccountModel> entities)
        {
            foreach (AccountModel entity in entities)
            {
                if (!await _hashPassword.IsPasswordValidAsync(entity.Password))
                    throw new ArgumentException(nameof(entity.Password), $"Password for user {entity.UserName} does not meet the required criteria.");
                entity.Password = await _hashPassword.HashPasswordAsync(entity.Password);
            }
            return entities;
        }
    }
}
