// File: Account.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class AccountModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _accountId;
        private string _userName = string.Empty;
        private string _password = string.Empty;
        private DateTime _accountCreatedDate;
        private DateTime _accountLastUpdatedDate = DateTime.UtcNow;
        private bool _accountStatus;

        // Corresponds to 'account_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint AccountId
        {
            get => _accountId;
            set => _accountId = value;
        }

        // Corresponds to 'user_name' (VARCHAR(20) UNIQUE)
        public string UserName
        {
            get => _userName;
            set => _userName = value ?? string.Empty;
        }

        // Corresponds to 'password' (VARCHAR(25))
        public string Password
        {
            get => _password;
            set => _password = value ?? string.Empty;
        }

        // Corresponds to 'account_create_date' (DATETIME)
        public DateTime AccountCreatedDate
        {
            get => _accountCreatedDate;
            set => _accountCreatedDate = value;
        }

        // corresponds to 'account_update_date' (DATETIME)
        public DateTime AccountLastUpdatedDate
        {
            get => _accountLastUpdatedDate;
            set => _accountLastUpdatedDate = value;
        }

        // Corresponds to 'account_status' (TINYINT(1))
        public bool AccountStatus
        {
            get => _accountStatus;
            set => _accountStatus = value;
        }

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

