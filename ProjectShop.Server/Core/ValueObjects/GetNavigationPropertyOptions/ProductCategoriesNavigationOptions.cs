namespace ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

public class ProductCategoriesNavigationOptions
{
    // Backing fields
    private bool _isGetCategory;
    private bool _isGetProduct;

    public bool IsGetCategory
    {
        get => _isGetCategory;
        set => _isGetCategory = value;
    }

    public bool IsGetProduct
    {
        get => _isGetProduct;
        set => _isGetProduct = value;
    }
}
