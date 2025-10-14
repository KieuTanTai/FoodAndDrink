namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class UserPaymentMethodNavigationOptions
{
    public bool IsGetBank { get; set; }
    public bool IsGetCustomer { get; set; }
    public bool IsGetInvoices { get; set; }
}
