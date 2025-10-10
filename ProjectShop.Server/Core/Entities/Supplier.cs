using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Supplier
{
    public uint SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string SupplierPhone { get; set; } = null!;

    public string SupplierEmail { get; set; } = null!;

    public uint CompanyLocationId { get; set; }

    public uint StoreLocationId { get; set; }

    public bool? SupplierStatus { get; set; }

    public DateTime SupplierCooperationDate { get; set; }

    public virtual Location CompanyLocation { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Location StoreLocation { get; set; } = null!;
}
