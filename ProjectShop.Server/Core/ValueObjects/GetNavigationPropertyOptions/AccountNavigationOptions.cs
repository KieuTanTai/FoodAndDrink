namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class AccountNavigationOptions
{
    public bool IsGetPerson { get; set; }
    public bool IsGetAccountAdditionalPermissions { get; set; }
    public bool IsGetAccountRoles { get; set; }
}
