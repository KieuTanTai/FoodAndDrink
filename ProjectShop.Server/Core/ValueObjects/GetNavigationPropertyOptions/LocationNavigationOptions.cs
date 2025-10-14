namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class LocationNavigationOptions
{
    public bool IsGetLocationCity { get; set; }
    public bool IsGetLocationDistrict { get; set; }
    public bool IsGetLocationType { get; set; }
    public bool IsGetLocationWard { get; set; }
    public bool IsGetDisposeProducts { get; set; }
    public bool IsGetEmployees { get; set; }
    public bool IsGetInventories { get; set; }
    public bool IsGetInventoryMovementDestinationLocations { get; set; }
    public bool IsGetInventoryMovementSourceLocations { get; set; }
    public bool IsGetSupplierCompanyLocations { get; set; }
    public bool IsGetSupplierStoreLocations { get; set; }
}
