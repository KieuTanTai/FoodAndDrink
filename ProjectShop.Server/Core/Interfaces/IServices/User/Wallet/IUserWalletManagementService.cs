namespace ProjectShop.Server.Core.Interfaces.IServices.User.Wallet
{
    internal interface IUserWalletManagementService<T> : IBaseService<T>, IBaseEnumTimeService<T>
        where T : class
    {

    }
}
