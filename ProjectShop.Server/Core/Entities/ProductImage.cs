using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class ProductImage
{
    public uint ProductImageId { get; init; }

    public string ProductBarcode { get; private set; } = null!;

    public string ProductImageUrl { get; set; } = null!;

    public sbyte ProductImagePriority { get; set; }

    public DateTime ProductImageCreatedDate { get; init; }

    public DateTime ProductImageLastUpdatedDate { get; set; }

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
