using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class AccountRole
{
    public uint AccountRoleId { get; init; }

    public uint AccountId { get; private set; }

    public uint RoleId { get; private set; }

    public DateTime AccountRoleAssignedDate { get; init; }

    public bool? AccountRoleStatus { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
