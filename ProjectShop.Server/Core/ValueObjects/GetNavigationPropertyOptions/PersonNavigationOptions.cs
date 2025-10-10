namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class PersonNavigationOptions
{
    // Backing fields
    private bool _isGetAccount;

    public bool IsGetAccount
    {
        get => _isGetAccount;
        set => _isGetAccount = value;
    }
}
