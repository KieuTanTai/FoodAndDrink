using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Permission
{
    public uint PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public string PermissionDescription { get; set; } = null!;

    public bool? PermissionStatus { get; set; }

    public DateTime PermissionCreatedDate { get; set; }

    public DateTime PermissionLastUpdatedDate { get; set; }

    public virtual ICollection<AccountAdditionalPermission> AccountAdditionalPermissions { get; set; } = new List<AccountAdditionalPermission>();

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
