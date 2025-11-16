namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class CustomerNavigationOptions
{
    public bool IsGetPerson { get; set; }
    public bool IsGetCarts { get; set; }
    public bool IsGetCustomerAddresses { get; set; }
    public bool IsGetInvoices { get; set; }
    public bool IsGetUserPaymentMethods { get; set; }
}
