using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class DisposeReason
{
    public uint DisposeReasonId { get; init; }

    public string DisposeReasonName { get; set; } = null!;

    public virtual ICollection<DisposeProduct> DisposeProducts { get; set; } = [];
}
