namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetByEnumAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllByEnumAsync<TEnum>(TEnum tEnum, int? maxGetCount = null) where TEnum : Enum;
        Task<TEntity?> GetByEnumAsync<TEnum>(TEnum tEnum) where TEnum : Enum;
    }
}
