using System.Data;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IExecuteOperationsAsync
    {
#nullable enable
        Task<bool> ExecuteAsync(string query, object? parameters = null, IDbTransaction? transaction = null);
#nullable disable
    }
}
