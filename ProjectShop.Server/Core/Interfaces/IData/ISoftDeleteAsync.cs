using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface ISoftDeleteAsync<T> where T : class
    {
        //public string GetSoftDeleteQuery();
        Task<bool> SoftDeleteAsync(T entity);
    }
}
