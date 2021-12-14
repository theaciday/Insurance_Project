using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLay.View
{
    public class ContractView
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public double FinalPrice { get; set; }// окончательная цена, после проверки рейтинга
        public int Duration { get; set; }
        public DateTime SignUpDate { get; set; }//Дата регистрации Страховой услуги

        public DateTime ValidityDate { get; set; }//Срок действия Страховой услуги
        public int CustomerId { get; set; }
        public List<string> Insuranses { get; set; }//Страховые услуги
    }
}
