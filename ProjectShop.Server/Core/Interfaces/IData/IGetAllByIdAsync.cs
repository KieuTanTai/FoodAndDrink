using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetAllByIdAsync<T> where T : class
    {
#nullable enable
        Task<List<T>?> GetAllByIdAsync(string id, string colIdName);
    }
}
