namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class DisposeProductNavigationOptions
{
    public bool IsGetProduct { get; set; }
    public bool IsGetLocation { get; set; }
    public bool IsGetDisposeByEmployee { get; set; }
    public bool IsGetDisposeReason { get; set; }
}
