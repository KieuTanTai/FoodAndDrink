namespace ProjectShop.Server.Core.ValueObjects
{
    public class ServiceResult<TEntity> where TEntity : class
    {
        public IEnumerable<JsonLogEntry>? LogEntries { get; set; }
        public TEntity? Data { get; set; }
        public bool IsSuccess { get; set; }

        public ServiceResult()
        {
            LogEntries = [];
            Data = null;
            IsSuccess = false;
        }

        public ServiceResult(IEnumerable<JsonLogEntry>? logEntries, TEntity? data, bool isSuccess)
        {
            LogEntries = logEntries;
            Data = data;
            IsSuccess = isSuccess;
        }

        public ServiceResult(bool isSuccess)
        {
            LogEntries = [];
            Data = null;
            IsSuccess = isSuccess;
        }
    }
}
