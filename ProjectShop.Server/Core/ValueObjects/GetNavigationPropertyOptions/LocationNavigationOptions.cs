namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class LocationNavigationOptions
{
    // Backing fields
    private bool _isGetLocationDistrict;
    private bool _isGetLocationWard;
    private bool _isGetLocationType;
    private bool _isGetLocationCity;
    private bool _isGetInventory;
    private bool _isGetSourceInventoryMovements;
    private bool _isGetDestinationInventoryMovements;
    private bool _isGetDisposeProducts;

    public bool IsGetLocationDistrict
    {
        get => _isGetLocationDistrict;
        set => _isGetLocationDistrict = value;
    }

    public bool IsGetLocationWard
    {
        get => _isGetLocationWard;
        set => _isGetLocationWard = value;
    }

    public bool IsGetLocationType
    {
        get => _isGetLocationType;
        set => _isGetLocationType = value;
    }

    public bool IsGetLocationCity
    {
        get => _isGetLocationCity;
        set => _isGetLocationCity = value;
    }

    public bool IsGetInventory
    {
        get => _isGetInventory;
        set => _isGetInventory = value;
    }

    public bool IsGetSourceInventoryMovements
    {
        get => _isGetSourceInventoryMovements;
        set => _isGetSourceInventoryMovements = value;
    }

    public bool IsGetDestinationInventoryMovements
    {
        get => _isGetDestinationInventoryMovements;
        set => _isGetDestinationInventoryMovements = value;
    }

    public bool IsGetDisposeProducts
    {
        get => _isGetDisposeProducts;
        set => _isGetDisposeProducts = value;
    }
}
