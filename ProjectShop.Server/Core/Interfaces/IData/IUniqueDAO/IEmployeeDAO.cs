namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IEmployeeDAO<TEntity> : IPersonDAO<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByHouseNumberAsync(string houseNumber);
        Task<IEnumerable<TEntity>> GetByHouseNumbersAsync(IEnumerable<string> houseNumbers, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByStreetAsync(string street, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByStreetsAsync(IEnumerable<string> streets, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByCityAsync(uint city, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByCitiesAsync(IEnumerable<uint> cities, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByWardIdAsync(uint wardId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByWardIdsAsync(IEnumerable<uint> wardIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByDistrictIdAsync(uint districtId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByDistrictIdsAsync(IEnumerable<uint> districtIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLocationIdAsync(uint locationId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLocationIdsAsync(IEnumerable<uint> locationIds, int? maxGetCount = null);
    }
}
