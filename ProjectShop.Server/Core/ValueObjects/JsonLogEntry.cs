namespace ProjectShop.Server.Core.ValueObjects
{
    public class JsonLogEntry
    {
        // Backing fields
        private DateTime _queryTime = DateTime.UtcNow;
        private object? _entityCall;
        private object? _methodCall;
        private object? _entity;
        private string? _message;
        private string? _errorName;
        private string? _errorMessage;
        private int? _affectedRows;

        public DateTime QueryTime
        {
            get => _queryTime;
            set => _queryTime = value;
        }

        public string QueryTimeString => QueryTime.ToString("yyyy-MM-dd HH:mm:ss 'UTC'");

        public object? EntityCall
        {
            get => _entityCall;
            set => _entityCall = value;
        }

        public object? MethodCall
        {
            get => _methodCall;
            set => _methodCall = value;
        }

        public object? Entity
        {
            get => _entity;
            set => _entity = value;
        }

        public string? Message
        {
            get => _message;
            set => _message = value;
        }

        public string? ErrorName
        {
            get => _errorName;
            set => _errorName = value;
        }

        public string? ErrorMessage
        {
            get => _errorMessage;
            set => _errorMessage = value;
        }

        public int? AffectedRows
        {
            get => _affectedRows;
            set => _affectedRows = value;
        }
    }
}
