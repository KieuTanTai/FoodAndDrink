namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class ProductImageNavigationOptions
{
    // Backing fields
    private bool _isGetProduct;

    public bool IsGetProduct
    {
        get => _isGetProduct;
        set => _isGetProduct = value;
    }
}
