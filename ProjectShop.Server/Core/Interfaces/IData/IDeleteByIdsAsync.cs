using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IDeleteByIdsAsync<TKey> where TKey : struct
    {
        //public string GetDeleteQuery();
        Task<int> DeleteByIdsAsync(TKey keys);
    }
}
