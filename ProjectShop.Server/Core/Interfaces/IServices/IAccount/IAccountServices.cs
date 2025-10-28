using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface IAccountServices : ILoginServices, ISearchAccountServices, ISignupServices, IUpdateAccountServices, IUpdatePasswordServices
    {
        
    }
}