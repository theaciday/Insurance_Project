using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLay.View
{
    public class CustomerView
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public int Rating { get; set; }
        public List<string> Roles { get; set; }
        public List<Contract> Contracts { get; set; }

    }
}
