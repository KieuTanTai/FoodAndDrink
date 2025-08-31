namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class InvoiceNavigationOptions
{
    public bool IsGetCustomer { get; set; }
    public bool IsGetEmployee { get; set; }
    public bool IsGetUserPaymentMethod { get; set; }
    public bool IsGetDetailInvoices { get; set; }
    public bool IsGetInvoiceDiscounts { get; set; }
}
