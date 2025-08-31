namespace ProjectShop.Server.Core.ValueObjects
{
    public class UpdateAccountPasswordRequest
    {
        public uint AccountId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
