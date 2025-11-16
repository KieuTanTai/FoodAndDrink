namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class DetailInventoryMovementNavigationOptions
{
    public bool IsGetInventoryMovement { get; set; }
    public bool IsGetProductBarcodeNavigation { get; set; }
    public bool IsGetProductLot { get; set; }
}
