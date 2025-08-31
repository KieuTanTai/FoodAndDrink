namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class ProductNavigationOptions
{
    public bool IsGetSupplier { get; set; }
    public bool IsGetDetailCarts { get; set; }
    public bool IsGetDetailProductLots { get; set; }
    public bool IsGetProductCategories { get; set; }
    public bool IsGetProductImages { get; set; }
    public bool IsGetDetailSaleEvents { get; set; }
    public bool IsGetDetailInvoices { get; set; }

    // NEW 
    public bool IsGetDetailInventoryMovements { get; set; }
    public bool IsGetDetailInventories { get; set; }
    public bool IsGetDisposeProducts { get; set; }
}
