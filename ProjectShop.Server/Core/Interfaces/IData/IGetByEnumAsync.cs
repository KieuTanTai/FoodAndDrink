namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetByEnumAsync<T> where T : class
    {
        Task<List<T>> GetAllByEnumAsync<TEnum>(TEnum tEnum, string colName) where TEnum : Enum;
        Task<T> GetByEnumAsync<TEnum>(TEnum tEnum, string colName) where TEnum : Enum;
    }
}
