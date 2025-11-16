namespace ProjectShop.Server.Core.ValueObjects
{
    public class ProductUpdateEnumBatchRequest<TEnum> where TEnum : Enum
    {
        public IEnumerable<string> Barcodes { get; set; } = [];
        public IEnumerable<TEnum> Values { get; set; } = [];
    }
}
