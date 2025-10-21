using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.AccountServices
{
    public class BaseGetAccountNavigationPropertiesService : IBaseGetNavigationPropertyServices<Account, AccountNavigationOptions>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<BaseGetAccountNavigationPropertiesService> _serviceResultFactory;
        private readonly IBaseHelperReturnTEntityService<BaseGetAccountNavigationPropertiesService> _baseHelperReturnAccountService;
        public BaseGetAccountNavigationPropertiesService(IUnitOfWork unitOfWork, ILogService logger,
            IServiceResultFactory<BaseGetAccountNavigationPropertiesService> serviceResultFactory,
            IBaseHelperReturnTEntityService<BaseGetAccountNavigationPropertiesService> baseHelperReturnAccountService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _serviceResultFactory = serviceResultFactory;
            _baseHelperReturnAccountService = baseHelperReturnAccountService;
        }

        // Method gốc: orchestrator, chỉ gọi các method đã tách nhỏ
        public async Task<ServiceResult<Account>> GetNavigationPropertyByOptionsAsync(Account account, AccountNavigationOptions options, [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            bool isSuccess = true;

            if (options.IsGetAccountAdditionalPermissions)
            {
                await LoadAdditionalPermissionsAsync(account, logEntries);
                isSuccess = ValidateAdditionalPermissionsLoaded(account, isSuccess);
            }

            if (options.IsGetAccountRoles)
            {
                await LoadAccountRolesAsync(account, logEntries);
                isSuccess = ValidateRolesLoaded(account, isSuccess);
            }
            
            if (options.IsGetPerson)
            {
                await LoadPersonAsync(account, logEntries);
                isSuccess = ValidatePersonLoaded(account, isSuccess);
            }

            AddFinalLogEntry(logEntries, isSuccess, methodCall);
            return _serviceResultFactory.CreateServiceResult(account, logEntries, isSuccess);
        }

        public async Task<ServiceResults<Account>> GetNavigationPropertyByOptionsAsync(IEnumerable<Account> accounts, AccountNavigationOptions options, [CallerMemberName] string? methodCall = null)
        {
            var accountList = accounts.ToList();
            List<JsonLogEntry> logEntries = [];
            bool isSuccess = true;

            if (options.IsGetAccountAdditionalPermissions)
            {
                await LoadAccountPermissionsAsync(accountList, logEntries);
                isSuccess = ValidateAdditionalPermissionsLoaded(accountList, isSuccess);
            }

            if (options.IsGetAccountRoles)
            {
                await LoadAccountRolesAsync(accountList, logEntries);
                isSuccess = ValidateRolesLoaded(accountList, isSuccess);
            }

            if (options.IsGetPerson)
            {
                await LoadPersonsAsync(accountList, logEntries);
                isSuccess = ValidatePersonsLoaded(accountList, isSuccess);
            }

            AddFinalLogEntry(logEntries, isSuccess, methodCall);
            return _serviceResultFactory.CreateServiceResults(accountList, logEntries, isSuccess);
        }

        private async Task<ServiceResults<AccountAdditionalPermission>> GetAccountAdditionalPermissionsAsync(uint accountId)
        {
            async Task<IEnumerable<AccountAdditionalPermission>> queryFunc(uint accountId)
            {
                return await _context.AccountAdditionalPermissions
                    .Where(perm => perm.AccountId == accountId)
                    .ToListAsync();
            }
            return await _baseHelperReturnAccountService.TryLoadICollectionEntitiesAsync<uint, AccountAdditionalPermission>(
                accountId,
                queryFunc,
                methodCall: nameof(GetAccountAdditionalPermissionsAsync)
            );
        }

        private async Task<ServiceResults<AccountRole>> GetAccountRolesAsync(uint accountId)
        {
            async Task<IEnumerable<AccountRole>> queryFunc(uint accountId)
            {
                return await _context.AccountRoles
                    .Where(role => role.AccountId == accountId)
                    .ToListAsync();
            }

            return await _baseHelperReturnAccountService.TryLoadICollectionEntitiesAsync<uint, AccountRole>(
                accountId,
                queryFunc,
                methodCall: nameof(GetAccountRolesAsync)
            );
        }

        private async Task<ServiceResult<Person>> GetPersonAsync(uint accountId)
        {
            async Task<Person?> queryFunc(uint accountId)
            {
                return await _context.People.FirstOrDefaultAsync(p => p.AccountId == accountId);
            }

            return await _baseHelperReturnAccountService.TryLoadEntityAsync<uint, Person>(
                accountId,
                queryFunc,
                () => new Person(),
                methodCall: nameof(GetPersonAsync)
            );
        }

        private async Task<IDictionary<uint, ServiceResults<AccountAdditionalPermission>>> GetAccountAdditionalPermissionsAsync(IEnumerable<uint> accountIds)
        {
            async Task<IEnumerable<AccountAdditionalPermission>> queryFunc(IEnumerable<uint> accountIds)
            {
                return await _context.AccountAdditionalPermissions
                    .Where(perm => accountIds.Contains(perm.AccountId))
                    .ToListAsync();
            }
            return await _baseHelperReturnAccountService.TryLoadICollectionEntitiesAsync<uint, AccountAdditionalPermission>(
                accountIds,
                queryFunc,
                entity => entity.AccountId,
                methodCall: nameof(GetAccountAdditionalPermissionsAsync)
            );
        }

        private async Task<IDictionary<uint, ServiceResults<AccountRole>>> GetAccountRolesAsync(IEnumerable<uint> accountIds)
        {
            async Task<IEnumerable<AccountRole>> queryFunc(IEnumerable<uint> accountIds)
            {
                return await _context.AccountRoles
                    .Where(role => accountIds.Contains(role.AccountId))
                    .ToListAsync();
            }
            return await _baseHelperReturnAccountService.TryLoadICollectionEntitiesAsync<uint, AccountRole>(
                accountIds,
                queryFunc,
                entity => entity.AccountId,
                methodCall: nameof(GetAccountRolesAsync)
            );
        }

        private async Task<IDictionary<uint, ServiceResult<Person>>> GetPersonsAsync(IEnumerable<uint> accountIds)
        {
            async Task<IEnumerable<Person>> queryFunc(IEnumerable<uint> accountIds)
            {
                return await _context.People
                    .Where(p => accountIds.Contains(p.AccountId))
                    .ToListAsync();
            }

            return await _baseHelperReturnAccountService.TryLoadEntitiesAsync<uint, Person>(
                accountIds,
                queryFunc,
                () => new Person(),
                entity => entity.AccountId,
                methodCall: nameof(GetPersonsAsync)
            );
        }

        // Helper methods: mỗi method xử lý 1 navigation property
        private async Task LoadAdditionalPermissionsAsync(Account account, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnAccountService.LoadICollectionEntitiesAsync<uint, AccountAdditionalPermission>(
                permissions => account.AccountAdditionalPermissions = [.. permissions],
                () => account.AccountId, (account) => GetAccountAdditionalPermissionsAsync(account),
                logEntries, nameof(LoadAdditionalPermissionsAsync));

        private async Task LoadAccountRolesAsync(Account account, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnAccountService.LoadICollectionEntitiesAsync<uint, AccountRole>(
                roles => account.AccountRoles = [.. roles],
                () => account.AccountId, (account) => GetAccountRolesAsync(account),
                logEntries, nameof(LoadAccountRolesAsync));

        private async Task LoadPersonAsync(Account account, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnAccountService.LoadEntityAsync<uint, Person>(person => account.Person = person,
                () => account.AccountId, GetPersonAsync, logEntries, nameof(LoadPersonAsync));


        private async Task LoadAccountPermissionsAsync(IEnumerable<Account> accounts, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnAccountService.LoadICollectionEntitiesAsync(accounts,
                (account, permissions) => account.AccountAdditionalPermissions = [.. permissions],
                account => account.AccountId, GetAccountAdditionalPermissionsAsync, logEntries, nameof(LoadAccountPermissionsAsync));

        private async Task LoadAccountRolesAsync(IEnumerable<Account> accounts, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnAccountService.LoadICollectionEntitiesAsync(accounts,
                (account, roles) => account.AccountRoles = [.. roles], account => account.AccountId,
                GetAccountRolesAsync, logEntries, nameof(LoadAccountRolesAsync));

        private async Task LoadPersonsAsync(IEnumerable<Account> accounts, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnAccountService.LoadEntitiesAsync(accounts,
                (account, person) => account.Person = person, account => account.AccountId,
                GetPersonsAsync, logEntries, nameof(LoadPersonsAsync));

        // Validation helper methods for single entity
        private static bool ValidateAdditionalPermissionsLoaded(Account account, bool currentSuccess)
            => currentSuccess && account.AccountAdditionalPermissions != null && account.AccountAdditionalPermissions.Count > 0;

        private static bool ValidateRolesLoaded(Account account, bool currentSuccess)
            => currentSuccess && account.AccountRoles != null && account.AccountRoles.Count > 0;

        private static bool ValidatePersonLoaded(Account account, bool currentSuccess)
            => currentSuccess && account.Person != null && account.Person.PersonId != 0;

        // Validation helper methods for multiple entities
        private static bool ValidateAdditionalPermissionsLoaded(IEnumerable<Account> accounts, bool currentSuccess)
            => currentSuccess && !accounts.Any(account => account.AccountAdditionalPermissions == null
                || account.AccountAdditionalPermissions.Count == 0);

        private static bool ValidateRolesLoaded(IEnumerable<Account> accounts, bool currentSuccess)
            => currentSuccess && !accounts.Any(account => account.AccountRoles == null || account.AccountRoles.Count == 0);

        private static bool ValidatePersonsLoaded(IEnumerable<Account> accounts, bool currentSuccess)
            => currentSuccess && !accounts.Any(account => account.Person == null || account.Person.PersonId == 0);

        // Final log entry helper
        private void AddFinalLogEntry(List<JsonLogEntry> logEntries, bool isSuccess, string? methodCall)
        {
            if (!isSuccess)
                logEntries.Add(_logger.JsonLogWarning<Account, BaseGetAccountNavigationPropertiesService>("One or more navigation properties could not be loaded.", methodCall: methodCall));
            else
                logEntries.Add(_logger.JsonLogInfo<Account, BaseGetAccountNavigationPropertiesService>("Successfully retrieved account with navigation properties.", methodCall: methodCall));
        }
    }
}