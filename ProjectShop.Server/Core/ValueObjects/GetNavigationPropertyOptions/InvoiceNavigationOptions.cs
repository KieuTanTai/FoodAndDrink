namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class InvoiceNavigationOptions
{
    // Backing fields
    private bool _isGetCustomer;
    private bool _isGetEmployee;
    private bool _isGetUserPaymentMethod;
    private bool _isGetDetailInvoices;
    private bool _isGetInvoiceDiscounts;

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

    public bool IsGetUserPaymentMethod
    {
        get => _isGetUserPaymentMethod;
        set => _isGetUserPaymentMethod = value;
    }

    public bool IsGetDetailInvoices
    {
        get => _isGetDetailInvoices;
        set => _isGetDetailInvoices = value;
    }

    public bool IsGetInvoiceDiscounts
    {
        get => _isGetInvoiceDiscounts;
        set => _isGetInvoiceDiscounts = value;
    }
}
