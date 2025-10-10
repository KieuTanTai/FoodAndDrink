using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class ProductImage
{
    public uint ProductImageId { get; set; }

    public string ProductBarcode { get; set; } = null!;

    public string ProductImageUrl { get; set; } = null!;

    public sbyte ProductImagePriority { get; set; }

    public DateTime ProductImageCreatedDate { get; set; }

    public DateTime ProductImageLastUpdatedDate { get; set; }

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
