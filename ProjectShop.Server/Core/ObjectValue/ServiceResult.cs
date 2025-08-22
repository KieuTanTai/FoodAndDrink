namespace ProjectShop.Server.Core.ObjectValue
{
    public class ServiceResult<TEntity> where TEntity : class
    {
        public IEnumerable<JsonLogEntry>? LogEntry { get; set; }
        public IEnumerable<TEntity>? Data { get; set; }
    }
}
