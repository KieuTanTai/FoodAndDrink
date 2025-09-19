namespace ProjectShop.Server.Core.ValueObjects
{
    public class RoleUpdateNamesRequest
    {
        // Backing fields
        private IEnumerable<uint> _roleIds = [];
        private IEnumerable<string> _newRoleNames = [];

        public IEnumerable<uint> RoleIds
        {
            get => _roleIds;
            set => _roleIds = value ?? [];
        }

        public IEnumerable<string> NewRoleNames
        {
            get => _newRoleNames;
            set => _newRoleNames = value ?? [];
        }
    }
}
