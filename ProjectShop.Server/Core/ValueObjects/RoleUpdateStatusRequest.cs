namespace ProjectShop.Server.Core.ValueObjects
{
    public class RoleUpdateStatusRequest
    {
        // Backing fields
        private IEnumerable<uint> _roleIds = [];
        private bool _status;

        public IEnumerable<uint> RoleIds
        {
            get => _roleIds;
            set => _roleIds = value ?? [];
        }

        public bool Status
        {
            get => _status;
            set => _status = value;
        }
    }
}
