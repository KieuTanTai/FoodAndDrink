using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Role
{
    public uint RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public bool? RoleStatus { get; set; }

    public DateTime RoleCreatedDate { get; set; }

    public DateTime RoleLastUpdatedDate { get; set; }

    public virtual ICollection<AccountRole> AccountRoles { get; set; } = new List<AccountRole>();

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
