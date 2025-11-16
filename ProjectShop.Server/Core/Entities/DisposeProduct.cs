using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class DisposeProduct
{
    public uint DisposeProductId { get; init; }

    public uint LocationId { get; private set; }

    public string ProductBarcode { get; private set; } = null!;

    public uint DisposeByEmployeeId { get; private set; }

    public uint DisposeReasonId { get; private set; }

    public uint DisposeQuantity { get; set; }

    public DateTime DisposedDate { get; init; }

    public virtual Employee DisposeByEmployee { get; set; } = null!;

    public virtual DisposeReason DisposeReason { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
