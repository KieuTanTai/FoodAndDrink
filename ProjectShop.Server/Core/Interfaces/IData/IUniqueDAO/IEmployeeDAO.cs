namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IEmployeeDAO<TEntity> : IPersonDAO<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByHouseNumberAsync(string houseNumber);
        Task<IEnumerable<TEntity>> GetByHouseNumbersAsync(IEnumerable<string> houseNumbers);
        Task<IEnumerable<TEntity>> GetByStreetAsync(string street);
        Task<IEnumerable<TEntity>> GetByStreetsAsync(IEnumerable<string> streets);
        Task<IEnumerable<TEntity>> GetByCityAsync(uint city);
        Task<IEnumerable<TEntity>> GetByCitiesAsync(IEnumerable<uint> cities);
        Task<IEnumerable<TEntity>> GetByWardIdAsync(uint wardId);
        Task<IEnumerable<TEntity>> GetByWardIdsAsync(IEnumerable<uint> wardIds);
        Task<IEnumerable<TEntity>> GetByDistrictIdAsync(uint districtId);
        Task<IEnumerable<TEntity>> GetByDistrictIdsAsync(IEnumerable<uint> districtIds);
        Task<IEnumerable<TEntity>> GetByLocationIdAsync(uint locationId);
        Task<IEnumerable<TEntity>> GetByLocationIdsAsync(IEnumerable<uint> locationIds);
    }
}
