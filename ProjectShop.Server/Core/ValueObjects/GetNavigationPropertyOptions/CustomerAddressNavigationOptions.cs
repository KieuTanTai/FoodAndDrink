namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class CustomerAddressNavigationOptions
{
    public bool IsGetCustomer { get; set; }
    public bool IsGetCustomerCity { get; set; }
    public bool IsGetCustomerDistrict { get; set; }
    public bool IsGetCustomerWard { get; set; }
}
