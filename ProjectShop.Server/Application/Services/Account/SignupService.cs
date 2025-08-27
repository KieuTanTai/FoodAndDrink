using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Application.Services.Account
{
    public class SignupService : ISignupService<AccountModel>
    {
        private readonly IDAO<AccountModel> _baseDAO;
        private readonly IAccountDAO<AccountModel> _accountDAO;
        private readonly IBaseHelperService<AccountModel> _helper;
        private readonly IHashPassword _hashPassword;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<SignupService> _serviceResultFactory;

        public SignupService(
            IDAO<AccountModel> baseDAO,
            IAccountDAO<AccountModel> accountDAO,
            IBaseHelperService<AccountModel> helper,
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
                    return _serviceResultFactory.CreateServiceResult<AccountModel>("Account with the same username already exists.", entity, false);
                if (!await _hashPassword.IsPasswordValidAsync(entity.Password))
                    return _serviceResultFactory.CreateServiceResult<AccountModel>("Password does not meet the required criteria.", entity, false);

                entity.Password = await _hashPassword.HashPasswordAsync(entity.Password);
                int affectedRows = await _baseDAO.InsertAsync(entity);
                if (affectedRows == 0)
                    logEntries.Add(_logger.JsonLogWarning<AccountModel, SignupService>($"Failed to insert the account for username: {entity.UserName}."));
                logEntries.Add(_logger.JsonLogInfo<AccountModel, SignupService>($"Account inserted successfully for username: {entity.UserName}.", affectedRows: affectedRows));
                return _serviceResultFactory.CreateServiceResult<AccountModel>(entity, logEntries);
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<AccountModel, SignupService>("An error occurred while inserting the account.", ex));
                return _serviceResultFactory.CreateServiceResult<AccountModel>(entity, logEntries);
            }
        }

        public async Task<ServiceResults<AccountModel>> AddAccountsAsync(IEnumerable<AccountModel> entities)
        {
            try
            {
                var filteredEntities = await _helper.FilterValidEntities(entities, entity => entity.UserName, _accountDAO.GetByUserNameAsync);
                filteredEntities.TryGetValue(filteredEntities.Keys.FirstOrDefault(), out var serviceResults);
                if (!serviceResults!.Data!.Any())
                    return _serviceResultFactory.CreateServiceResults<AccountModel>([], serviceResults.LogEntries!.Append(_logger.JsonLogWarning<AccountModel, SignupService>("No valid accounts to insert after filtering.", null)));

                // Hash passwords for valid entities
                var validEntities = serviceResults.Data;
                validEntities = await HashPasswordAsync(validEntities!);
                int affectedRows = await _baseDAO.InsertAsync(validEntities);
                if (affectedRows == 0)
                    serviceResults.LogEntries = serviceResults.LogEntries!.Append(_logger.JsonLogWarning<AccountModel, SignupService>("Failed to insert the accounts."));
                serviceResults.LogEntries = serviceResults.LogEntries!.Append(_logger.JsonLogInfo<AccountModel, SignupService>($"Accounts inserted successfully.", affectedRows: affectedRows));
                return serviceResults;
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<AccountModel>([],
                [
                    _logger.JsonLogError<AccountModel, SignupService>("An error occurred while inserting multiple accounts.", ex)
                ]);
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
