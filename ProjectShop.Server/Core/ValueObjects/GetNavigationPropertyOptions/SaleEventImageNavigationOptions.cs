namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class SaleEventImageNavigationOptions
{
    // Backing fields
    private bool _isGetSaleEvent;

    public bool IsGetSaleEvent
    {
        get => _isGetSaleEvent;
        set => _isGetSaleEvent = value;
    }
}
