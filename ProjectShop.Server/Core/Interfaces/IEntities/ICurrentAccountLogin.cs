namespace ProjectShop.Server.Core.Interfaces.IEntities
{
    public interface ICurrentAccountLogin<T, TRole> where T : class where TRole : class
    {
        void SetAccount(T currentAccount);
        T GetCurrentAccountAsync();
        Task<IEnumerable<TRole>> GetCurrentAccountRolesAsync();
    }
}
