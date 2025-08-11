namespace ProjectShop.Server.Core.Interfaces.IServices.User.Storage
{
    internal interface IDetailStorageManagementService<T, TKey> : IBaseLinkingDataService<T, TKey>, IBaseEnumTimeService<T> where T : class where TKey : class
    {
        Task<List<T>> GetByFavorated(bool isFavorated);
    }
}
