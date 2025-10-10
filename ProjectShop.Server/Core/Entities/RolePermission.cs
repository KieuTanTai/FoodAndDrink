using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class RolePermission
{
    public uint RolePermissionId { get; set; }

    public uint RoleId { get; set; }

    public uint PermissionId { get; set; }

    public DateTime RolePermissionCreatedDate { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
