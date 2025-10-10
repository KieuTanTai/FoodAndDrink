using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Product
{
    public string ProductBarcode { get; set; } = null!;

    public uint SupplierId { get; set; }

    public uint CountryId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal ProductNetWeight { get; set; }

    public string ProductUnit { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public string ProductBrand { get; set; } = null!;

    public decimal ProductBasePrice { get; set; }

    public string ProductRatingAge { get; set; } = null!;

    public bool? ProductStatus { get; set; }

    public DateTime ProductAddedDate { get; set; }

    public DateTime ProductLastUpdatedDate { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<DetailCart> DetailCarts { get; set; } = new List<DetailCart>();

    public virtual ICollection<DetailInventory> DetailInventories { get; set; } = new List<DetailInventory>();

    public virtual ICollection<DetailInventoryMovement> DetailInventoryMovements { get; set; } = new List<DetailInventoryMovement>();

    public virtual ICollection<DetailInvoice> DetailInvoices { get; set; } = new List<DetailInvoice>();

    public virtual ICollection<DetailSaleEvent> DetailSaleEvents { get; set; } = new List<DetailSaleEvent>();

    public virtual ICollection<DisposeProduct> DisposeProducts { get; set; } = new List<DisposeProduct>();

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    public virtual ProductDrink? ProductDrink { get; set; }

    public virtual ProductFruit? ProductFruit { get; set; }

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ProductMeat? ProductMeat { get; set; }

    public virtual ProductSnack? ProductSnack { get; set; }

    public virtual ProductVegetable? ProductVegetable { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;
}
