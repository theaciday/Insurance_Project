using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public int Rating { get; set; }
        public List<Contract> Contracts { get; set; }
        public List<Role> Roles { get; set; }
    }
}
