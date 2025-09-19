namespace ProjectShop.Server.Core.ValueObjects
{
    public class ProductUpdateBoolBatchRequest
    {
        // Backing fields
        private IEnumerable<string> _barcodes = [];
        private bool _value;

        public IEnumerable<string> Barcodes
        {
            get => _barcodes;
            set => _barcodes = value ?? [];
        }

        public bool Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
