namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class SaleEventNavigationOptions
{
    // Backing fields
    private bool _isGetDetailSaleEvents;
    private bool _isGetInvoiceDiscounts;

    public bool IsGetDetailSaleEvents
    {
        get => _isGetDetailSaleEvents;
        set => _isGetDetailSaleEvents = value;
    }

    public bool IsGetInvoiceDiscounts
    {
        get => _isGetInvoiceDiscounts;
        set => _isGetInvoiceDiscounts = value;
    }
}
