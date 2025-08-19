// File: RolesOfUser.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public struct RolesOfUserKey
    {
        public uint AccountId { get; }
        public uint RoleId { get; }

        public RolesOfUserKey(uint accountId, uint roleId)
        {
            AccountId = accountId;
            RoleId = roleId;
        }
    }

    public class RolesOfUserModel : IGetIdEntity<uint>
    {
        // Corresponds to 'id' (INT UNSIGNED AUTO_INCREMENT)
        public uint Id { get; set; }

        // Corresponds to 'account_id' (INT UNSIGNED)
        public uint AccountId { get; set; }

        // Corresponds to 'role_id' (INT UNSIGNED)
        public uint RoleId { get; set; }

        // Corresponds to 'added_date' (DATETIME)
        public DateTime AddedDate { get; set; }

        // Navigation properties
        public AccountModel Account { get; set; } = null!;
        public RoleModel Role { get; set; } = null!;
        // End of navigation properties

        public RolesOfUserModel(uint id, uint accountId, uint roleId, DateTime addedDate)
        {
            Id = id;
            AccountId = accountId;
            RoleId = roleId;
            AddedDate = addedDate;
        }

        public RolesOfUserModel()
        {
        }

        public uint GetIdEntity() => Id;
    }
}

