namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IBaseRelativeService<T> where T : class
    {
        Task<List<T>> GetRelativeAsync(string input, string colName);
        Task<List<T>> GetAllByEnumAsync<TEnum>(TEnum value, string colName) where TEnum : Enum;
    }
}
