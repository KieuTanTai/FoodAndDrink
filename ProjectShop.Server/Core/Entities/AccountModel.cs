// File: Account.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class AccountModel : IGetIdEntity<uint>
    {
        // Corresponds to 'account_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint AccountId { get; set; }

        // Corresponds to 'user_name' (VARCHAR(20) UNIQUE)
        public string UserName { get; set; } = string.Empty;

        // Corresponds to 'password' (VARCHAR(25))
        public string Password { get; set; } = string.Empty;

        // Corresponds to 'account_create_date' (DATETIME)
        public DateTime AccountCreatedDate { get; set; }

        // corresponds to 'account_update_date' (DATETIME)
        public DateTime AccountLastUpdatedDate { get; set; } = DateTime.UtcNow;

        // Corresponds to 'account_status' (TINYINT(1))
        public bool AccountStatus { get; set; }

        // Navigation properties
        public CustomerModel Customer { get; set; } = null!;
        public EmployeeModel Employee { get; set; } = null!;
        public ICollection<RolesOfUserModel> RolesOfUsers { get; set; } = new List<RolesOfUserModel>();
        // End of navigation properties

        public AccountModel() { }

        public AccountModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
            AccountCreatedDate = DateTime.UtcNow;
            AccountLastUpdatedDate = DateTime.UtcNow;
            AccountStatus = true;
        }

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

