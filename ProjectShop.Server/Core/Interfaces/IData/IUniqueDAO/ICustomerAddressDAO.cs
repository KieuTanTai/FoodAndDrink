namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ICustomerAddressDAO<TEntity> : IGetByStatusAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByCityIdAsync(uint cityId);
        Task<IEnumerable<TEntity>> GetByCustomerIdAsync(string customerId);
        Task<IEnumerable<TEntity>> GetByDistrictIdAsync(uint districtId);
        Task<IEnumerable<TEntity>> GetByWardIdAsync(uint wardId);
        Task<IEnumerable<TEntity>> GetByCityIdAndDistrictIdAsync(uint cityId, uint districtId);
        Task<IEnumerable<TEntity>> GetByCityIdAndWardIdAsync(uint cityId, uint wardId);
        Task<IEnumerable<TEntity>> GetByDistrictIdAndWardIdAsync(uint districtId, uint wardId);
        Task<IEnumerable<TEntity>> GetByStreetLikeAsync(string streetLike);
    }
}
