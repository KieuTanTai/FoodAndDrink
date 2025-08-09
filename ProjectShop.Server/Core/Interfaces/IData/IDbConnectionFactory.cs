namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IDbConnectionFactory
    {
        System.Data.IDbConnection CreateConnection();
        //void ExecuteQuery(string query);
    }
}
