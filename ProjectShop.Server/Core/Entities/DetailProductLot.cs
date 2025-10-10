using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class DetailProductLot
{
    public uint ProductLotId { get; set; }

    public string ProductBarcode { get; set; } = null!;

    public DateTime ProductLotMfgDate { get; set; }

    public DateTime ProductLotExpDate { get; set; }

    public int ProductLotInitialQuantity { get; set; }

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;

    public virtual ProductLot ProductLot { get; set; } = null!;
}
