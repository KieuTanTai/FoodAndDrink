using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IServices.User.Developer
{
    internal interface IDeveloperManagementService<T> : IBaseService<T>, IBaseRelativeService<T>, IBaseEnumTimeService<T> where T : class
    {
    }
}
