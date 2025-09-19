namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class InventoryMovementNavigationOptions
{
    // Backing fields
    private bool _isGetSourceLocation;
    private bool _isGetDestinationLocation;

    public bool IsGetSourceLocation
    {
        get => _isGetSourceLocation;
        set => _isGetSourceLocation = value;
    }

    public bool IsGetDestinationLocation
    {
        get => _isGetDestinationLocation;
        set => _isGetDestinationLocation = value;
    }
}
