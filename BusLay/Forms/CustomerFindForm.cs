using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLay.Forms
{
    public class CustomerFindForm
    {
        public string FirstName { get; set; }
        public DateTime MaxDOB { get; set; }
        public DateTime MinDOB { get; set; }
        public int MaxRating { get; set; }
        public int MinRating { get; set; }
    }
}
