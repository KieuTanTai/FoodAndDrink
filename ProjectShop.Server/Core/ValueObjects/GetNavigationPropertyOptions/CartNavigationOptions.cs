namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class CartNavigationOptions
{
    // Backing fields
    private bool _isGetCustomer;
    private bool _isGetDetailCarts;

    public bool IsGetCustomer
    {
        get => _isGetCustomer;
        set => _isGetCustomer = value;
    }

    public bool IsGetDetailCarts
    {
        get => _isGetDetailCarts;
        set => _isGetDetailCarts = value;
    }
}
