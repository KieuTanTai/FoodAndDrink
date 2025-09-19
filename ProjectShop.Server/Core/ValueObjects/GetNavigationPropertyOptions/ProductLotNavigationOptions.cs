namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class ProductLotNavigationOptions
{
    // Backing fields
    private bool _isGetInventories;
    private bool _isGetDetailInventoryMovements;

    public bool IsGetInventories
    {
        get => _isGetInventories;
        set => _isGetInventories = value;
    }

    public bool IsGetDetailInventoryMovements
    {
        get => _isGetDetailInventoryMovements;
        set => _isGetDetailInventoryMovements = value;
    }
}
