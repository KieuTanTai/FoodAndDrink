﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IQueryOperationsAsync
    {
#nullable enable
        Task<List<TResult>?> QueryAsync<TResult>(string query, object? parameters = null);
        Task<TResult?> QueryFirstOrDefaultAsync<TResult>(string query, object? parameters = null, IDbTransaction? transaction = null);
#nullable disable
    }
}
