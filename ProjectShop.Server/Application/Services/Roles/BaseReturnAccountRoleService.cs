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

        public async Task<ServiceResult<RolesOfUserModel>> GetNavigationPropertyByOptionsAsync(RolesOfUserModel role, RolesOfUserNavigationOptions? options, [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            if (options?.IsGetRole == true)
            {
                ServiceResult<RoleModel> result = await TryLoadRoleAsync(role.RoleId, methodCall);
                logEntries.AddRange(result.LogEntries!);
                role.Role = result.Data!;
            }

            if (options?.IsGetAccount == true)
            {
                ServiceResult<AccountModel> result = await TryLoadAccountAsync(role.AccountId, methodCall);
                logEntries.AddRange(result.LogEntries!);
                role.Account = result.Data!;
            }
            logEntries.Add(_logger.JsonLogInfo<RolesOfUserModel, BaseReturnAccountRoleService>("GetNavigationPropertyByOptionsAsync completed."));
            return _serviceResultFactory.CreateServiceResult(role, logEntries);
        }

        public async Task<ServiceResults<RolesOfUserModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<RolesOfUserModel> roles, RolesOfUserNavigationOptions? options, [CallerMemberName] string? methodCall = null)
        {
            var roleList = roles.ToList();
            List<JsonLogEntry> logEntries = [];
            if (options?.IsGetRole == true)
            {
                var roleIds = roleList.Select(r => r.RoleId).ToList();
                var rolesDict = await TryLoadRolesAsyncs(roleIds, methodCall);
                foreach (var role in roleList)
                {
                    rolesDict.TryGetValue(role.RoleId, out var serviceResult);
                    logEntries.AddRange(serviceResult!.LogEntries!);
                    if (serviceResult.Data!.RoleId == 0)
                        break;
                    role.Role = serviceResult.Data!;
                }
            }

            if (options?.IsGetAccount == true)
            {
                var accountIds = roleList.Select(r => r.AccountId).ToList();
                var accountsDict = await TryLoadAccountsAsyncs(accountIds, methodCall);
                foreach (var role in roleList)
                {
                    accountsDict.TryGetValue(role.AccountId, out var serviceResult);
                    logEntries.AddRange(serviceResult!.LogEntries!);
                    if (serviceResult.Data!.AccountId == 0)
                        break;
                    role.Account = serviceResult.Data!;
                }
            }
            logEntries.Add(_logger.JsonLogInfo<RolesOfUserModel, BaseReturnAccountRoleService>("GetNavigationPropertyByOptionsAsync completed."));
            return _serviceResultFactory.CreateServiceResults(roleList, logEntries);
        }

        private async Task<ServiceResult<RoleModel>> TryLoadRoleAsync(uint roleId, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                RoleModel? roleModel = await _baseRoleDAO.GetSingleDataAsync(roleId.ToString());
                bool isExists = roleModel != null;
                roleModel ??= new RoleModel();
                string message = isExists ? "Role loaded successfully." : "Role not found.";
                return _serviceResultFactory.CreateServiceResult(message, roleModel, isExists, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult("Error loading role.", new RoleModel(), false, ex, methodCall: methodCall);
            }
        }

        private async Task<ServiceResult<AccountModel>> TryLoadAccountAsync(uint accountId, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                AccountModel? accountModel = await _baseAccountDAO.GetSingleDataAsync(accountId.ToString());
                bool isExists = accountModel != null;
                accountModel ??= new AccountModel();
                string message = isExists ? "Account loaded successfully." : "Account not found.";
                return _serviceResultFactory.CreateServiceResult(message, accountModel, isExists, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult("Error loading account.", new AccountModel(), false, ex, methodCall: methodCall);
            }
        }

        private async Task<IDictionary<uint, ServiceResult<RoleModel>>> TryLoadRolesAsyncs(IEnumerable<uint> roleIds, [CallerMemberName] string? methodCall = null)
        {
            uint firstRoleId = roleIds.FirstOrDefault();
            try
            {
                IEnumerable<RoleModel> roles = await _baseRoleDAO.GetByInputsAsync(roleIds.Select(id => id.ToString()));
                if (roles == null || !roles.Any())
                {
                    return new Dictionary<uint, ServiceResult<RoleModel>>
                    {
                        [firstRoleId] = _serviceResultFactory.CreateServiceResult("No roles found.", new RoleModel(), false, methodCall: methodCall)
                    };
                }

                return roles.ToDictionary(role => role.RoleId, role => _serviceResultFactory.CreateServiceResult("Role loaded successfully.", role, true, methodCall: methodCall));
            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResult<RoleModel>>
                {
                    [firstRoleId] = _serviceResultFactory.CreateServiceResult("Error loading roles.", new RoleModel(), false, ex, methodCall: methodCall)
                };
            }
        }

        private async Task<IDictionary<uint, ServiceResult<AccountModel>>> TryLoadAccountsAsyncs(IEnumerable<uint> accountIds, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                IEnumerable<AccountModel> accounts = await _baseAccountDAO.GetByInputsAsync(accountIds.Select(id => id.ToString()));
                if (accounts == null || !accounts.Any())
                {
                    uint firstAccountId = accountIds.FirstOrDefault();
                    return new Dictionary<uint, ServiceResult<AccountModel>>
                    {
                        [firstAccountId] = _serviceResultFactory.CreateServiceResult("No accounts found.", new AccountModel(), false, methodCall: methodCall)
                    };
                }
                return accounts.ToDictionary(account => account.AccountId, account => _serviceResultFactory.CreateServiceResult("Account loaded successfully.", account, true, methodCall: methodCall));
            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResult<AccountModel>>
                {
                    [accountIds.FirstOrDefault()] = _serviceResultFactory.CreateServiceResult("Error loading accounts.", new AccountModel(), false, ex, methodCall: methodCall)
                };
            }
        }
    }
}
