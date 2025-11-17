namespace ProjectShop.Server.Core.ValueObjects.Requests.FrontEndRequestsForProduct
{
    public class ProductUpdateBoolBatchRequest
    {
        public IEnumerable<string> Barcodes { get; set; } = [];
        public bool Value { get; set; }
    }
}
