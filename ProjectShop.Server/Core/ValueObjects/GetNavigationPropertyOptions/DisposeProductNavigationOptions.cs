namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class DisposeProductNavigationOptions
{
    // Backing fields
    private bool _isGetProduct;
    private bool _isGetLocation;
    private bool _isGetDisposeByEmployee;
    private bool _isGetDisposeReason;

    public bool IsGetProduct
    {
        get => _isGetProduct;
        set => _isGetProduct = value;
    }

    public bool IsGetLocation
    {
        get => _isGetLocation;
        set => _isGetLocation = value;
    }

    public bool IsGetDisposeByEmployee
    {
        get => _isGetDisposeByEmployee;
        set => _isGetDisposeByEmployee = value;
    }

    public bool IsGetDisposeReason
    {
        get => _isGetDisposeReason;
        set => _isGetDisposeReason = value;
    }
}
