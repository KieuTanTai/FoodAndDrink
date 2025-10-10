using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class DetailInvoice
{
    public uint DetailInvoiceId { get; set; }

    public uint InvoiceId { get; set; }

    public string ProductBarcode { get; set; } = null!;

    public uint DetailInvoiceQuantity { get; set; }

    public decimal DetailInvoicePrice { get; set; }

    public bool? DetailInvoiceStatus { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
