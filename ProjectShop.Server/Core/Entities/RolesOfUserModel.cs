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
        public uint Id { get; private set; }

        // Corresponds to 'account_id' (INT UNSIGNED)
        public uint AccountId { get; private set; }

        // Corresponds to 'role_id' (INT UNSIGNED)
        public uint RoleId { get; private set; }

        // Corresponds to 'added_date' (DATETIME)
        public DateTime AddedDate { get; private set; }

        // Navigation properties
        public AccountModel Account { get; private set; } = null!;
        public RoleModel Role { get; private set; } = null!;

        public RolesOfUserModel(uint id, uint accountId, uint roleId, DateTime addedDate)
        {
            Id = id;
            AccountId = accountId;
            RoleId = roleId;
            AddedDate = addedDate;
        }

        public uint GetIdEntity() => Id;
    }
}

