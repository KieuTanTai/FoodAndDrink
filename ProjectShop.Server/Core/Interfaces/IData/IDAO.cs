using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IDAO<TModel> : IDbOperationAsync<TModel>, IQueryOperationsAsync, IExecuteOperationsAsync
        where TModel : class
    {
    }
}
