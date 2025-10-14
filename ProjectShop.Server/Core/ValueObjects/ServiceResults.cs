namespace ProjectShop.Server.Core.ValueObjects
{
    public class ServiceResults<TEntity> where TEntity : class
    {
        public IEnumerable<JsonLogEntry>? LogEntries { get; set; }
        public IEnumerable<TEntity>? Data { get; set; }
        public bool IsSuccess { get; set; }

        public ServiceResults()
        {
            LogEntries = [];
            Data = [];
            IsSuccess = false;
        }

        public ServiceResults(IEnumerable<JsonLogEntry>? logEntries, IEnumerable<TEntity>? data, bool isSuccess)
        {
            LogEntries = logEntries;
            Data = data;
            IsSuccess = isSuccess;
        }

        public ServiceResults(bool isSuccess)
        {
            LogEntries = [];
            Data = [];
            IsSuccess = isSuccess;
        }
    }
}
