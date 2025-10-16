using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class DetailSaleEventRepository : Repository<DetailSaleEvent>, IDetailSaleEventRepository
    {
        public DetailSaleEventRepository(IDBContext context) : base(context)
        {
        }

        // TODO: Implement all methods from IDetailSaleEventRepository
    }
}
