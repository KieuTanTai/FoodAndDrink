using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.WebAPI.Controllers
{
    public class AccountController(IAccountServices accountServices, ILogger<AccountController> logger, ILogService logService)
    {
        private readonly IAccountServices _accountServices = accountServices;
        private readonly ILogger<AccountController> _logger = logger;
        private readonly ILogService _logService = logService;
    }
}