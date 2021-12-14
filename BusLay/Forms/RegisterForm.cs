using DAL.Entities;
using System;
using System.Collections.Generic;

namespace BusLay.Forms
{
    public class RegisterForm
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public int Raiting { get; set; }
        public Contract Contract { get; set; }
    }
}
