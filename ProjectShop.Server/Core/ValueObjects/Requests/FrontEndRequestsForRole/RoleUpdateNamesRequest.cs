namespace ProjectShop.Server.Core.ValueObjects.Requests.FrontEndRequestsForRole
{
    public class RoleUpdateNamesRequest
    {
        public IEnumerable<uint> RoleIds { get; set; } = [];
        public IEnumerable<string> NewRoleNames { get; set; } = [];
    }
}
