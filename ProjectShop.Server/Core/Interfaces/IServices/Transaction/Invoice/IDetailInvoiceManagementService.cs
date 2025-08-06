using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IServices.Transaction.Invoice
{
    public interface IDetailInvoiceManagementService<T, TKeys> : IBaseLinkingDataService<T, TKeys> where T : class where TKeys : class
    {
        Task<List<T>> GetAllByEnumAsync<TEnum>(TEnum value, string colName) where TEnum : Enum;
        Task<int> UpdateAsync(T entity);
        Task<int> UpdateManyAsync(IEnumerable<T> entities);
    }
}
