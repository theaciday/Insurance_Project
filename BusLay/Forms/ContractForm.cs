using System;
using System.Collections.Generic;

namespace BusLay.Forms
{
    public class ContractForm
    {
        public string Details { get; set; }
        public double FinalPrice { get; set; }
        public int Duration { get; set; }
        public List<int> InsurancesId { get; set; }
        public int CustomerId { get; set; }
        public string ValidityDate { get; set; }

    }
}
