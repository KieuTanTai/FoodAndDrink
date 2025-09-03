namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ILocationDAO<TEntity> : IBaseLocationDAO<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByLocationNameAsync(string name);
        Task<IEnumerable<TEntity>> GetByLocationNamesAsync(IEnumerable<string> names, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLocationTypeIdAsync(uint typeId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLocationTypeIdsAsync(IEnumerable<uint> typeIds, int? maxGetCount = null);
        Task<TEntity?> GetByHouseNumberAsync(string houseNumber);
        Task<IEnumerable<TEntity>> GetByStreetAsync(string streetName, int? maxGetCount = null);
        Task<TEntity?> GetByLocationEmailAsync(string email);
        Task<IEnumerable<TEntity>> GetByLocationEmailsAsync(IEnumerable<string> emails, int? maxGetCount = null);
        Task<TEntity?> GetByLocationPhoneAsync(string phone);
        Task<IEnumerable<TEntity>> GetByLocationPhonesAsync(IEnumerable<string> phones, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLocationCityAsync(uint cityId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLocationCitiesAsync(IEnumerable<uint> cityIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLocationDistrictAsync(uint districtId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLocationDistrictsAsync(IEnumerable<uint> districtIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLocationWardIdAsync(uint wardId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLocationWardIdsAsync(IEnumerable<uint> wardIds, int? maxGetCount = null);
    }
}
