namespace ProjectShop.Server.Core.Interfaces.IServices.Bank
{
    public interface IBankManagementService<T> : IBaseService<T>, IBaseRelativeService<T> where T : class
    {
    }
}
