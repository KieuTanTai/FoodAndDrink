namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class DetailInventoryNavigationOptions
{
    // Backing fields
    private bool _isGetProduct;
    private bool _isGetInventory;

    public bool IsGetProduct
    {
        get => _isGetProduct;
        set => _isGetProduct = value;
    }

    public bool IsGetInventory
    {
        get => _isGetInventory;
        set => _isGetInventory = value;
    }
}
