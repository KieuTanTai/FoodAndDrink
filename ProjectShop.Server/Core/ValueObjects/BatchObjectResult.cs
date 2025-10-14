namespace ProjectShop.Server.Core.ValueObjects
{
    public class BatchObjectResult<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> ValidEntities { get; set; } = [];
        public IEnumerable<BatchItemResult<TEntity>> BatchResults { get; set; } = [];
    }
}
