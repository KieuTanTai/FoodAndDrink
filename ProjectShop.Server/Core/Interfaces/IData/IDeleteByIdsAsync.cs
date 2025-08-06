using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IDeleteByIdsAsync
    {
        public string GetDeleteQuery();
        Task<int> DeleteByIdsAsync(object keys);
    }
}
