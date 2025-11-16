using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Account
{
    public uint AccountId { get; init; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime AccountCreatedDate { get; init; }

    public DateTime AccountLastUpdatedDate { get; set; }

    public bool AccountStatus { get; set; }

    public virtual ICollection<AccountAdditionalPermission> AccountAdditionalPermissions { get; set; } = [];

    public virtual ICollection<AccountRole> AccountRoles { get; set; } = [];

    public virtual Person? Person { get; set; }
}
