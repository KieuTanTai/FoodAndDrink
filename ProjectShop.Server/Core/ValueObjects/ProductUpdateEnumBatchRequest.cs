namespace ProjectShop.Server.Core.ValueObjects
{
    public class ProductUpdateEnumBatchRequest<TEnum> where TEnum : Enum
    {
        // Backing fields
        private IEnumerable<string> _barcodes = [];
        private IEnumerable<TEnum> _values = [];

        public IEnumerable<string> Barcodes
        {
            get => _barcodes;
            set => _barcodes = value ?? [];
        }

        public IEnumerable<TEnum> Values
        {
            get => _values;
            set => _values = value ?? [];
        }
    }
}
