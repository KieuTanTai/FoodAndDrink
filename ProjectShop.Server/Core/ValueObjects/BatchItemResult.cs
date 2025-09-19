namespace ProjectShop.Server.Core.ValueObjects
{
    public class BatchItemResult<TEntity> where TEntity : class
    {
        // Backing fields
        private TEntity _input = default!;
        private bool _isSuccess;
        private string? _errorMessage;

        public TEntity Input
        {
            get => _input;
            set => _input = value;
        }

        public bool IsSuccess
        {
            get => _isSuccess;
            set => _isSuccess = value;
        }

        public string? ErrorMessage
        {
            get => _errorMessage;
            set => _errorMessage = value;
        }
    }
}
