namespace ProjectShop.Server.Core.ValueObjects
{
    public class AccountUpdateStatusRequest
    {
        public uint AccountId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool Status { get; set; } = false;

    }
}
