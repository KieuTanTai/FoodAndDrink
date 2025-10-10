using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class DisposeProduct
{
    public uint DisposeProductId { get; set; }

    public uint LocationId { get; set; }

    public string ProductBarcode { get; set; } = null!;

    public uint DisposeByEmployeeId { get; set; }

    public uint DisposeReasonId { get; set; }

    public uint DisposeQuantity { get; set; }

    public DateTime DisposedDate { get; set; }

    public virtual Employee DisposeByEmployee { get; set; } = null!;

    public virtual DisposeReason DisposeReason { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
