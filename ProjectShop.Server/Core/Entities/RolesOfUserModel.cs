// File: RolesOfUser.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class RolesOfUserModel : IGetIdEntity<uint>
    {
        // Corresponds to 'id' (INT UNSIGNED AUTO_INCREMENT)
        public uint Id { get; private set; }

        // Corresponds to 'account_id' (INT UNSIGNED)
        public uint AccountId { get; private set; }

        // Corresponds to 'role_id' (INT UNSIGNED)
        public uint RoleId { get; private set; }

        // Corresponds to 'create_date' (DATETIME)
        public DateTime CreateDate { get; private set; }

        // Navigation properties
        public AccountModel Account { get; private set; } = null!;
        public RoleModel Role { get; private set; } = null!;

        public RolesOfUserModel(uint id, uint accountId, uint roleId, DateTime createDate)
        {
            Id = id;
            AccountId = accountId;
            RoleId = roleId;
            CreateDate = createDate;
        }

        public uint GetIdEntity() => Id;
    }
}

