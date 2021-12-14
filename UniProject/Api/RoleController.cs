using BusLay.Extentions;
using BusLay.Services;
using BusLay.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace UniProject.Api
{
    [Route("api")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly RoleService roleService;

        public RoleController(RoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("roles")]
        public ActionResult<List<RoleView>> AllRoles()
        {
            return roleService.GetRoles().Select(x => x.ToView()).ToList();
        }

    }
}
