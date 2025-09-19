namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class UserPaymentMethodNavigationOptions
{
    // Backing fields
    private bool _isGetBank;
    private bool _isGetCustomer;
    private bool _isGetInvoices;

    public bool IsGetBank
    {
        get => _isGetBank;
        set => _isGetBank = value;
    }

    public bool IsGetCustomer
    {
        get => _isGetCustomer;
        set => _isGetCustomer = value;
    }

    public bool IsGetInvoices
    {
        get => _isGetInvoices;
        set => _isGetInvoices = value;
    }
}
