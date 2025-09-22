// File: Role.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class RoleModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _roleId;
        private string _roleName = string.Empty;
        private bool _roleStatus;
        private DateTime _roleCreatedDate = DateTime.UtcNow;
        private DateTime _roleLastUpdatedDate = DateTime.UtcNow;

        // Corresponds to 'role_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint RoleId
        {
            get => _roleId;
            set => _roleId = value;
        }

        // Corresponds to 'role_name' (VARCHAR(30))
        public string RoleName
        {
            get => _roleName;
            set => _roleName = value ?? string.Empty;
        }

        // Corresponds to 'role_status' (TINYINT(1))
        public bool RoleStatus
        {
            get => _roleStatus;
            set => _roleStatus = value;
        }

        // Corresponds to 'role_create_date' (DATETIME)
        public DateTime RoleCreatedDate
        {
            get => _roleCreatedDate;
            set => _roleCreatedDate = value;
        }

        // Corresponds to 'role_update_date' (DATETIME)
        public DateTime RoleLastUpdatedDate
        {
            get => _roleLastUpdatedDate;
            set => _roleLastUpdatedDate = value;
        }

        // Navigation properties
        public ICollection<RolesOfUserModel> RolesOfUsers { get; set; } = [];
        // End of navigation properties

        public RoleModel(uint roleId, string roleName, bool roleStatus, DateTime roleCreatedDate, DateTime roleLastUpdatedDate)
        {
            RoleId = roleId;
            RoleName = roleName;
            RoleStatus = roleStatus;
            RoleCreatedDate = roleCreatedDate;
            RoleLastUpdatedDate = roleLastUpdatedDate;
        }

        public RoleModel() { }

        public uint GetIdEntity() => RoleId;
    }
}

