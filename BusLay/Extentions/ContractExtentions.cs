using BusLay.View;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLay.Extentions
{
    public static class ContractExtentions
    {
        public static ContractView ToView(this Contract contract)
        {
            return new ContractView()
            {
                CustomerId = contract.CustomerId,
                Details = contract.Details,
                Duration = contract.Duration,
                SignUpDate = contract.SignUpDate,
                FinalPrice = contract.FinalPrice,
                Id = contract.Id,
                Insuranses = contract.Insuranses.Select(x => x.InsurName).ToList(),
                ValidityDate = contract.ValidityDate,
            };
        }
    }
}
