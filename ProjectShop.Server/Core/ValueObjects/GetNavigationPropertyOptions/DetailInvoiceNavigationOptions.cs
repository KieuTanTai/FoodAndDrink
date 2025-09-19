namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class DetailInvoiceNavigationOptions
{
    // Backing fields
    private bool _isGetInvoice;
    private bool _isGetProduct;

    public bool IsGetInvoice
    {
        get => _isGetInvoice;
        set => _isGetInvoice = value;
    }

    public bool IsGetProduct
    {
        get => _isGetProduct;
        set => _isGetProduct = value;
    }
}
