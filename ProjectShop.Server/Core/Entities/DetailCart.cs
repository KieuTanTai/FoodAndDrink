using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class DetailCart
{
    public uint DetailCartId { get; init; }

    public uint CartId { get; private set; }

    public string ProductBarcode { get; private set; } = null!;

    public DateTime DetailCartAddedDate { get; init; }

    public decimal DetailCartPrice { get; set; }

    public uint DetailCartQuantity { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
