namespace ProjectShop.Server.Core.ValueObjects
{
    public class AccountUpdateStatusRequest
    {
        // Backing fields
        private uint _accountId;
        private string _userName = string.Empty;
        private bool _status = false;

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

        public bool Status
        {
            get => _status;
            set => _status = value;
        }
    }
}
