namespace ProjectShop.Server.Core.ValueObjects
{
    public class ProductUpdateStringBatchRequest
    {
        public IEnumerable<string> Barcodes { get; set; } = [];
        public IEnumerable<string> Values { get; set; } = [];
    }
}
