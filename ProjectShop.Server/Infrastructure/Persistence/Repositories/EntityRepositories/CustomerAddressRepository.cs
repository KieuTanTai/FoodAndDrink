using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class CustomerAddressRepository(IDBContext context, IMaxGetRecord maxGetRecord) : Repository<CustomerAddress>(context, maxGetRecord), ICustomerAddressRepository
    {
        public async Task<IEnumerable<CustomerAddress>> GetByCustomerIdAsync(uint customerId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomerAddress>> GetByLocationIdAsync(uint locationId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerAddress?> GetByCustomerIdAndLocationIdAsync(uint customerId, uint locationId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerAddress?> GetDefaultAddressByCustomerIdAsync(uint customerId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
