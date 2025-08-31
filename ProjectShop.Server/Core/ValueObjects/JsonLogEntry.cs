namespace ProjectShop.Server.Core.ValueObjects
{
    public class JsonLogEntry
    {
        public DateTime QueryTime { get; set; } = DateTime.UtcNow;

        public string QueryTimeString => QueryTime.ToString("yyyy-MM-dd HH:mm:ss 'UTC'");

        public object? EntityCall { get; set; }

        public object? MethodCall { get; set; }

        public object? Entity { get; set; }

        public string? Message { get; set; }

        public string? ErrorName { get; set; }

        public string? ErrorMessage { get; set; }

        public int? AffectedRows { get; set; }
    }
}
