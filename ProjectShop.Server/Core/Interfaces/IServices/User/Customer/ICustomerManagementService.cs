namespace ProjectShop.Server.Core.Interfaces.IServices.User.Customer
{
    internal interface ICustomerManagementService<T> : IBaseService<T>, IBaseRelativeService<T>, IBaseEnumTimeService<T> where T : class
    {
        Task<List<T>> GetByGender<TEnum>(TEnum gender) where TEnum : Enum;
    }
}
