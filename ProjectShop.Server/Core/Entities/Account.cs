using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Account
{
    public uint AccountId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime AccountCreatedDate { get; set; }

    public DateTime AccountLastUpdatedDate { get; set; }

    public bool? AccountStatus { get; set; }

    public virtual ICollection<AccountAdditionalPermission> AccountAdditionalPermissions { get; set; } = new List<AccountAdditionalPermission>();

    public virtual ICollection<AccountRole> AccountRoles { get; set; } = new List<AccountRole>();

    public virtual Person? Person { get; set; }
}
