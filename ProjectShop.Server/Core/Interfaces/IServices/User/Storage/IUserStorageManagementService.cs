namespace ProjectShop.Server.Core.Interfaces.IServices.User.Storage
{
    internal interface IUserStorageManagementService<T> where T : class
    {
        Task<List<T>> GetAllUserStorage();
        Task<T> GetStorageById(string id);
        Task<int> InsertUserStorage(T entity);
    }
}
