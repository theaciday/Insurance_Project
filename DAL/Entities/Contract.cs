using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Contract
    {
        public int Id { get; set; }
        
        public string Details { get; set; }// Детали контракта
        
        public double FinalPrice { get; set; }// окончательная цена, после проверки рейтинга

        public int Duration { get; set; }

        public DateTime SignUpDate { get; set; }//Дата регистрации Страховой услуги

        public DateTime ValidityDate { get; set; }//Срок действия Страховой услуги

        public Customer Customer { get; set; }// Потребитель

        public int CustomerId { get; set; }

        public List<Insurance> Insuranses { get; set; }//Страховые услуги

    }
}
