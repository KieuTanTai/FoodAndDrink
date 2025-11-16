using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Product
{
    public string ProductBarcode { get; init; } = null!;

    public uint SupplierId { get; private set; }

    public uint CountryId { get; private set; }

    public string ProductName { get; set; } = null!;

    public decimal ProductNetWeight { get; set; }

    public string ProductUnit { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public string ProductBrand { get; set; } = null!;

    public decimal ProductBasePrice { get; set; }

    public string ProductRatingAge { get; set; } = null!;

    public bool? ProductStatus { get; set; }

    public DateTime ProductAddedDate { get; init; }

    public DateTime ProductLastUpdatedDate { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<DetailCart> DetailCarts { get; set; } = [];

    public virtual ICollection<DetailInventory> DetailInventories { get; set; } = [];

    public virtual ICollection<DetailInventoryMovement> DetailInventoryMovements { get; set; } = [];

    public virtual ICollection<DetailInvoice> DetailInvoices { get; set; } = [];

    public virtual ICollection<DetailSaleEvent> DetailSaleEvents { get; set; } = [];

    public virtual ICollection<DisposeProduct> DisposeProducts { get; set; } = [];

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = [];

    public virtual ProductDrink? ProductDrink { get; set; }

    public virtual ProductFruit? ProductFruit { get; set; }

    public virtual ICollection<ProductImage> ProductImages { get; set; } = [];

    public virtual ProductMeat? ProductMeat { get; set; }

    public virtual ProductSnack? ProductSnack { get; set; }

    public virtual ProductVegetable? ProductVegetable { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;
}
