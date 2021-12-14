using DAL.Entities;
using System;
using System.Collections.Generic;

namespace BusLay.Forms
{
    public class CustomerForm
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public int Rating { get; set; }
        public Contract Contract { get; set; }
        public List<string> Roles { get; set; }
    }
}
