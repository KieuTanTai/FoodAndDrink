namespace ProjectShop.Server.Core.Entities.EntitiesRequest
{
    public class UpdateAccountStatusRequest
    {
        public uint AccountId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool Status { get; set; } = false;

    }
}
