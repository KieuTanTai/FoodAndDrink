// File: Account.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class AccountModel : IGetIdEntity<uint>
    {
        // Corresponds to 'account_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint AccountId { get; private set; }

        // Corresponds to 'user_name' (VARCHAR(20) UNIQUE)
        public string UserName { get; private set; }

        // Corresponds to 'password' (VARCHAR(25))
        public string Password { get; private set; }

        // Corresponds to 'account_create_date' (DATETIME)
        public DateTime AccountCreatedDate { get; private set; }

        // corresponds to 'account_update_date' (DATETIME)
        public DateTime AccountLastUpdatedDate { get; private set; } = DateTime.UtcNow;

        // Corresponds to 'account_status' (TINYINT(1))
        public bool AccountStatus { get; private set; }

        // Navigation properties
        public CustomerModel Customer { get; private set; } = null!;
        public EmployeeModel Employee { get; private set; } = null!;
        public ICollection<RolesOfUserModel> RolesOfUsers { get; private set; } = new List<RolesOfUserModel>();

        public AccountModel(uint accountId, string userName, string password, DateTime accountCreatedDate, DateTime accountLastUpdatedDate, bool accountStatus)
        {
            AccountId = accountId;
            UserName = userName;
            Password = password;
            AccountCreatedDate = accountCreatedDate;
            AccountLastUpdatedDate = accountLastUpdatedDate;
            AccountStatus = accountStatus;
        }

        public uint GetIdEntity() => AccountId;
    }
}

