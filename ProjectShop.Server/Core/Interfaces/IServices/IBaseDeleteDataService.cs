using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    internal interface IBaseDeleteDataService
    {
        Task<int> DeleteAsync(string id);
    }
}
