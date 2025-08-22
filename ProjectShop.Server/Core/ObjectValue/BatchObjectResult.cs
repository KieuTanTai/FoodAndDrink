namespace ProjectShop.Server.Core.ObjectValue
{
    public class BatchObjectResult<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> ValidEntities { get; set; } = new List<TEntity>();
        public IEnumerable<BatchItemResult<TEntity>> BatchResults { get; set; } = new List<BatchItemResult<TEntity>>();
    }
}
