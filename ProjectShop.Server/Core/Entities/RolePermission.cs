using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class RolePermission
{
    public uint RolePermissionId { get; init; }

    public uint RoleId { get; private set; }

    public uint PermissionId { get; private set; }

    public DateTime RolePermissionCreatedDate { get; init; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
