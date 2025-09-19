namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class InventoryNavigationOptions
{
    // Backing fields
    private bool _isGetLocation;

    public bool IsGetLocation
    {
        get => _isGetLocation;
        set => _isGetLocation = value;
    }
}
