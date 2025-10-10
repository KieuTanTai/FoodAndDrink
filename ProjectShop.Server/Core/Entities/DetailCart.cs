using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class DetailCart
{
    public uint DetailCartId { get; set; }

    public uint CartId { get; set; }

    public string ProductBarcode { get; set; } = null!;

    public DateTime DetailCartAddedDate { get; set; }

    public decimal DetailCartPrice { get; set; }

    public uint DetailCartQuantity { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
