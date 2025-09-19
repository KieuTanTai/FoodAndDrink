namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class RoleNavigationOptions
{
    // Backing fields
    private bool _isGetRolesOfUsers;

    public bool IsGetRolesOfUsers
    {
        get => _isGetRolesOfUsers;
        set => _isGetRolesOfUsers = value;
    }
}
