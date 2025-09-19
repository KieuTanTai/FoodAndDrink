namespace ProjectShop.Server.Core.ValueObjects
{
    public class ProductUpdateDecimalBatchRequest
    {
        // Backing fields
        private IEnumerable<string> _barcodes = [];
        private IEnumerable<decimal> _values = [];

        public IEnumerable<string> Barcodes
        {
            get => _barcodes;
            set => _barcodes = value ?? [];
        }

        public IEnumerable<decimal> Values
        {
            get => _values;
            set => _values = value ?? [];
        }
    }
}
