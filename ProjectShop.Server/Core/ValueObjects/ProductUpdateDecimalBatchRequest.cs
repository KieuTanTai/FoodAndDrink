namespace ProjectShop.Server.Core.ValueObjects
{
    public class ProductUpdateDecimalBatchRequest
    {
        public IEnumerable<string> Barcodes { get; set; } = [];
        public IEnumerable<decimal> Values { get; set; } = [];
    }
}
