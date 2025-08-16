using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ILocationDAO<T> : IGetByStatusAsync<T>, IGetRelativeAsync<T> where T : class
    {
        Task<T?> GetByLocationNameAsync(string name);
        Task<IEnumerable<T>> GetByLocationNamesAsync(IEnumerable<string> names);
        Task<IEnumerable<T>> GetByLocationTypeIdAsync(uint typeId);
        Task<IEnumerable<T>> GetByLocationTypeIdsAsync(IEnumerable<uint> typeIds);
        Task<T?> GetByHouseNumberAsync(string houseNumber);
        Task<IEnumerable<T>> GetByStreetAsync(string streetName);
        Task<T?> GetByLocationEmailAsync(string email);
        Task<IEnumerable<T>> GetByLocationEmailsAsync(IEnumerable<string> emails);
        Task<T?> GetByLocationPhoneAsync(string phone);
        Task<IEnumerable<T>> GetByLocationPhonesAsync(IEnumerable<string> phones);
        Task<IEnumerable<T>> GetByLocationCityAsync(uint cityId);
        Task<IEnumerable<T>> GetByLocationCitiesAsync(IEnumerable<uint> cityIds);
        Task<IEnumerable<T>> GetByLocationDistrictAsync(uint districtId);
        Task<IEnumerable<T>> GetByLocationDistrictsAsync(IEnumerable<uint> districtIds);
        Task<IEnumerable<T>> GetByLocationWardIdAsync(uint wardId);
        Task<IEnumerable<T>> GetByLocationWardIdsAsync(IEnumerable<uint> wardIds);
    }
}
