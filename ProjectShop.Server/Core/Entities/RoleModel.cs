// File: Role.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class RoleModel : IGetIdEntity<uint>
    {
        // Corresponds to 'role_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint RoleId { get; private set; }

        // Corresponds to 'role_name' (VARCHAR(30))
        public string RoleName { get; private set; }

        // Corresponds to 'role_status' (TINYINT(1))
        public bool RoleStatus { get; private set; }

        // Corresponds to 'role_create_date' (DATETIME)
        public DateTime RoleCreatedDate { get; private set; } = DateTime.UtcNow;

        // Corresponds to 'role_update_date' (DATETIME)
        public DateTime RoleLastUpdatedDate { get; private set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<RolesOfUserModel> RolesOfUsers { get; private set; } = new List<RolesOfUserModel>();

        public RoleModel(uint roleId, string roleName, bool roleStatus, DateTime roleCreatedDate, DateTime roleLastUpdatedDate)
        {
            RoleId = roleId;
            RoleName = roleName;
            RoleStatus = roleStatus;
            RoleCreatedDate = roleCreatedDate;
            RoleLastUpdatedDate = roleLastUpdatedDate;
        }

        public uint GetIdEntity() => RoleId;
    }
}

