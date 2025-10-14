using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Supplier
{
    public uint SupplierId { get; init; }

    public string SupplierName { get; set; } = null!;

    public string SupplierPhone { get; set; } = null!;

    public string SupplierEmail { get; set; } = null!;

    public uint CompanyLocationId { get; private set; }

    public uint StoreLocationId { get; private set; }

    public bool? SupplierStatus { get; set; }

    public DateTime SupplierCooperationDate { get; init; }

    public virtual Location CompanyLocation { get; set; } = null!;

    public virtual ICollection<Product> Products { get; init; } = [];

    public virtual Location StoreLocation { get; set; } = null!;
}
