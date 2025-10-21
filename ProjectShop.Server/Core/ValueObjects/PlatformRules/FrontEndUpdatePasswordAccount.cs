using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.PlatformRules
{
    public class FrontEndUpdatePasswordAccount
    {
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public FrontEndUpdatePasswordAccount() { }

        public FrontEndUpdatePasswordAccount(uint accountId, string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}