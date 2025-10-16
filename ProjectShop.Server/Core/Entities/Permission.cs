using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Permission
{
    public uint PermissionId { get; init; }

    public string PermissionName { get; set; } = null!;

    public string PermissionDescription { get; set; } = null!;

    public bool? PermissionStatus { get; set; }

    public DateTime PermissionCreatedDate { get; init; }

    public DateTime PermissionLastUpdatedDate { get; set; }

    public virtual ICollection<AccountAdditionalPermission> AccountAdditionalPermissions { get; set; } = [];

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = [];
}
