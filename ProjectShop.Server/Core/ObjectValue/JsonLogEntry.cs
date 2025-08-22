namespace ProjectShop.Server.Core.ObjectValue
{
    public class JsonLogEntry
    {
        public DateTime QueryTime { get; set; } = DateTime.UtcNow;

        public object? EntityCall { get; set; }

        public object? Entity { get; set; }

        public string? ErrorName { get; set; }

        public string? Message { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
