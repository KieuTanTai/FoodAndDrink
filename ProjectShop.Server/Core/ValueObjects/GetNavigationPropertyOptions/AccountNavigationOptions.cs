namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class AccountNavigationOptions
{
    // Backing fields
    private bool _isGetCustomer;
    private bool _isGetEmployee;
    private bool _isGetRolesOfUsers;

    public bool IsGetCustomer
    {
        get => _isGetCustomer;
        set => _isGetCustomer = value;
    }

    public bool IsGetEmployee
    {
        get => _isGetEmployee;
        set => _isGetEmployee = value;
    }

    public bool IsGetRolesOfUsers
    {
        get => _isGetRolesOfUsers;
        set => _isGetRolesOfUsers = value;
    }
}
