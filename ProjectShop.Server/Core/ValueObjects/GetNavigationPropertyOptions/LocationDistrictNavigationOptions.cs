namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class LocationDistrictNavigationOptions
{
    public bool IsGetLocations { get; set; }
    public bool IsGetCompanySuppliers { get; set; }
    public bool IsGetStoreSuppliers { get; set; }
    public bool IsGetCustomers { get; set; }
    public bool IsGetEmployees { get; set; }
}
