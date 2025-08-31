namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class LocationNavigationOptions
{
    public bool IsGetLocationDistrict { get; set; }
    public bool IsGetLocationWard { get; set; }
    public bool IsGetLocationType { get; set; }
    public bool IsGetLocationCity { get; set; }
    public bool IsGetInventories { get; set; }
    public bool IsGetSourceInventoryMovements { get; set; }
    public bool IsGetDestinationInventoryMovements { get; set; }
    public bool IsGetDisposeProducts { get; set; }
}
