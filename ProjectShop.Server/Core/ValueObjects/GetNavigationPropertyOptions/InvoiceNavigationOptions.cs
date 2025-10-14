namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class InvoiceNavigationOptions
{
    public bool IsGetCustomer { get; set; }
    public bool IsGetEmployee { get; set; }
    public bool IsGetSaleEvent { get; set; }
    public bool IsGetUserPaymentMethod { get; set; }
    public bool IsGetDetailInvoices { get; set; }
}
