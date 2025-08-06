namespace ProjectShop.Server.Core.Interfaces.IServices.User.Publisher
{
    internal interface IPublisherManagementService<T> : IBaseService<T>, IBaseEnumTimeService<T>, IBaseRelativeService<T> where T : class
    {
    }
}
