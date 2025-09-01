using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services.Roles
{
    public class BaseReturnAccountRoleService : IBaseGetNavigationPropertyServices<RolesOfUserModel, RolesOfUserNavigationOptions>
    {
        private readonly IDAO<RoleModel> _baseRoleDAO;
        private readonly IDAO<AccountModel> _baseAccountDAO;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<BaseReturnAccountRoleService> _serviceResultFactory;

        public BaseReturnAccountRoleService(IDAO<RoleModel> baseRoleDAO, IDAO<AccountModel> baseAccountDAO, ILogService logger, IServiceResultFactory<BaseReturnAccountRoleService> serviceResultFactory)
        {
            _baseRoleDAO = baseRoleDAO;
            _baseAccountDAO = baseAccountDAO;
            _logger = logger;
            _serviceResultFactory = serviceResultFactory;
        }

        // Method orchestrator

        public async Task<ServiceResult<RolesOfUserModel>> GetNavigationPropertyByOptionsAsync(RolesOfUserModel role, RolesOfUserNavigationOptions? options, [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            if (options?.IsGetRole == true)
                await LoadRoleAsync(role, logEntries);

            if (options?.IsGetAccount == true)
                await LoadAccountAsync(role, logEntries);

            logEntries.Add(_logger.JsonLogInfo<RolesOfUserModel, BaseReturnAccountRoleService>("Successfully retrieved account's roles with navigation properties.", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResult(role, logEntries);
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetNavigationPropertyByOptionsAsync(
            IEnumerable<RolesOfUserModel> roles,
            RolesOfUserNavigationOptions? options,
            [CallerMemberName] string? methodCall = null)
        {
            var roleList = roles.ToList();
            List<JsonLogEntry> logEntries = [];
            if (options?.IsGetRole == true)
                await LoadRolesAsync(roleList, logEntries);

            if (options?.IsGetAccount == true)
                await LoadAccountsAsync(roleList, logEntries);

            logEntries.Add(_logger.JsonLogInfo<RolesOfUserModel, BaseReturnAccountRoleService>("Successfully retrieved account's roles with navigation properties.", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResults(roleList, logEntries);
        }

        private async Task<ServiceResult<RoleModel>> TryLoadRoleAsync(uint roleId)
        {
            try
            {
                RoleModel? roleModel = await _baseRoleDAO.GetSingleDataAsync(roleId.ToString());
                bool isExists = roleModel != null;
                roleModel ??= new RoleModel();
                string message = isExists ? "Role loaded successfully." : "Role not found.";
                return _serviceResultFactory.CreateServiceResult(message, roleModel, isExists);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult("Error loading role.", new RoleModel(), false, ex);
            }
        }

        private async Task<ServiceResult<AccountModel>> TryLoadAccountAsync(uint accountId)
        {
            try
            {
                AccountModel? accountModel = await _baseAccountDAO.GetSingleDataAsync(accountId.ToString());
                bool isExists = accountModel != null;
                accountModel ??= new AccountModel();
                string message = isExists ? "Account loaded successfully." : "Account not found.";
                return _serviceResultFactory.CreateServiceResult(message, accountModel, isExists);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult("Error loading account.", new AccountModel(), false, ex);
            }
        }

        private async Task<IDictionary<uint, ServiceResult<RoleModel>>> TryLoadRolesAsyncs(IEnumerable<uint> roleIds)
        {
            uint firstRoleId = roleIds.FirstOrDefault();
            try
            {
                IEnumerable<RoleModel> roles = await _baseRoleDAO.GetByInputsAsync(roleIds.Select(id => id.ToString()));
                if (roles == null || !roles.Any())
                {
                    return new Dictionary<uint, ServiceResult<RoleModel>>
                    {
                        [firstRoleId] = _serviceResultFactory.CreateServiceResult("No roles found.", new RoleModel(), false)
                    };
                }

                return roles.ToDictionary(role => role.RoleId, role => _serviceResultFactory.CreateServiceResult("Role loaded successfully.", role, true));
            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResult<RoleModel>>
                {
                    [firstRoleId] = _serviceResultFactory.CreateServiceResult("Error loading roles.", new RoleModel(), false, ex)
                };
            }
        }

        private async Task<IDictionary<uint, ServiceResult<AccountModel>>> TryLoadAccountsAsyncs(IEnumerable<uint> accountIds)
        {
            try
            {
                IEnumerable<AccountModel> accounts = await _baseAccountDAO.GetByInputsAsync(accountIds.Select(id => id.ToString()));
                if (accounts == null || !accounts.Any())
                {
                    uint firstAccountId = accountIds.FirstOrDefault();
                    return new Dictionary<uint, ServiceResult<AccountModel>>
                    {
                        [firstAccountId] = _serviceResultFactory.CreateServiceResult("No accounts found.", new AccountModel(), false)
                    };
                }
                return accounts.ToDictionary(account => account.AccountId, account => _serviceResultFactory.CreateServiceResult("Account loaded successfully.", account, true));
            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResult<AccountModel>>
                {
                    [accountIds.FirstOrDefault()] = _serviceResultFactory.CreateServiceResult("Error loading accounts.", new AccountModel(), false, ex)
                };
            }
        }

        // Helper cho 1 role
        private async Task LoadRoleAsync(RolesOfUserModel role, List<JsonLogEntry> logEntries)
        {
            var result = await TryLoadRoleAsync(role.RoleId);
            logEntries.AddRange(result.LogEntries!);
            role.Role = result.Data!;
            logEntries.Add(_logger.JsonLogInfo<RolesOfUserModel, BaseReturnAccountRoleService>($"Loaded Role for account's roles with RoleId {role.RoleId}."));
        }

        private async Task LoadAccountAsync(RolesOfUserModel role, List<JsonLogEntry> logEntries)
        {
            var result = await TryLoadAccountAsync(role.AccountId);
            logEntries.AddRange(result.LogEntries!);
            role.Account = result.Data!;
            logEntries.Add(_logger.JsonLogInfo<RolesOfUserModel, BaseReturnAccountRoleService>($"Loaded Account for account's roles with AccountId {role.AccountId}."));
        }

        // Helper cho nhiều roles
        private async Task LoadRolesAsync(IEnumerable<RolesOfUserModel> roles, List<JsonLogEntry> logEntries)
        {
            var roleList = roles.ToList();
            var roleIds = roleList.Select(r => r.RoleId).ToList();
            var rolesDict = await TryLoadRolesAsyncs(roleIds);
            foreach (var role in roleList)
            {
                if (!rolesDict.TryGetValue(role.RoleId, out var serviceResult) || serviceResult == null || serviceResult.Data == null)
                {
                    role.Role = new RoleModel();
                    continue;
                }
                logEntries.AddRange(serviceResult.LogEntries!);
                role.Role = serviceResult.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<RolesOfUserModel, BaseReturnAccountRoleService>(" Loaded Roles for account's roles."));
        }

        private async Task LoadAccountsAsync(IEnumerable<RolesOfUserModel> roles, List<JsonLogEntry> logEntries)
        {
            var roleList = roles.ToList();
            var accountIds = roleList.Select(r => r.AccountId).ToList();
            var accountsDict = await TryLoadAccountsAsyncs(accountIds);
            foreach (var role in roleList)
            {
                if (!accountsDict.TryGetValue(role.AccountId, out var serviceResult) || serviceResult == null || serviceResult.Data == null)
                {
                    role.Account = new AccountModel();
                    continue;
                }
                logEntries.AddRange(serviceResult.LogEntries!);
                role.Account = serviceResult.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<RolesOfUserModel, BaseReturnAccountRoleService>(" Loaded Accounts for account's roles."));
        }
    }
}
