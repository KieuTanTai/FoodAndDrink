namespace ProjectShop.Server.Core.ValueObjects
{
    public class ServiceResult<TEntity> where TEntity : class
    {
        // Backing fields
        private IEnumerable<JsonLogEntry>? _logEntries;
        private TEntity? _data;
        private bool _isSuccess;

        public IEnumerable<JsonLogEntry>? LogEntries
        {
            get => _logEntries;
            set => _logEntries = value;
        }

        public TEntity? Data
        {
            get => _data;
            set => _data = value;
        }

        public bool IsSuccess
        {
            get => _isSuccess;
            set => _isSuccess = value;
        }

        public ServiceResult()
        {
            LogEntries = [];
            Data = null;
            IsSuccess = false;
        }
    }
}
