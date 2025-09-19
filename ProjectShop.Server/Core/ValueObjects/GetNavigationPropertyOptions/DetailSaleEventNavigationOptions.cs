namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class DetailSaleEventNavigationOptions
{
    // Backing fields
    private bool _isGetSaleEvent;
    private bool _isGetProduct;

    public bool IsGetSaleEvent
    {
        get => _isGetSaleEvent;
        set => _isGetSaleEvent = value;
    }

    public bool IsGetProduct
    {
        get => _isGetProduct;
        set => _isGetProduct = value;
    }
}
