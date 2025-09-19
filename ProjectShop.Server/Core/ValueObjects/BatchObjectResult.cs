namespace ProjectShop.Server.Core.ValueObjects
{
    public class BatchObjectResult<TEntity> where TEntity : class
    {
        // Backing fields
        private IEnumerable<TEntity> _validEntities = [];
        private IEnumerable<BatchItemResult<TEntity>> _batchResults = [];

        public IEnumerable<TEntity> ValidEntities
        {
            get => _validEntities;
            set => _validEntities = value;
        }

        public IEnumerable<BatchItemResult<TEntity>> BatchResults
        {
            get => _batchResults;
            set => _batchResults = value;
        }
    }
}
