namespace ProjectShop.Server.Core.ValueObjects.Requests.FrontEndRequestsForProduct
{
    public class ProductUpdateStringBatchRequest
    {
        public IEnumerable<string> Barcodes { get; set; } = [];
        public IEnumerable<string> Values { get; set; } = [];
    }
}
