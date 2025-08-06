using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IServices.User
{
    public interface IUserManagementService<T> : IBaseService<T>, IBaseRelativeService<T>, IBaseEnumTimeService<T> where T : class
    {

    }
}
