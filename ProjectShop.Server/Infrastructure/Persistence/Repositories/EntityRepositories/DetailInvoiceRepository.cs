using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class DetailInvoiceRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<DetailInvoice>(context, maxGetRecord), IDetailInvoiceRepository
    {
        public async Task<IEnumerable<DetailInvoice>> GetByInvoiceIdAsync(uint invoiceId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetailInvoice>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<DetailInvoice?> GetByInvoiceIdAndProductBarcodeAsync(uint invoiceId, string productBarcode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetailInvoice>> GetBySaleEventIdAsync(uint? saleEventId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
