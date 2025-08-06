using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetDataByEnumAsync<T> where T : class
    {
        Task<List<T>> GetAllByEnumAsync<TEnum>(TEnum value, string colName) where TEnum : Enum;
    }
}
