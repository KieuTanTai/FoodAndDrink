namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IColumnService
    {
        List<string> GetValidColumns(string tableName);
        bool IsValidColumn(string tableName, string columnName);
    }
}
