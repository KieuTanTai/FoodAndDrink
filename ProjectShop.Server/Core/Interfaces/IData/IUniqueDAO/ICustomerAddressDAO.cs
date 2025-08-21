namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ICustomerAddressDAO<TEntity> : IGetByStatusAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByCityIdAsync(uint cityId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByCustomerIdAsync(string customerId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByDistrictIdAsync(uint districtId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByWardIdAsync(uint wardId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByCityIdAndDistrictIdAsync(uint cityId, uint districtId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByCityIdAndWardIdAsync(uint cityId, uint wardId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByDistrictIdAndWardIdAsync(uint districtId, uint wardId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByStreetLikeAsync(string streetLike, int? maxGetCount = null);
    }
}
