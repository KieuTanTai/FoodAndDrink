namespace ProjectShop.Server.Core.Constants
{
    /// <summary>
    /// Defines primary key column names for entities.
    /// Most entities follow the convention: {EntityName}Id (e.g., AccountId, RoleId, ProductId)
    /// Only exceptions are listed here.
    /// </summary>
    public static class EntityPrimaryKeyNames
    {
        public const string IdSuffix = "Id";
        public const string ProductBarcode = "ProductBarcode";
    }
}
