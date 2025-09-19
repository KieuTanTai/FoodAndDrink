namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class InvoiceDiscountNavigationOptions
{
    // Backing fields
    private bool _isGetSaleEvent;
    private bool _isGetInvoice;

    public bool IsGetSaleEvent
    {
        get => _isGetSaleEvent;
        set => _isGetSaleEvent = value;
    }

    public bool IsGetInvoice
    {
        get => _isGetInvoice;
        set => _isGetInvoice = value;
    }
}
