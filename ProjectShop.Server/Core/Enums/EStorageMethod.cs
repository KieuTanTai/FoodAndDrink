namespace ProjectShop.Server.Core.Enums;

/// <summary>
/// Represents the storage method for products
/// </summary>
public enum EStorageMethod
{
    /// <summary>
    /// Requires refrigeration
    /// </summary>
    REFRIGERATED,

    /// <summary>
    /// Frozen storage
    /// </summary>
    FROZEN,

    /// <summary>
    /// Room temperature storage
    /// </summary>
    ROOM_TEMPERATURE,

    /// <summary>
    /// Dry storage conditions
    /// </summary>
    DRY_STORAGE
}