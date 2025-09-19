namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class PersonNavigationOptions
{
    // Backing fields
    private bool _isGetAccount;
    private bool _isGetWard;
    private bool _isGetDistrict;
    private bool _isGetCity;

    public bool IsGetAccount
    {
        get => _isGetAccount;
        set => _isGetAccount = value;
    }

    public bool IsGetWard
    {
        get => _isGetWard;
        set => _isGetWard = value;
    }

    public bool IsGetDistrict
    {
        get => _isGetDistrict;
        set => _isGetDistrict = value;
    }

    public bool IsGetCity
    {
        get => _isGetCity;
        set => _isGetCity = value;
    }
}
