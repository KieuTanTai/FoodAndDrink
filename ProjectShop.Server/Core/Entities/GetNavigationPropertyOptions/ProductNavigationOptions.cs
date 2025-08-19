namespace ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;

public class ProductNavigationOptions
{
    public bool IsGetSupplier { get; set; }
    public bool IsGetDetailCarts { get; set; }
    public bool IsGetProductLots { get; set; }
    public bool IsGetProductCategories { get; set; }
    public bool IsGetProductImages { get; set; }
    public bool IsGetDetailSaleEvents { get; set; }
    public bool IsGetDetailInvoices { get; set; }
}
