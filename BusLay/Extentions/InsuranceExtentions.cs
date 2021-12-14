using BusLay.View;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLay.Extentions
{
    public static class InsuranceExtentions
    {
        public static InsuranceView ToView(this Insurance insurance)
        {
            return new InsuranceView()
            {
                Id = insurance.Id,
                InsurName = insurance.InsurName,
                InsurType = insurance.InsurType,
                Price = insurance.Price,
            };
        }
    }
}
