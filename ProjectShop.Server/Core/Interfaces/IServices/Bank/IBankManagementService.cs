using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;

namespace ProjectShop.Server.Core.Interfaces.IServices.Bank
{
    public interface IBankManagementService<T> : IBaseService<T>, IBaseRelativeService<T> where T : class
    {
    }
}
