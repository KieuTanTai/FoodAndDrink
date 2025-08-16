namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IEmployeeDAO<T> : IPersonDAO<T> where T : class
    {
        Task<T?> GetByHouseNumberAsync(string houseNumber);
        Task<IEnumerable<T>> GetByHouseNumbersAsync(IEnumerable<string> houseNumbers);
        Task<IEnumerable<T>> GetByStreetAsync(string street);
        Task<IEnumerable<T>> GetByStreetsAsync(IEnumerable<string> streets);
        Task<IEnumerable<T>> GetByCityAsync(uint city);
        Task<IEnumerable<T>> GetByCitiesAsync(IEnumerable<uint> cities);
        Task<IEnumerable<T>> GetByWardIdAsync(uint wardId);
        Task<IEnumerable<T>> GetByWardIdsAsync(IEnumerable<uint> wardIds);
        Task<IEnumerable<T>> GetByDistrictIdAsync(uint districtId);
        Task<IEnumerable<T>> GetByDistrictIdsAsync(IEnumerable<uint> districtIds);
        Task<IEnumerable<T>> GetByLocationIdAsync(uint locationId);
        Task<IEnumerable<T>> GetByLocationIdsAsync(IEnumerable<uint> locationIds);
    }
}
