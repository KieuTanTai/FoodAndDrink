namespace ProjectShop.Server.Core.ValueObjects
{
    public class ServiceResults<TEntity> where TEntity : class
    {
        public IEnumerable<JsonLogEntry>? LogEntries { get; set; }
        public IEnumerable<TEntity>? Data { get; set; }

        public ServiceResults()
        {
            LogEntries = new List<JsonLogEntry>();
            Data = new List<TEntity>();
        }
    }
}
