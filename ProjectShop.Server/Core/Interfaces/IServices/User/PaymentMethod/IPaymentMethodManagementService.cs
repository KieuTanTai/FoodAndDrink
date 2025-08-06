using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IServices.User.PaymentMethod
{
    public interface IPaymentMethodManagementService<T> : IBaseService<T>, IBaseRelativeService<T>, IBaseEnumTimeService<T>  where T : class
    {

        Task<List<T>> GetAllByIdAsync(string id, string colName);
    }
}
