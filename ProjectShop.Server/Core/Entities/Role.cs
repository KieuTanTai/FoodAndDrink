using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Role
{
    public uint RoleId { get; init; }

    public string RoleName { get; set; } = null!;

    public bool? RoleStatus { get; set; }

    public DateTime RoleCreatedDate { get; init; }

    public DateTime RoleLastUpdatedDate { get; set; }

    public virtual ICollection<AccountRole> AccountRoles { get; init; } = [];

    public virtual ICollection<RolePermission> RolePermissions { get; init; } = [];
}
