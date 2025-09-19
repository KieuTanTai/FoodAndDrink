namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class DisposeReasonNavigationOptions
{
    // Backing fields
    private bool _isGetDisposeProducts;

    public bool IsGetDisposeProducts
    {
        get => _isGetDisposeProducts;
        set => _isGetDisposeProducts = value;
    }
}
