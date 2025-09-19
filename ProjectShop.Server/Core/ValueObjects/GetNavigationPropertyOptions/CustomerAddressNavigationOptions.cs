namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class CustomerAddressNavigationOptions
{
    // Backing fields
    private bool _isGetCity;
    private bool _isGetDistrict;
    private bool _isGetWard;
    private bool _isGetCustomer;

    public bool IsGetCity
    {
        get => _isGetCity;
        set => _isGetCity = value;
    }

    public bool IsGetDistrict
    {
        get => _isGetDistrict;
        set => _isGetDistrict = value;
    }

    public bool IsGetWard
    {
        get => _isGetWard;
        set => _isGetWard = value;
    }

    public bool IsGetCustomer
    {
        get => _isGetCustomer;
        set => _isGetCustomer = value;
    }
}
