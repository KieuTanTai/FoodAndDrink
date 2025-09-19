namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class CustomerNavigationOptions
{
    // Backing fields
    private bool _isGetCart;
    private bool _isGetInvoices;
    private bool _isGetCustomerAddresses;

    public bool IsGetCart
    {
        get => _isGetCart;
        set => _isGetCart = value;
    }

    public bool IsGetInvoices
    {
        get => _isGetInvoices;
        set => _isGetInvoices = value;
    }

    public bool IsGetCustomerAddresses
    {
        get => _isGetCustomerAddresses;
        set => _isGetCustomerAddresses = value;
    }
}
