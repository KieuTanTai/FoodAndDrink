namespace ProjectShop.Server.Core.ValueObjects.Requests.FrontEndRequestsForRole
{
    public class RoleUpdateStatusRequest
    {
        public IEnumerable<uint> RoleIds { get; set; } = [];
        public bool Status { get; set; }
    }
}
