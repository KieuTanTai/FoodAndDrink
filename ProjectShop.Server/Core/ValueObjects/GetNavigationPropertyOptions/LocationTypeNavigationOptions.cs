namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class LocationTypeNavigationOptions
{
    // Backing fields
    private bool _isGetLocations;

    public bool IsGetLocations
    {
        get => _isGetLocations;
        set => _isGetLocations = value;
    }
}
