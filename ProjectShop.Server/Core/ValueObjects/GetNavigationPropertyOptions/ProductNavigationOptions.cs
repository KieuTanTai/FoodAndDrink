namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class ProductNavigationOptions
{
    // Backing fields
    private bool _isGetSupplier;
    private bool _isGetDetailCarts;
    private bool _isGetDetailProductLots;
    private bool _isGetProductCategories;
    private bool _isGetProductImages;
    private bool _isGetDetailSaleEvents;
    private bool _isGetDetailInvoices;
    private bool _isGetDetailInventoryMovements;
    private bool _isGetDetailInventories;
    private bool _isGetDisposeProducts;

    public bool IsGetSupplier
    {
        get => _isGetSupplier;
        set => _isGetSupplier = value;
    }

    public bool IsGetDetailCarts
    {
        get => _isGetDetailCarts;
        set => _isGetDetailCarts = value;
    }

    public bool IsGetDetailProductLots
    {
        get => _isGetDetailProductLots;
        set => _isGetDetailProductLots = value;
    }

    public bool IsGetProductCategories
    {
        get => _isGetProductCategories;
        set => _isGetProductCategories = value;
    }

    public bool IsGetProductImages
    {
        get => _isGetProductImages;
        set => _isGetProductImages = value;
    }

    public bool IsGetDetailSaleEvents
    {
        get => _isGetDetailSaleEvents;
        set => _isGetDetailSaleEvents = value;
    }

    public bool IsGetDetailInvoices
    {
        get => _isGetDetailInvoices;
        set => _isGetDetailInvoices = value;
    }

    // NEW 
    public bool IsGetDetailInventoryMovements
    {
        get => _isGetDetailInventoryMovements;
        set => _isGetDetailInventoryMovements = value;
    }

    public bool IsGetDetailInventories
    {
        get => _isGetDetailInventories;
        set => _isGetDetailInventories = value;
    }

    public bool IsGetDisposeProducts
    {
        get => _isGetDisposeProducts;
        set => _isGetDisposeProducts = value;
    }
}
