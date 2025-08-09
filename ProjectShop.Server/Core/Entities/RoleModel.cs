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

        // Navigation property
        public ICollection<RolesOfUserModel> RolesOfUsers { get; private set; } = new List<RolesOfUserModel>();

        public RoleModel(uint roleId, string roleName, bool roleStatus)
        {
            RoleId = roleId;
            RoleName = roleName;
            RoleStatus = roleStatus;
        }

        public uint GetIdEntity() => RoleId;
    }
}

