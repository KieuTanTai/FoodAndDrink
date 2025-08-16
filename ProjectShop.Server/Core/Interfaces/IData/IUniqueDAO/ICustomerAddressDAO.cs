namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ICustomerAddressDAO<T> : IGetByStatusAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetByCityIdAsync(uint cityId);
        Task<IEnumerable<T>> GetByCustomerIdAsync(string customerId);
        Task<IEnumerable<T>> GetByDistrictIdAsync(uint districtId);
        Task<IEnumerable<T>> GetByWardIdAsync(uint wardId);
        Task<IEnumerable<T>> GetByCityIdAndDistrictIdAsync(uint cityId, uint districtId);
        Task<IEnumerable<T>> GetByCityIdAndWardIdAsync(uint cityId, uint wardId);
        Task<IEnumerable<T>> GetByDistrictIdAndWardIdAsync(uint districtId, uint wardId);
        Task<IEnumerable<T>> GetByStreetLikeAsync(string streetLike);
    }
}
