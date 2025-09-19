namespace ProjectShop.Server.Core.ValueObjects
{
    public class AccountUpdatePasswordRequest
    {
        // Backing fields
        private uint _accountId;
        private string _userName = string.Empty;
        private string _newPassword = string.Empty;

        public uint AccountId
        {
            get => _accountId;
            set => _accountId = value;
        }

        public string UserName
        {
            get => _userName;
            set => _userName = value ?? string.Empty;
        }

        public string NewPassword
        {
            get => _newPassword;
            set => _newPassword = value ?? string.Empty;
        }
    }
}
