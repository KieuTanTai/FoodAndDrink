namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class BankNavigationOptions
{
    // Backing fields
    private bool _isGetUserPaymentMethods;

    public bool IsGetUserPaymentMethods
    {
        get => _isGetUserPaymentMethods;
        set => _isGetUserPaymentMethods = value;
    }
}
