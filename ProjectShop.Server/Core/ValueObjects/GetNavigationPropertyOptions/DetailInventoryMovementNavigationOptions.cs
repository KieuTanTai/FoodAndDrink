namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class DetailInventoryMovementNavigationOptions
{
    // Backing fields
    private bool _isGetProduct;
    private bool _isGetInventoryMovement;
    private bool _isGetProductLot;

    public bool IsGetProduct
    {
        get => _isGetProduct;
        set => _isGetProduct = value;
    }

    public bool IsGetInventoryMovement
    {
        get => _isGetInventoryMovement;
        set => _isGetInventoryMovement = value;
    }

    public bool IsGetProductLot
    {
        get => _isGetProductLot;
        set => _isGetProductLot = value;
    }
}
