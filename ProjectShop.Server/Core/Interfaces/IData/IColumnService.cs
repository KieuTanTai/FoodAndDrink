namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IColumnService
    {
        Task<List<string>> GetValidColumns(string tableName);
        Task<bool> IsValidColumn(string tableName, string columnName);
    }
}
