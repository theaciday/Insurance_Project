using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Session
    {
        public Guid Id { get; set; }
        public Guid RefreshToken { get; set; }
        public int CustomerId { get; set; }
        public bool IsExpired { get; set; }
        public Customer Customer { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
