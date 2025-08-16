namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetByEnumAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetAllByEnumAsync<TEnum>(TEnum tEnum) where TEnum : Enum;
        Task<T?> GetByEnumAsync<TEnum>(TEnum tEnum) where TEnum : Enum;
    }
}
