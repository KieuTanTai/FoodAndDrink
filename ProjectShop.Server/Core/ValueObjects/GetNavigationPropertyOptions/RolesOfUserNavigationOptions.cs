namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class RolesOfUserNavigationOptions
{
    // Backing fields
    private bool _isGetAccount;
    private bool _isGetRole;

    public bool IsGetAccount
    {
        get => _isGetAccount;
        set => _isGetAccount = value;
    }

    public bool IsGetRole
    {
        get => _isGetRole;
        set => _isGetRole = value;
    }
}
