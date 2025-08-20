namespace ProjectShop.Server.Core.Entities.EntitiesRequest
{
    public class BatchItemResult<TEntity> where TEntity : class
    {
        public TEntity Input { get; set; } = default!;
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
