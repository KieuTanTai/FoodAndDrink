namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class DetailCartNavigationOptions
{
    // Backing fields
    private bool _isGetCart;
    private bool _isGetProduct;

    public bool IsGetCart
    {
        get => _isGetCart;
        set => _isGetCart = value;
    }

    public bool IsGetProduct
    {
        get => _isGetProduct;
        set => _isGetProduct = value;
    }
}
