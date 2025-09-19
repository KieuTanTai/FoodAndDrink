namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class SupplierNavigationOptions
{
    // Backing fields
    private bool _isGetStoreLocation;
    private bool _isGetCompanyLocation;
    private bool _isGetProducts;

    public bool IsGetStoreLocation
    {
        get => _isGetStoreLocation;
        set => _isGetStoreLocation = value;
    }

    public bool IsGetCompanyLocation
    {
        get => _isGetCompanyLocation;
        set => _isGetCompanyLocation = value;
    }

    public bool IsGetProducts
    {
        get => _isGetProducts;
        set => _isGetProducts = value;
    }
}
