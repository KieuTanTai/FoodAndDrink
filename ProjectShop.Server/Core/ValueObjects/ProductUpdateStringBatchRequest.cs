namespace ProjectShop.Server.Core.ValueObjects
{
    public class ProductUpdateStringBatchRequest
    {
        // Backing fields
        private IEnumerable<string> _barcodes = [];
        private IEnumerable<string> _values = [];

        public IEnumerable<string> Barcodes
        {
            get => _barcodes;
            set => _barcodes = value ?? [];
        }

        public IEnumerable<string> Values
        {
            get => _values;
            set => _values = value ?? [];
        }
    }
}
