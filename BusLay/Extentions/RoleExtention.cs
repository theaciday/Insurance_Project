using BusLay.View;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLay.Extentions
{
    public static class RoleExtention
    {
        public static RoleView ToView(this Role role)
        {
            return new RoleView()
            {
                Id = role.Id,
                RoleName = role.Name,
            };
        }
    }
}
