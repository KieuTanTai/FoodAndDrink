using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class DetailInvoice
{
    public uint DetailInvoiceId { get; init; }

    public uint InvoiceId { get; private set; }

    public string ProductBarcode { get; private set; } = null!;

    public uint DetailInvoiceQuantity { get; set; }

    public decimal DetailInvoicePrice { get; set; }

    public bool? DetailInvoiceStatus { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
