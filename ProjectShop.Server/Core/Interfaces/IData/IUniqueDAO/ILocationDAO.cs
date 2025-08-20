namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ILocationDAO<TEntity> : IGetByStatusAsync<TEntity>, IGetRelativeAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByLocationNameAsync(string name);
        Task<IEnumerable<TEntity>> GetByLocationNamesAsync(IEnumerable<string> names);
        Task<IEnumerable<TEntity>> GetByLocationTypeIdAsync(uint typeId);
        Task<IEnumerable<TEntity>> GetByLocationTypeIdsAsync(IEnumerable<uint> typeIds);
        Task<TEntity?> GetByHouseNumberAsync(string houseNumber);
        Task<IEnumerable<TEntity>> GetByStreetAsync(string streetName);
        Task<TEntity?> GetByLocationEmailAsync(string email);
        Task<IEnumerable<TEntity>> GetByLocationEmailsAsync(IEnumerable<string> emails);
        Task<TEntity?> GetByLocationPhoneAsync(string phone);
        Task<IEnumerable<TEntity>> GetByLocationPhonesAsync(IEnumerable<string> phones);
        Task<IEnumerable<TEntity>> GetByLocationCityAsync(uint cityId);
        Task<IEnumerable<TEntity>> GetByLocationCitiesAsync(IEnumerable<uint> cityIds);
        Task<IEnumerable<TEntity>> GetByLocationDistrictAsync(uint districtId);
        Task<IEnumerable<TEntity>> GetByLocationDistrictsAsync(IEnumerable<uint> districtIds);
        Task<IEnumerable<TEntity>> GetByLocationWardIdAsync(uint wardId);
        Task<IEnumerable<TEntity>> GetByLocationWardIdsAsync(IEnumerable<uint> wardIds);
    }
}
