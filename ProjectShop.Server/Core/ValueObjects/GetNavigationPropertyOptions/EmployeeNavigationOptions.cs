namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class EmployeeNavigationOptions
{
    public bool IsGetPerson { get; set; }
    public bool IsGetEmployeeWorkLocation { get; set; }
    public bool IsGetDisposeProducts { get; set; }
    public bool IsGetInvoices { get; set; }
}
