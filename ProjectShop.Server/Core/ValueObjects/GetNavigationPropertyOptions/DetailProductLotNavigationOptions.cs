namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class DetailProductLotNavigationOptions
{
    // Backing fields
    private bool _isGetProduct;
    private bool _isGetProductLot;

    public bool IsGetProduct
    {
        get => _isGetProduct;
        set => _isGetProduct = value;
    }

    public bool IsGetProductLot
    {
        get => _isGetProductLot;
        set => _isGetProductLot = value;
    }
}
