namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class EmployeeNavigationOptions
{
    // Backing fields
    private bool _isGetEmployeeWard;
    private bool _isGetEmployeeDistrict;
    private bool _isGetEmployeeCity;
    private bool _isGetLocation;
    private bool _isGetPerson;

    public bool IsGetPerson
    {
        get => _isGetPerson;
        set => _isGetPerson = value;
    }
    public bool IsGetEmployeeWard
    {
        get => _isGetEmployeeWard;
        set => _isGetEmployeeWard = value;
    }

    public bool IsGetEmployeeDistrict
    {
        get => _isGetEmployeeDistrict;
        set => _isGetEmployeeDistrict = value;
    }

    public bool IsGetEmployeeCity
    {
        get => _isGetEmployeeCity;
        set => _isGetEmployeeCity = value;
    }

    public bool IsGetLocation
    {
        get => _isGetLocation;
        set => _isGetLocation = value;
    }
}
