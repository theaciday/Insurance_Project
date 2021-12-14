using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    //Страхование
    public class Insurance
    {
        public int Id { get; set; }
      
        public string InsurName { get; set; }
    
        public string InsurType { get; set; }
   
        public double Price { get; set; }
        
        public Contract Contract { get; set; }//Договор
    }
}
