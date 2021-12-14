using BusLay.View;
using DAL.Entities;
using System.Linq;

namespace BusLay.Extentions
{
    public static class ViewExtentions
    {
        public static CustomerView ToView(this Customer customer)
        {
            return new CustomerView
            {
                Id = customer.Id,
                DOB = customer.DOB,
                Contracts = customer.Contracts,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Rating = customer.Rating,
                Roles = customer.Roles.Select(x => x.Name).ToList(),
                UserName = customer.Username
            };
        }
        public static CustomerForAdminView ToAdminView(this Customer customer)
        {
            return new CustomerForAdminView
            {
                Id = customer.Id,
                DOB = customer.DOB,
                Roles = customer.Roles.Select(x => x.Name).ToList(),
                Contracts = customer.Contracts,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Rating = customer.Rating,
                Username = customer.Username
            };
        }
    }
}
