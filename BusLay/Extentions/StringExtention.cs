using System;

namespace BusLay.Extentions
{
    public static class StringExtention
    {
        public static Guid ToGuid(this string s)
        {
            return Guid.Parse(s);
        }
    }
}
