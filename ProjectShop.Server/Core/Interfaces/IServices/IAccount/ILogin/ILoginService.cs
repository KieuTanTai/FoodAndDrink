namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount.ILogin
{
    internal interface ILoginService<T> where T : class
    {
        Task<T> HandleLoginAsync(string username, string password);
    }
}
