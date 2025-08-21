using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IInventoryDAO<TEntity> : IGetByStatusAsync<TEntity>, IGetDataByDateTimeAsync<TEntity> where TEntity : InventoryModel
    {
        Task<TEntity?> GetByLocationIdAsync(uint locationId);
        Task<IEnumerable<TEntity>> GetAllByLocationIdsAsync(IEnumerable<uint> locationIds, int? maxGetCount = null);
    }
}
