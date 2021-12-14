using BusLay.Context;
using BusLay.Forms;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLay.Services
{
    public class RoleService
    {
        private readonly DataContext dbContext;
        public RoleService(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string AddRole(CreateRoleForm roleForm)
        {
            var newRole = dbContext.Roles.Add(new Role
            {
                Name = roleForm.RoleName,
            });
            return newRole.Entity.Name;
        }

        public List<Role> GetRoles()
        {
            var roles = dbContext.Roles.AsQueryable().ToList();
            return roles;
        }
    }
}
