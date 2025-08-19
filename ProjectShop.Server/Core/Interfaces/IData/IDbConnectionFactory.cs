using System.Data;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnection();
        //void ExecuteQuery(string query);
    }
}
