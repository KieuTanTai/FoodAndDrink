namespace ProjectShop.Server.Core.ValueObjects
{
    public class RoleUpdateStatusRequest
    {
        public IEnumerable<uint> RoleIds { get; set; } = [];
        public bool Status { get; set; }
    }
}
