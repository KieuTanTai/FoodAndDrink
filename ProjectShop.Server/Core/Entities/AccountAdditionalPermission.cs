using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class AccountAdditionalPermission
{
    public uint AccountAdditionalPermissionId { get; set; }

    public uint AccountId { get; set; }

    public uint PermissionId { get; set; }

    /// <summary>
    /// 1=granted, 0=denied
    /// </summary>
    public bool? IsGranted { get; set; }

    public DateTime AdditionalPermissionAssignedDate { get; set; }

    public bool? AdditionalPermissionStatus { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Permission Permission { get; set; } = null!;
}
