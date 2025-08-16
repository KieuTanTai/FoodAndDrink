using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class CurrentAccountLoginService : ICurrentAccountLogin<AccountModel, RolesOfUserModel>
    {
        public static AccountModel CurrentLogin { get; private set; } = null!;
        private readonly IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> _roleOfUser;

        public CurrentAccountLoginService(IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey> roleOfUser)
        {
            _roleOfUser = roleOfUser ?? throw new ArgumentNullException(nameof(roleOfUser), "Role of user DAO cannot be null.");
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetCurrentAccountRolesAsync()
        {
            try
            {
                if (CurrentLogin == null)
                    throw new InvalidOperationException("No account is currently logged in.");
                IEnumerable<RolesOfUserModel> rolesOfUserModels = await _roleOfUser.GetByAccountIdAsync(CurrentLogin.AccountId);
                if (rolesOfUserModels == null || !rolesOfUserModels.Any())
                    throw new InvalidOperationException("No roles found for the current account.");
                return rolesOfUserModels;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving the current account roles.", ex);
            }
        }

        public AccountModel GetCurrentAccountAsync()
        {
            if (CurrentLogin == null)
                throw new InvalidOperationException("No account is currently logged in.");
            return CurrentLogin;
        }

        public void SetAccount(AccountModel currentAccount)
        {
            if (currentAccount == null)
                throw new ArgumentNullException(nameof(currentAccount), "Current account cannot be null.");
            CurrentLogin = currentAccount;
        }
    }
}
