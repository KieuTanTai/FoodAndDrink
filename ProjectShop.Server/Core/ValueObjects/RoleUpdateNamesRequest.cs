namespace ProjectShop.Server.Core.ValueObjects
{
    public class RoleUpdateNamesRequest
    {
        public IEnumerable<uint> RoleIds { get; set; } = [];
        public IEnumerable<string> NewRoleNames { get; set; } = [];
    }
}
