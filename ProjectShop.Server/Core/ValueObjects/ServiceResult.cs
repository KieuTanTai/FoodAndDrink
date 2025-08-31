namespace ProjectShop.Server.Core.ValueObjects
{
    public class ServiceResult<TEntity> where TEntity : class
    {
        public IEnumerable<JsonLogEntry>? LogEntries { get; set; }
        public TEntity? Data { get; set; }

        public ServiceResult()
        {
            LogEntries = new List<JsonLogEntry>();
            Data = null;
        }
    }
}
