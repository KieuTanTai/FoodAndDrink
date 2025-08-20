using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Interfaces.IServices.Role;

namespace ProjectShop.Server.WebAPI.Controllers
{
    public class RoleServiceController : ControllerBase
    {
        private readonly ILogger<RoleServiceController> _logger;
        private readonly IAddAccountRoleService<RolesOfUserModel, RolesOfUserKey> _addAccountRoleService;
        private readonly IAddRoleService<RoleModel> _addRoleService;
        private readonly IUpdateRoleService _updateRoleService;
        private readonly IDeleteAccountRoleService<RolesOfUserKey> _deleteAccountRoleService;
        private readonly ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey> _searchAccountRoleService;
        private readonly ISearchRoleService<RoleModel, RoleNavigationOptions> _searchRoleService;

        public RoleServiceController(ILogger<RoleServiceController> logger, IAddAccountRoleService<RolesOfUserModel, RolesOfUserKey> addAccountRoleService, 
                IAddRoleService<RoleModel> addRoleService, IUpdateRoleService updateRoleService, IDeleteAccountRoleService<RolesOfUserKey> deleteAccountRoleService, ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, 
                RolesOfUserKey> searchAccountRoleService, ISearchRoleService<RoleModel, RoleNavigationOptions> searchRoleService)
        {
            _logger = logger;
            _addAccountRoleService = addAccountRoleService;
            _addRoleService = addRoleService;
            _updateRoleService = updateRoleService;
            _deleteAccountRoleService = deleteAccountRoleService;
            _searchAccountRoleService = searchAccountRoleService;
            _searchRoleService = searchRoleService;
        }

        //API test get list of roles
    }
}
