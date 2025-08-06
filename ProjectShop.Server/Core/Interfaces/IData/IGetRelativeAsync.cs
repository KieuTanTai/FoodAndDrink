using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetRelativeAsync<T> where T : class
    {
        public string GetQueryDataString(string colName);
        Task<List<T>> GetRelativeAsync(string input, string colName);
    }
}
