namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class CategoryNavigationOptions
{
    // Backing fields
    private bool _isGetProductCategories;

    public bool IsGetProductCategories
    {
        get => _isGetProductCategories;
        set => _isGetProductCategories = value;
    }
}
