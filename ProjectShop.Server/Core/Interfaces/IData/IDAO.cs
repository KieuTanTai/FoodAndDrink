using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IDAO<TModel> : ICrudOperationsAsync<TModel>, IQueryOperationsAsync, IExecuteOperationsAsync
        where TModel : class
    {
    }
}
