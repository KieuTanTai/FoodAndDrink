namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class CustomerNavigationOptions
{
    public bool IsGetCart { get; set; }
    public bool IsGetInvoices { get; set; }
    public bool IsGetCustomerAddresses { get; set; }
}
