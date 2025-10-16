using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IDetailProductLotRepository : IRepository<DetailProductLot>
    {
        Task<IEnumerable<DetailProductLot>> GetByProductLotIdAsync(uint productLotId, CancellationToken cancellationToken = default);
        Task<IEnumerable<DetailProductLot>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default);
        Task<DetailProductLot?> GetByProductLotIdAndProductBarcodeAsync(uint productLotId, string productBarcode, CancellationToken cancellationToken = default);
    }
}
