using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IInventoryDAO<T> : IGetByStatusAsync<T>, IGetDataByDateTimeAsync<T> where T : InventoryModel
    {
        Task<T?> GetByLocationIdAsync(uint locationId);
        Task<IEnumerable<T>> GetAllByLocationIdsAsync(IEnumerable<uint> locationIds);
    }
}
