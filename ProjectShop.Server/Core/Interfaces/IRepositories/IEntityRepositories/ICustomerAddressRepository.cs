using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface ICustomerAddressRepository : IRepository<CustomerAddress>
    {
        Task<IEnumerable<CustomerAddress>> GetByCustomerIdAsync(uint customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<CustomerAddress>> GetByLocationIdAsync(uint locationId, CancellationToken cancellationToken = default);
        Task<CustomerAddress?> GetByCustomerIdAndLocationIdAsync(uint customerId, uint locationId, CancellationToken cancellationToken = default);
        Task<CustomerAddress?> GetDefaultAddressByCustomerIdAsync(uint customerId, CancellationToken cancellationToken = default);
    }
}
