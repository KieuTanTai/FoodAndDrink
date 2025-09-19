namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class LocationCityNavigationOptions
{
    // Backing fields
    private bool _isGetLocations;
    private bool _isGetCompanySuppliers;
    private bool _isGetStoreSuppliers;
    private bool _isGetCustomers;
    private bool _isGetEmployees;

    public bool IsGetLocations
    {
        get => _isGetLocations;
        set => _isGetLocations = value;
    }

    public bool IsGetCompanySuppliers
    {
        get => _isGetCompanySuppliers;
        set => _isGetCompanySuppliers = value;
    }

    public bool IsGetStoreSuppliers
    {
        get => _isGetStoreSuppliers;
        set => _isGetStoreSuppliers = value;
    }

    public bool IsGetCustomers
    {
        get => _isGetCustomers;
        set => _isGetCustomers = value;
    }

    public bool IsGetEmployees
    {
        get => _isGetEmployees;
        set => _isGetEmployees = value;
    }
}
