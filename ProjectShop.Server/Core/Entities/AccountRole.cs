using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class AccountRole
{
    public uint AccountRoleId { get; set; }

    public uint AccountId { get; set; }

    public uint RoleId { get; set; }

    public DateTime AccountRoleAssignedDate { get; set; }

    public bool? AccountRoleStatus { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
