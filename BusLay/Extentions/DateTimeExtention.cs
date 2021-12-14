using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLay.Extentions
{
    public static class DateTimeExtention
    {
        public static DateTime ToDateTime(this string s)
        {
            return Convert.ToDateTime(s);
        }

    }
}
